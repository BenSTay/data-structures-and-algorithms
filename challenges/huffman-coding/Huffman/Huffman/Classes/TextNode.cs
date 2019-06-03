using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Classes
{
    public class TextNode
    {
        public uint Count { get; set; }
        public char Char { get; set; }
        public TextNode Left { get; set; }
        public TextNode Right { get; set; }
        public bool[] Bits { get; set; }

        /// <summary>
        /// Constructs a leaf node.
        /// </summary>
        /// <param name="count">The number of times the node's char appears in the input text.</param>
        /// <param name="chr">A char from the input text.</param>
        public TextNode(uint count, char chr)
        {
            Count = count;
            Char = chr;
        }

        /// <summary>
        /// Constructs a parent node (with no Char).
        /// </summary>
        /// <param name="left">The node's left child.</param>
        /// <param name="right">The node's right child.</param>
        public TextNode(TextNode left, TextNode right)
        {
            Left = left;
            Right = right;
            Count = left.Count + right.Count;
        }

        /// <summary>
        /// Default Node constructor.
        /// </summary>
        public TextNode() { }
    }
}
