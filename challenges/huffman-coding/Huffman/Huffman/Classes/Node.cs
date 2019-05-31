using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Classes
{
    public class Node
    {
        public uint Count { get; set; }
        public char Char { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(uint count, char chr)
        {
            Count = count;
            Char = chr;
        }

        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
            Count = left.Count + right.Count;
        }

        public Node() { }
    }
}
