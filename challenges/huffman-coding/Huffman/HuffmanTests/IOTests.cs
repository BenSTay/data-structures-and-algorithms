using System;
using Xunit;
using System.IO;
using Huffman.Classes;

namespace HuffmanTests
{
    public class IOTests
    {
        [Fact]
        public void CanGetChars()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/CanGetChars.txt";
            string success = "Success";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(success);
            }

            Assert.Equal(success.ToCharArray(), FileReader.GetChars(path));
        }
    }
}
