using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman.Classes
{
    public class Tree
    { 
        public Node Root { get; set; }

        public Tree(Dictionary<char, uint> charCounts)
        {
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
    }
}
