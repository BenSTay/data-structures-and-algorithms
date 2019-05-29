using System;
using Huffman.Classes;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] chars = FileReader.GetChars($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/shakespeare-hamlet-25.txt");
            Tree tree = new Tree(chars);
        }
    }
}
