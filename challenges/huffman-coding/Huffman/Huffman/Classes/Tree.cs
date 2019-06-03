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
            int pathLength = _document.Path.Length - _document.Name.Length - _document.Ext.Length;
            string path = $"{_document.Path.Substring(0, pathLength)}{_document.Name}-compressed{_document.Ext}";
            Queue<bool> bitQueue = new Queue<bool>();

            foreach (byte b in _document.Bytes)
            {
                foreach (bool bit in Nodes[b].Bits)
                {
                    bitQueue.Enqueue(bit);
                }
            }

            WriteHeader(path, (byte)(bitQueue.Count % 8));

            using (FileStream fs = File.Open(path, FileMode.Append))
            {
                while (bitQueue.Count > 0)
                {
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
            int pathLength = _document.Path.Length - _document.Name.Length - _document.Ext.Length;
            string path = $"{_document.Path.Substring(0, pathLength)}{_document.Name}-decompressed{_document.Ext}";
            Node current = Root;

            using (FileStream fs = File.Create(path))
            {
                for (ulong i = Header.Length; i < (ulong)_document.Bytes.Length - 1; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (current.Left is null)
                        {
                            fs.WriteByte(current.Byte);
                            current = Root;
                        }

                        if ((_document.Bytes[i] & (1 << (7 - j))) == 0)
                            current = current.Left;

                        else current = current.Right;
                    }
                }

                for (int i = 0; i < 8 - Header.TrailingBits; i++)
                {
                    if ((_document.Bytes[_document.Bytes.Length - 1] & (1 << (7 - i))) == 0)
                        current = current.Left;

                    else current = current.Right;

                    if (current.Left is null)
                    {
                        fs.WriteByte(current.Byte);
                        current = Root;
                    }
                }
            }
        }
    }
}
