using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Huffman.Classes
{
    public class FileReader
    {

        public static char[] GetChars(string file)
        {
            string fileText;

            using (StreamReader reader = new StreamReader(file))
            {
                fileText = reader.ReadToEnd();
            }

            return fileText.ToCharArray();
        }
    }
}
