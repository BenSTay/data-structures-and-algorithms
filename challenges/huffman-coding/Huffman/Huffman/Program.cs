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

            string compressedFilePath = $"{filePath.Substring(0, filePath.Length - 4)}-compressed.txt";

            tree.Compress(compressedFilePath);

            Tree tree2 = new Tree(compressedFilePath);

            tree2.Decompress($"{filePath.Substring(0,filePath.Length - 4)}-decompressed.txt");
        }
    }
}
