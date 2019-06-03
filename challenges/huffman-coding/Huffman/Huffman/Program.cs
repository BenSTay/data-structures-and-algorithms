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
            //Document doc = new Document("../../../../../shakespeare.txt");

            //Tree tree = new Tree(doc, false);
            //tree.Compress();

            //Document doc2 = new Document("../../../../../shakespeare-compressed.txt");
            //Tree tree2 = new Tree(doc2, true);
            //tree2.Decompress();

            //Document doc = new Document("C:/Users/stayl/Music/impossible_soul.wav");
            //Tree tree = new Tree(doc, false);
            //tree.Compress();

            Document doc2 = new Document("C:/Users/stayl/Music/impossible_soul-compressed.wav");
            Tree tree2 = new Tree(doc2, true);
            tree2.Decompress();
        }
    }
}
