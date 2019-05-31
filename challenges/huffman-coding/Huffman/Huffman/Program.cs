using System;
using System.Collections.Generic;
using Huffman.Classes;
using System.IO;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../../../shakespeare.txt";

            Tree tree = new Tree(filePath);

            tree.Compress($"{filePath}c");
        }
    }
}
