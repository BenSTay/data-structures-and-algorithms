using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman.Classes
{
    class PriorityQueue
    {
        readonly Dictionary<ulong, Queue<Node>> PQ;
        private List<ulong> Counts;
        public uint Count { get; set; } = 0;

        public PriorityQueue(Dictionary<byte, ulong> byteCounts)
        {
            PQ = new Dictionary<ulong, Queue<Node>>();

            foreach (byte b in byteCounts.Keys)
            {
                Node node = new Node(b, byteCounts[b]);

                if (!PQ.ContainsKey(byteCounts[b]))
                    PQ.Add(byteCounts[b], new Queue<Node>());

                PQ[byteCounts[b]].Enqueue(node);
                Count++;
            }

            Counts = PQ.Keys.ToList();
            Counts.Sort();
        }

        public void Enqueue(Node node)
        {
            if (!PQ.ContainsKey(node.Count))
            {
                PQ.Add(node.Count, new Queue<Node>());

                if (Counts.Count == 0 || Counts.Last() < node.Count)
                    Counts.Add(node.Count);

                else Counts.Insert(Counts.FindIndex(c => c > node.Count), node.Count);
            }

            PQ[node.Count].Enqueue(node);
            Count++;
        }

        public Node Dequeue()
        {
            if (PQ.Keys.Count == 0)
                throw new InvalidOperationException("Cannot Dequeue from empty Queue");

            Node node = PQ[Counts[0]].Dequeue();

            if (PQ[Counts[0]].Count == 0)
            {
                PQ.Remove(Counts[0]);
                Counts.RemoveAt(0);
            }

            Count--;

            return node;
        }
    }
}
