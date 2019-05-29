using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Huffman.Classes
{
    public class FileReader
    {

        public static Dictionary<char, uint> GetChars(string file)
        {
            Dictionary<char, uint> charCounts = new Dictionary<char, uint>();

            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    char c = (char)reader.Read();

                    if (charCounts.ContainsKey(c)) charCounts[c]++;
                    else charCounts.Add(c, 1);
                }
            }

            return charCounts;
        }
    }
}
