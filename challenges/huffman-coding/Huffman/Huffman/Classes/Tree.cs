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
        public Node Root { get; set; }
        public string Text { get; set; }

        public Tree(string path)
        {
            Text = File.ReadAllText(path);

            Dictionary<char, uint> charCounts = new Dictionary<char, uint>();

            foreach (char c in Text)
            {
                if (charCounts.ContainsKey(c)) charCounts[c]++;
                else charCounts.Add(c, 1);
            }

            Root = Build(SortChars(charCounts));
        }

        private static Dictionary<uint, Queue<Node>> SortChars(Dictionary<char, uint> charCounts)
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

        private static Node Build(Dictionary<uint, Queue<Node>> nodeValues)
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

            return nodeValues[values[0]].Dequeue();
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

        public void Compress(string path)
        {
            Dictionary<char, bool[]> charBits = GetCharBits();
            Queue<bool> bitQueue = new Queue<bool>();

            foreach (char c in charBits.Keys)
            {
                foreach (byte b in Encoding.UTF32.GetBytes($"{c}"))
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        bitQueue.Enqueue(!((b & (1 << i)) == 0));
                    }
                }

                foreach (bool bit in charBits[c])
                {
                    bitQueue.Enqueue(bit);
                }   

                for ( int i = 0; i < 32; i++)
                {
                    bitQueue.Enqueue(false);
                }
            }

            for (int i = 0; i < 32; i++)
            {
                bitQueue.Enqueue(false);
            }

            foreach (char c in Text)
            {
                foreach (bool bit in charBits[c])
                {
                    bitQueue.Enqueue(bit);
                }
            }

            for (int i = 0; i < 32; i++)
            {
                bitQueue.Enqueue(false);
            }

            using (FileStream fs = File.Create(path))
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
    }
}
