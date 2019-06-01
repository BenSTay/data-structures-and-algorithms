using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Huffman.Classes
{
    public class Tree
    {
        private const char Separator = (char)27;
        public Node Root { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }

        public Tree(string path)
        {
            Path = path;
            Text = File.ReadAllText(path);
        }

        // Compression Methods

        /// <summary>
        /// 
        /// </summary>
        public void Compress()
        {
            BuildTree();

            Dictionary<char, bool[]> charBits = GetCharBits();
            Queue<bool> bitQueue = GetBitQueue(charBits);

            string newPath = $"{Path}c";
            WriteHeader(charBits, newPath, bitQueue.Count % 8);
            WriteCompressedBody(newPath, bitQueue);
        }

        /// <summary>
        /// Constructs a Huffman tree using the file's text.
        /// </summary>
        private void BuildTree()
        {
            Dictionary<uint, Queue<Node>> nodes = MakeNodes();

            List<uint> values = nodes.Keys.ToList();
            values.Sort();

            Node left = null;
            uint value = values[0];

            while (nodes[value].First().Count < Text.Length)
            {
                Node node = nodes[value].Dequeue();
                if (left is null) left = node;
                else
                {
                    Node parent = new Node(left, node);
                    left = null;

                    uint count = parent.Count;

                    if (nodes.ContainsKey(count)) nodes[count].Enqueue(parent);
                    else
                    {
                        if (values.Count == 0 || count > values.Last()) values.Add(count);
                        else values.Insert(values.FindIndex(v => v > count), count);

                        Queue<Node> queue = new Queue<Node>();
                        queue.Enqueue(parent);

                        nodes.Add(count, queue);
                    }
                }

                if (nodes[value].Count == 0)
                {
                    nodes.Remove(value);
                    values.RemoveAt(0);
                    value = values[0];
                }
            }

            Root = nodes[value].Dequeue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<uint, Queue<Node>> MakeNodes()
        {
            Dictionary<char, uint> charCounts = new Dictionary<char, uint>();

            foreach (char c in Text)
            {
                if (charCounts.ContainsKey(c)) charCounts[c]++;
                else charCounts.Add(c, 1);
            }

            Dictionary<uint, Queue<Node>> nodeValues = new Dictionary<uint, Queue<Node>>();

            foreach (char c in charCounts.Keys)
            {
                uint count = charCounts[c];
                if (!nodeValues.ContainsKey(count)) nodeValues.Add(count, new Queue<Node>());

                Node node = new Node(count, c);
                nodeValues[count].Enqueue(node);
            }

            return nodeValues;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<char, bool[]> GetCharBits()
        {
            Dictionary<char, bool[]> charBits = new Dictionary<char, bool[]>();
            GetCharBits(charBits, Root, new bool[0]);
            return charBits;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charBits"></param>
        /// <param name="node"></param>
        /// <param name="bits"></param>
        private void GetCharBits(Dictionary<char, bool[]> charBits, Node node, bool[] bits)
        {
            if (node.Char != 0) charBits.Add(node.Char, bits);
            else
            {
                GetCharBits(charBits, node.Left, bits.Append(false).ToArray());
                GetCharBits(charBits, node.Right, bits.Append(true).ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charBits"></param>
        /// <returns></returns>
        private Queue<bool> GetBitQueue(Dictionary<char, bool[]> charBits)
        {
            Queue<bool> bitQueue = new Queue<bool>();

            foreach (char c in Text)
            {
                foreach (bool bit in charBits[c])
                {
                    bitQueue.Enqueue(bit);
                }
            }

            return bitQueue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charBits"></param>
        /// <param name="path"></param>
        /// <param name="remainder"></param>
        private void WriteHeader(Dictionary<char, bool[]> charBits, string path, int remainder)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                // Writes the # of bits to ignore at the end of the file
                if (remainder == 0) sw.Write($"0{Separator}");
                else sw.Write($"{8 - remainder}{Separator}");

                // Writes the huffman table
                foreach (char c in charBits.Keys)
                {
                    sw.Write($"{c}{Separator}");
                    foreach (bool bit in charBits[c])
                    {
                        if (bit) sw.Write(1);
                        else sw.Write(0);
                    }
                    sw.Write(Separator);
                }
                sw.Write('\0');
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bitQueue"></param>
        private void WriteCompressedBody(string path, Queue<bool> bitQueue)
        {
            using (FileStream fs = File.Open(path, FileMode.Append))
            {
                while (bitQueue.Count > 0)
                {
                    byte b = 0;

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

        // Decompression Methods

        /// <summary>
        /// Decompresses a compressed text file.
        /// </summary>
        public void Decompress()
        {
            ParseHeader();
            Queue<bool> bits = ParseBody();

            string newPath = $"{Path.Substring(0, Path.Length - 5)}-decompressed.txt";
            Node current = Root;

            using (StreamWriter sw = File.CreateText(newPath))
            {
                // Traverses the Huffman tree bit by bit, printing the char held
                // in each leaf node to the decompressed file.
                while (bits.TryDequeue(out bool bit))
                {
                    if (!bit) current = current.Left;
                    else current = current.Right;

                    if (current.Char != (char)0)
                    {
                        sw.Write(current.Char);
                        current = Root;
                    }
                }
            }
        }

        /// <summary>
        /// Re-constructs the Huffman tree using the compressed file's header.
        /// </summary>
        private void ParseHeader()
        {
            Root = new Node();

            // Gets the file's header and splits it on the default separator.
            string header = Text.Substring(0, Text.IndexOf('\0'));
            string[] split = header.Split(Separator);
            
            // Iterates through header, ignoring the first line.
            for (int i = 1; i < split.Length - 1; i++)
            {
                char c = split[i++][0];
                string bits = split[i];

                Node current = Root;

                // Adds the node containing char c to the tree at the position
                // specifified by its associated binary code.
                for (int j = 0; j < bits.Length; j++)
                {
                    if (bits[j] == '0')
                    {
                        if (current.Left is null) current.Left = new Node();
                        current = current.Left;
                    }
                    else
                    {
                        if (current.Right is null) current.Right = new Node();
                        current = current.Right;
                    }
                }
                current.Char = c;
            }
        }

        /// <summary>
        /// Parses the body of a compressed text file.
        /// </summary>
        /// <returns>A Queue of bits (represented as booleans)</returns>
        private Queue<bool> ParseBody()
        {
            // Gets the number of bytes taken by the header.
            string header = Text.Substring(0, Text.IndexOf('\0'));
            int headerBytes = Encoding.UTF8.GetByteCount(header);

            byte[] bytes = File.ReadAllBytes(Path);
            Queue<bool> bits = new Queue<bool>();

            // Parses all bytes after the header, ignoring the last byte.
            for (int i = headerBytes; i < bytes.Length - 1; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    bits.Enqueue(!((bytes[i] & (1 << j)) == 0));
                }
            }

            // Parses the last byte, ignoring trailing zeroes.
            int ignoredBits = int.Parse(Text.First().ToString());
            for (int j = 7; j >= ignoredBits; j--)
            {
                bits.Enqueue(!((bytes[bytes.Length - 1] & (1 << j)) == 0));
            }

            return bits;
        }
    }
}
