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

        private Dictionary<uint, Queue<Node>> MakeNodes(Dictionary<char, uint> charCounts)
        {
            Dictionary<uint, Queue<Node>> nodeValues = new Dictionary<uint, Queue<Node>>();

            foreach (char c in charCounts.Keys)
            {
                uint count = charCounts[c];
                if (!nodeValues.ContainsKey(count)) nodeValues.Add(count, new Queue<Node>());
                nodeValues[count].Enqueue(new Node(count, c));
            }

            return nodeValues;
        }

        private void BuildTree(Dictionary<uint, Queue<Node>> nodeValues)
        {
            List<uint> values = nodeValues.Keys.ToList();
            values.Sort();
            
            while (values.Count > 1 || nodeValues[values[0]].Count > 1)
            {
                Node left, right, parent;

                left = nodeValues[values[0]].Dequeue();

                if (nodeValues[values[0]].TryDequeue(out Node n2)) right = n2;
                else
                {
                    nodeValues.Remove(values[0]);
                    values.RemoveAt(0);
                    right = nodeValues[values[0]].Dequeue();
                }

                if (nodeValues[values[0]].Count == 0)
                {
                    nodeValues.Remove(values[0]);
                    values.RemoveAt(0);
                }

                parent = new Node(left, right);

                if (nodeValues.ContainsKey(parent.Count)) nodeValues[parent.Count].Enqueue(parent);
                else
                {
                    if (values.Count == 0 || parent.Count > values.Last()) values.Add(parent.Count);
                    else values.Insert(values.FindIndex(v => v > parent.Count), parent.Count);

                    Queue<Node> queue = new Queue<Node>();
                    queue.Enqueue(parent);

                    nodeValues.Add(parent.Count, queue);
                }
            }

            Root = nodeValues[values[0]].Dequeue();
        }

        private Dictionary<char, bool[]> GetCharBits()
        {
            Dictionary<char, bool[]> charBits = new Dictionary<char, bool[]>();
            GetCharBits(charBits, Root, new bool[0]);
            return charBits;
        }

        private void GetCharBits(Dictionary<char, bool[]> charBits, Node node, bool[] bits)
        {
            if (node.Char != 0) charBits.Add(node.Char, bits);
            else
            {
                GetCharBits(charBits, node.Left, bits.Append(false).ToArray());
                GetCharBits(charBits, node.Right, bits.Append(true).ToArray());
            }
        }

        public void Compress()
        {
            // Construct Huffman Tree
            Dictionary<char, uint> charCounts = new Dictionary<char, uint>();

            foreach (char c in Text)
            {
                if (charCounts.ContainsKey(c)) charCounts[c]++;
                else charCounts.Add(c, 1);
            }

            Dictionary<uint, Queue<Node>> nodeValues = MakeNodes(charCounts);
            BuildTree(nodeValues);

            Dictionary<char, bool[]> charBits = GetCharBits();
            bool[] test = charBits['\r'];
            Queue<bool> bitQueue = GetBitQueue(charBits);

            string newPath = $"{Path}c";
            WriteHeader(charBits, newPath, bitQueue.Count % 8);
            WriteCompressedBody(newPath, bitQueue);
        }

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
                sw.Write(Separator);
            }
        }
        
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

        public void Decompress()
        {
            string[] split = Text.Split(Separator);
            int ignoredBits = int.Parse(split[0]);

            int startIndex = BuildTree(split);
            Queue<bool> bits = GetBits(split, startIndex, ignoredBits);

            string newPath = $"{Path.Substring(0, Path.Length - 4)}decompressed.txt";
            Node current = Root;

            using (StreamWriter sw = File.CreateText(newPath))
            {
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

        private int BuildTree(string[] split)
        {
            Root = new Node();
            Node current;
            int i;

            for (i = 1; i < split.Length; i += 2)
            {
                if (string.IsNullOrEmpty(split[i])) break;

                char c = split[i].ToCharArray()[0];
                string bits = split[i+ 1];

                current = Root;

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

            return i + 1;
        }

        private Queue<bool> GetBits(string[] split, int startIndex, int ignoredBits)
        {
            int i;
            StringBuilder builder = new StringBuilder();
            for (i = 0; i < startIndex; i++)
            {
                builder.Append(split[i]);
            }

            byte[] bytes = File.ReadAllBytes(Path);
            int headerBytes = Encoding.UTF8.GetByteCount(builder.ToString());
            Queue<bool> bits = new Queue<bool>();

            for (i = headerBytes + startIndex; i < bytes.Length - 1; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    bits.Enqueue(!((bytes[i] & (1 << j)) == 0));
                }
            }

            for (int j = 7; j >= ignoredBits; j--)
            {
                bits.Enqueue(!((bytes[i] & (1 << j)) == 0));
            }

            return bits;
        }
    }
}
