using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman.Classes
{
    public class Heap
    {
        private class HeapNode
        {
            public char Value { get; set; }
            public uint Count { get; set; }
            public HeapNode Left { get; set; }
            public HeapNode Right { get; set; }
            public HeapNode Parent { get; set; }

            public HeapNode(char value, uint count)
            {
                Value = value;
                Count = count;
            }

            public HeapNode(char value, uint count, HeapNode parent)
            {
                Value = value;
                Count = count;
                Parent = parent;
            }
        }

        readonly HeapNode Root;
        int Count;

        public Heap(Dictionary<char, uint> chars)
        {
            Count = chars.Keys.Count;
            Root = Populate(chars);
            Heapify(Root);
        }

        private static HeapNode Populate(Dictionary<char, uint> chars)
        {
            if (chars.Keys.Count == 0) return null;

            char key = chars.Keys.First();
            uint value = chars.GetValueOrDefault(key);
            chars.Remove(key);

            HeapNode root = new HeapNode(key, value);
            Queue<HeapNode> breadth = new Queue<HeapNode>();
            HeapNode current = root;

            while (chars.Keys.Count > 0)
            {
                key = chars.Keys.First();
                value = chars.GetValueOrDefault(key);
                chars.Remove(key);

                if (current.Right != null) current = breadth.Dequeue();
                HeapNode node = new HeapNode(key, value, current);
                breadth.Enqueue(node);

                if (current.Left is null) current.Left = node;
                else current.Right = node;
            }

            return root;
        }

        private static void Heapify(HeapNode current)
        {
            if (current.Left != null) Heapify(current.Left);
            if (current.Right != null) Heapify(current.Right);

            while (current.Parent != null)
            {
                if (current.Count < current.Parent.Count)
                {
                    Swap(current, current.Parent);
                    current = current.Parent;
                }
                else break;
            }
        }

        private static void Swap(HeapNode node1, HeapNode node2)
        {
            char tempValue = node1.Value;
            uint tempCount = node1.Count;

            node1.Value = node2.Value;
            node1.Count = node2.Count;

            node2.Value = tempValue;
            node2.Count = tempCount;
        }

        public List<Node> Sort()
        {
            List<Node> kvps = new List<Node>();

            while (Count > 1)
            {
                kvps.Add(new Node(Root.Count, Root.Value));
                HeapNode runner = Root;

                int depth = (int)Math.Log(Count, 2);
                int min = (int)(Math.Pow(2, depth) + Math.Pow(2, depth - 1));

                // Finds the last node in the heap
                while (depth > 0)
                {
                    depth--;

                    if (Count < min)
                    {
                        runner = runner.Left;
                        min -= (int)Math.Pow(2, depth - 1);
                    }
                    else
                    {
                        runner = runner.Right;
                        min += (int)Math.Pow(2, depth - 1);
                    }
                }

                Swap(Root, runner);

                if (runner.Parent.Right == runner) runner.Parent.Right = null;
                else runner.Parent.Left = null;
                runner.Parent = null;

                Count--;
                Heapify(Root);
            }

            kvps.Add(new Node(Root.Count, Root.Value));

            return kvps;
        }
    }
}
