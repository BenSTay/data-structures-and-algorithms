using System;
using System.Collections.Generic;
using Huffman.Classes;
using System.IO;
using System.Text;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../../../shakespeare.txt";

            Tree tree = new Tree(filePath);

            tree.Compress();

            Tree tree2 = new Tree($"{filePath}c");

            tree2.Decompress();
        }
    }
}
