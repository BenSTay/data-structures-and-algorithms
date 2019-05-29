using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Classes
{
    public class Tree
    {
        public Tree(char[] chars)
        {
            SortChars(CountChars(chars));
        }

        private static Dictionary<char, uint> CountChars(char[] chars)
        {
            Dictionary<char, uint> charTable = new Dictionary<char, uint>();

            foreach (char c in chars)
            {
                if (charTable.ContainsKey(c)) charTable[c]++;
                else charTable.Add(c, 1);
            }

            return charTable;
        }

        private static List<Node> SortChars(Dictionary<char, uint> charTable)
        {
            Heap heap = new Heap(charTable);
            return heap.Sort();
        }
    }
}
