using Huffman.Classes;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document("../../../../../shakespeare.txt");
            Tree tree = new Tree(doc);
            tree.GenerateUML();
            //tree.Compress();

            //Document doc2 = new Document("../../../../../shakespeare-compressed.txt");
            //Tree tree2 = new Tree(doc2);
            //tree2.Decompress();
        }
    }
}
