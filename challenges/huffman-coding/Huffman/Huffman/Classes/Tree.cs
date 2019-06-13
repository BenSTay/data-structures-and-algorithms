using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Huffman.Classes
{
    class Tree
    {
        readonly Document _document;
        private Node Root { get; set; }
        private Dictionary<byte, Node> Nodes { get; set; }
        private HeaderInfo Header { get; set; }

        public Tree(Document document)
        {
            _document = document;
            Header = _document.ParseHeader();

            if (!Header.IsValid)
            {
                Root = Build(_document);
                Nodes = new Dictionary<byte, Node>();
                GetNodeBits(Root, Nodes, new bool[0]);
            }

            else
            {
                Root = new Node();
                Build(Root, Header);
            }
        }

        private static Node Build(Document document)
        {
            Dictionary<byte, ulong> byteCounts = document.GetByteCounts();
            PriorityQueue PQ = new PriorityQueue(byteCounts);

            while (PQ.Count > 1)
            {
                Node node = new Node(PQ.Dequeue(), PQ.Dequeue());
                PQ.Enqueue(node);
            }

            return PQ.Dequeue();
        }

        private static void Build(Node root, HeaderInfo header)
        {
            foreach (byte b in header.HuffmanTable.Keys)
            {
                Node current = root;

                for (int i = 0; i < header.HuffmanTable[b].Length; i++)
                {
                    if (!header.HuffmanTable[b][i])
                    {
                        if (current.Left is null)
                            current.Left = new Node();

                        current = current.Left;
                    }

                    else
                    {
                        if (current.Right is null)
                            current.Right = new Node();

                        current = current.Right;
                    }
                }

                current.Byte = b;
            }
        }

        public static void GetNodeBits(Node root, Dictionary<byte, Node> nodes, bool[] bits)
        {
            if (root.Left is null)
            {
                root.Bits = bits;
                nodes.Add(root.Byte, root);
            }
            else
            {
                GetNodeBits(root.Left, nodes, bits.Append(false).ToArray());
                GetNodeBits(root.Right, nodes, bits.Append(true).ToArray());
            }
        }

        public void Compress()
        {
            if (Header.IsValid) throw new InvalidOperationException("Cannot compress compressed file");

            int pathLength = _document.Path.Length - _document.Name.Length - _document.Ext.Length;
            string path = $"{_document.Path.Substring(0, pathLength)}{_document.Name}-compressed{_document.Ext}";
            string temp = $"{path.Substring(0, path.Length - _document.Ext.Length)}.temp";

            byte currentByte = 0;
            int bitPosition = 7;

            List<byte> encoded = new List<byte>();
            byte[] bytes;

            BinaryWriter writer = new BinaryWriter(File.Create(temp));

            using (BinaryReader reader = new BinaryReader(File.Open(_document.Path, FileMode.Open)))
            {
                long totalbits = reader.BaseStream.Length;

                while (reader.BaseStream.Position < totalbits - Document.Mibibyte)
                {
                    bytes = reader.ReadBytes(Document.Mibibyte);
                    (currentByte, bitPosition) = Encode(currentByte, bitPosition, bytes, encoded);

                    if (encoded.Count >= Document.Mibibyte)
                    {
                        writer.Write(encoded.ToArray());
                        encoded.Clear();
                    }
                }

                bytes = reader.ReadBytes((int)(totalbits % Document.Mibibyte));
                (currentByte, bitPosition) = Encode(currentByte, bitPosition, bytes, encoded);

                if (bitPosition != 7) encoded.Add(currentByte);

                writer.Write(encoded.ToArray());
                encoded.Clear();
            }

            writer.Dispose();
            WriteHeader(path, temp, (byte)((bitPosition + 1) % 8));
        }

        private (byte, int) Encode(byte currentByte, int bitPosition, byte[] bytes, List<byte> encoded)
        {
            foreach (byte b in bytes)
            {
                for (int i = 0; i < Nodes[b].Bits.Length; i++)
                {
                    if (Nodes[b].Bits[i])
                        currentByte |= (byte)(1 << bitPosition);

                    bitPosition--;

                    if (bitPosition < 0)
                    {
                        bitPosition = 7;
                        encoded.Add(currentByte);
                        currentByte = 0;
                    }
                }
            }

            return (currentByte, bitPosition);
        }

        private void WriteHeader(string path, string temp, byte trailingBits)
        {
            FileStream tempStream = File.Open(temp, FileMode.Open);
            using (FileStream stream = File.Create(path))
            {
                stream.WriteByte(trailingBits);
                foreach (byte b in Nodes.Keys)
                {
                    stream.WriteByte(b);
                    stream.WriteByte((byte)Nodes[b].Bits.Length);

                    int pos = 0;

                    while (pos < Nodes[b].Bits.Length)
                    {
                        byte bits = 0;

                        for (int i = 7; i >= 0; i--)
                        {
                            if (Nodes[b].Bits[pos++])
                                bits |= (byte)(1 << i);

                            if (Nodes[b].Bits.Length == pos) break;
                        }

                        stream.WriteByte(bits);
                    }
                }
                stream.WriteByte(Nodes.Keys.Last());

                tempStream.CopyTo(stream);
                tempStream.Dispose();
                File.Delete(temp);
            }
        }

        public void Decompress()
        {
            if (!Header.IsValid) throw new InvalidOperationException("Cannot decompressed uncompressed file.");

            int pathLength = _document.Path.Length - _document.Name.Length - _document.Ext.Length;
            string path = $"{_document.Path.Substring(0, pathLength)}{_document.Name}-decompressed{_document.Ext}";
            Node current = Root;

            List<byte> decoded = new List<byte>();
            byte[] encoded;

            BinaryWriter writer = new BinaryWriter(File.Create(path));

            using (BinaryReader reader = new BinaryReader(File.Open(_document.Path, FileMode.Open)))
            {
                long totalBytes = reader.BaseStream.Length;
                reader.BaseStream.Position = Header.EndPosition;

                while (reader.BaseStream.Position < totalBytes - Document.Mibibyte)
                {
                    encoded = reader.ReadBytes(Document.Mibibyte);
                    current = Decode(encoded, decoded, current);

                    if (decoded.Count >= Document.Mibibyte)
                    {
                        writer.Write(decoded.ToArray());
                        decoded.Clear();
                    }
                }

                encoded = reader.ReadBytes((int)((totalBytes - Header.EndPosition) % Document.Mibibyte) - 1);
                current = Decode(encoded, decoded, current);

                byte last = reader.ReadByte();

                for (int i = 0; i < (8 - Header.TrailingBits); i++)
                {
                    if ((last & (1 << (7 - i))) == 0)
                        current = current.Left;

                    else current = current.Right;

                    if (current.Left is null)
                    {
                        decoded.Add(current.Byte);
                        current = Root;
                    }
                }
            }

            writer.Write(decoded.ToArray());
        }

        private Node Decode(byte[] encoded, List<byte> decoded, Node current)
        {
            foreach (byte b in encoded)
            {
                for (int i = 0; i < 8; i++)
                {
                    if ((b & (1 << (7 - i))) == 0)
                        current = current.Left;

                    else current = current.Right;

                    if (current.Left is null)
                    {
                        decoded.Add(current.Byte);
                        current = Root;
                    }
                }
            }
            return current;
        }

        public void GenerateUML()
        {
            StringBuilder UMLBuilder = new StringBuilder();

            UMLBuilder.AppendLine("@startuml");

            if (Root.Left.Left != null)
            {
                UMLBuilder.AppendLine("( ) -down-> (0)");
                GenerateUML(UMLBuilder, "0", Root.Left);
            }
            else UMLBuilder.AppendLine($"( ) -down-> (\"{Root.Left.Byte}\")");

            if (Root.Right.Left != null)
            {
                UMLBuilder.AppendLine("( ) -down-> (1)");
                GenerateUML(UMLBuilder, "1", Root.Right);
            }
            else UMLBuilder.AppendLine($"( ) -down-> (\"{Root.Right.Byte}\")");

            UMLBuilder.AppendLine("@enduml");

            _document.Path.Substring(0, _document.Path.Length - _document.Ext.Length);

            File.WriteAllText(
                $"{_document.Path.Substring(0, _document.Path.Length - _document.Ext.Length)}.puml",
                    UMLBuilder.ToString()
                ); 
        }

        private void GenerateUML(StringBuilder UMLBuilder, string binary, Node current)
        {
            if (current.Left.Left != null)
            {
                UMLBuilder.AppendLine($"({binary}) -down-> ({binary}0)");
                GenerateUML(UMLBuilder, $"{binary}0", current.Left);
            }
            else UMLBuilder.AppendLine($"({binary}) -down-> (\"{current.Left.Byte}\")");

            if (current.Right.Left != null)
            {
                UMLBuilder.AppendLine($"({binary}) -down-> ({binary}1)");
                GenerateUML(UMLBuilder, $"{binary}1", current.Right);
            }
            else UMLBuilder.AppendLine($"({binary}) -down-> (\"{current.Right.Byte}\")");
        }
    }
}
