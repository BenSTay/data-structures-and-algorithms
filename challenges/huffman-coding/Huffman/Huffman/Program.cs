using System;
using System.Collections.Generic;
using Huffman.Classes;
using System.Text;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document("../../../../../shakespeare.txt");

            Tree tree = new Tree(doc, false);
            tree.Compress();

            Document doc2 = new Document("../../../../../shakespeare-compressed.txt");
            Tree tree2 = new Tree(doc2, true);
            tree2.Decompress();
        }
    }
}
