using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Huffman.Classes
{
    class Tree
    {
        readonly Document _document;
        private Node Root { get; set; }
        private Dictionary<byte, Node> Nodes { get; set; }
        private HeaderInfo Header { get; set; }
        readonly bool Compressed;

        public Tree(Document document, bool compressed)
        {
            _document = document;
            Compressed = compressed;

            if (!Compressed)
            {
                Root = Build(_document);
                Nodes = new Dictionary<byte, Node>();
                GetNodeBits(Root, Nodes, new bool[0]);
            }

            else
            {
                Header = _document.ParseHeader();
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
            if (Compressed) throw new InvalidOperationException("Cannot compress compressed file");

            int pathLength = _document.Path.Length - _document.Name.Length - _document.Ext.Length;
            string path = $"{_document.Path.Substring(0, pathLength)}{_document.Name}-compressed{_document.Ext}";
            Queue<bool> bitQueue = new Queue<bool>();

            using (FileStream stream = File.Open(_document.Path, FileMode.Open))
            {
                while (stream.Position < stream.Length)
                {
                    byte b = (byte)stream.ReadByte();
                    for (int i = 0; i < Nodes[b].Bits.Length; i++)
                    {
                        bitQueue.Enqueue(Nodes[b].Bits[i]);
                    }
                }
            }

            WriteHeader(path, (byte)(bitQueue.Count % 8));
            int length = bitQueue.Count;


            using (FileStream fs = File.Open(path, FileMode.Append))
            {
                while (bitQueue.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Writing: {100 - (100 * bitQueue.Count)/length}%");

                    byte b = 0;

                    // Dequeues 8 bits from the queue at a time to create bytes which can be
                    // written to the new file.
                    for (int i = 0; i < 8; i++)
                    {
                        if (bitQueue.TryDequeue(out bool bit))
                        {
                            if (bit) b |= (byte)(1 << (7 - i));
                        }
                        else break;
                    }

                    fs.WriteByte(b);
                }
            }
        }

        private void WriteHeader(string path, byte trailingBits)
        {
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
            }
        }

        public void Decompress()
        {
            if (!Compressed) throw new InvalidOperationException("Cannot decompressed uncompressed file.");

            int pathLength = _document.Path.Length - _document.Name.Length - _document.Ext.Length;
            string path = $"{_document.Path.Substring(0, pathLength)}{_document.Name}-decompressed{_document.Ext}";
            Node current = Root;

            FileStream readStream = File.Open(_document.Path, FileMode.Open);
            FileStream writeStream = File.Create(path);

            readStream.Position = Header.EndPosition;

            byte b;

            while (readStream.Position < readStream.Length - 1)
            {
                b = (byte)readStream.ReadByte();

                for (int i = 0; i < 8; i++)
                {
                    if ((b & (1 << (7 - i))) == 0)
                        current = current.Left;

                    else current = current.Right;

                    if (current.Left is null)
                    {
                        writeStream.WriteByte(current.Byte);
                        current = Root;
                    }
                }
            }

            b = (byte)readStream.ReadByte();
            readStream.Dispose();

            for (int i = 0; i < 8 - Header.TrailingBits; i++)
            {
                if ((b & (1 << (7 - i))) == 0)
                    current = current.Left;

                else current = current.Right;

                if (current.Left is null)
                {
                    writeStream.WriteByte(current.Byte);
                    current = Root;
                }
            }

            writeStream.Dispose();
        }
    }
}
