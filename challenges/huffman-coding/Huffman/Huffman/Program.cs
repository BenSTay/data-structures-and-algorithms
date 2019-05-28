using System;
using Huffman.Classes;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader.GetChars($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/shakespeare-hamlet-25.txt");
            Console.WriteLine("Hello World!");
        }
    }
}
