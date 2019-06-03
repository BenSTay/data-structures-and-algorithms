using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Huffman.Classes
{
    public class TextTree
    {
        private const char Separator = (char)27;
        public TextNode Root { get; set; }
        public Dictionary<char, TextNode> Nodes { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }

        /// <summary>
        /// Constructs a new Tree using the given input file. 
        /// </summary>
        /// <param name="path">The input file's location.</param>
        public TextTree(string path)
        {
            Path = path;
            Text = File.ReadAllText(path);
            Nodes = new Dictionary<char, TextNode>();
        }

        // Compression Methods

        /// <summary>
        /// Uses Huffman Coding to compress a text file at the given path.
        /// </summary>
        /// <param name="path">The compressed file's destination.</param>
        public void Compress(string path)
        {
            BuildTree();

            Queue<bool> bitQueue = new Queue<bool>();

            foreach (char c in Text)
            {
                foreach (bool bit in Nodes[c].Bits)
                {
                    bitQueue.Enqueue(bit);
                }
            }

            WriteHeader(path, bitQueue.Count % 8);
            WriteCompressedBody(path, bitQueue);
        }

        /// <summary>
        /// Constructs a Huffman tree using the file's text.
        /// </summary>
        private void BuildTree()
        {
            Dictionary<uint, Queue<TextNode>> nodes = MakeNodes();

            List<uint> values = nodes.Keys.ToList();
            values.Sort();

            TextNode left = null;
            uint value = values[0];

            
            while (nodes[value].First().Count < Text.Length)
            {
                // Dequeues the node with the lowest count from the queue
                TextNode node = nodes[value].Dequeue();

                if (left is null) left = node;

                // Once two nodes have been dequeued, they are added as children to a
                // new parent node, which has the sum of its children's counts as its count.
                else
                {
                    TextNode parent = new TextNode(left, node);
                    left = null;

                    uint count = parent.Count;

                    // The parent node is inserted back into the "priority queue".
                    if (nodes.ContainsKey(count)) nodes[count].Enqueue(parent);
                    else
                    {
                        if (values.Count == 0 || count > values.Last()) values.Add(count);
                        else values.Insert(values.FindIndex(v => v > count), count);

                        Queue<TextNode> queue = new Queue<TextNode>();
                        queue.Enqueue(parent);

                        nodes.Add(count, queue);
                    }
                }

                // Once a "sub-queue" has been emptied, it is removed.
                if (nodes[value].Count == 0)
                {
                    nodes.Remove(value);
                    values.RemoveAt(0);
                    value = values[0];
                }
            }

            Root = nodes[value].Dequeue();
            GetCharBits(Root, new bool[0]);
        }

        /// <summary>
        /// Creates the leaf nodes of the Huffman Tree and places them into a "priority queue".
        /// </summary>
        /// <returns>The "priority queue" (represented as a dictionary of queues)</returns>
        private Dictionary<uint, Queue<TextNode>> MakeNodes()
        {
            Dictionary<char, uint> charCounts = new Dictionary<char, uint>();

            // Counts the number of occurrances for each char in the input text.
            foreach (char c in Text)
            {
                if (charCounts.ContainsKey(c)) charCounts[c]++;
                else charCounts.Add(c, 1);
            }

            Dictionary<uint, Queue<TextNode>> nodeValues = new Dictionary<uint, Queue<TextNode>>();

            // Creates nodes using each char and its associated count, and places them into a 
            // "priority queue"
            foreach (char c in charCounts.Keys)
            {
                uint count = charCounts[c];
                if (!nodeValues.ContainsKey(count)) nodeValues.Add(count, new Queue<TextNode>());

                TextNode node = new TextNode(count, c);
                nodeValues[count].Enqueue(node);
                Nodes.Add(c, node);
            }

            return nodeValues;
        }

        /// <summary>
        /// Recursively assigns each leaf node the binary sequence required to navigate to it from the root.
        /// </summary>
        /// <param name="node">The node being accessed.</param>
        /// <param name="bits">The binary sequence.</param>
        private void GetCharBits(TextNode node, bool[] bits)
        {
            if (node.Char != 0) node.Bits = bits;
            else
            {
                GetCharBits(node.Left, bits.Append(false).ToArray());
                GetCharBits(node.Right, bits.Append(true).ToArray());
            }
        }

        /// <summary>
        /// Writes the Huffman table to the output file so that the tree can be reconstructed during decompression.
        /// </summary>
        /// <param name="path">The compressed file's path.</param>
        /// <param name="remainder">The number of trailing bits to ignore.</param>
        private void WriteHeader(string path, int remainder)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                // Writes the # of bits to ignore at the end of the file
                if (remainder == 0) sw.Write($"0{Separator}");
                else sw.Write($"{8 - remainder}{Separator}");

                // Writes the huffman table
                foreach (char c in Nodes.Keys)
                {
                    sw.Write($"{c}{Separator}");
                    foreach (bool bit in Nodes[c].Bits)
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
        /// Writes all chars in the original file to the new file using their Huffman codes.
        /// </summary>
        /// <param name="path">The new file's path.</param>
        /// <param name="bitQueue">The queue of bits to be written to the file.</param>
        private void WriteCompressedBody(string path, Queue<bool> bitQueue)
        {
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

        // Decompression Methods

        /// <summary>
        /// Decompresses a compressed text file.
        /// </summary>
        /// <param name="path">The decompressed file's destination.</param>
        public void Decompress(string path)
        {
            ParseHeader();
            Queue<bool> bits = ParseBody();

            TextNode current = Root;

            using (StreamWriter sw = File.CreateText(path))
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
            Root = new TextNode();

            // Gets the file's header and splits it on the default separator.
            string header = Text.Substring(0, Text.IndexOf('\0'));
            string[] split = header.Split(Separator);
            
            // Iterates through header, ignoring the first line.
            for (int i = 1; i < split.Length - 1; i++)
            {
                char c = split[i++][0];
                string bits = split[i];

                TextNode current = Root;

                // Adds the node containing char c to the tree at the position
                // specifified by its associated binary code.
                for (int j = 0; j < bits.Length; j++)
                {
                    if (bits[j] == '0')
                    {
                        if (current.Left is null) current.Left = new TextNode();
                        current = current.Left;
                    }
                    else
                    {
                        if (current.Right is null) current.Right = new TextNode();
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
