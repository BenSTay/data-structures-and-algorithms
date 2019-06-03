namespace Huffman.Classes
{
    class Node
    {
        public byte Byte { get; set; }
        public ulong Count { get; set; }
        public bool[] Bits { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }


        public Node(byte b, ulong count)
        {
            Byte = b;
            Count = count;
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
