using System;
using System.Collections.Generic;
using Huffman.Classes;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, uint> charCounts = FileReader.GetChars($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/shakespeare-hamlet-25.txt");
            Tree tree = new Tree(charCounts);
        }
    }
}
