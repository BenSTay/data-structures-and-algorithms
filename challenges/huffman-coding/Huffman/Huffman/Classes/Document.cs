using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Huffman.Classes
{
    class Document
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Ext { get; set; }

        public const int Mibibyte = 1048576;

        public Document(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            Path = path;
            Ext = path.Substring(path.LastIndexOf('.'));
            Name = path.Substring(path.LastIndexOf('/') + 1);
            Name = Name.Substring(0, Name.Length - Ext.Length);
        }

        public Dictionary<byte, ulong> GetByteCounts()
        {
            Dictionary<byte, ulong> byteCounts =
                new Dictionary<byte, ulong>();

            using (BinaryReader reader = new BinaryReader(File.Open(Path, FileMode.Open)))
            {
                long totalbits = reader.BaseStream.Length;

                while (reader.BaseStream.Position < totalbits - Mibibyte)
                {
                    foreach (byte b in reader.ReadBytes(Mibibyte))
                    {
                        if (byteCounts.ContainsKey(b))
                            byteCounts[b]++;

                        else byteCounts.Add(b, 1);
                    }
                }

                foreach (byte b in reader.ReadBytes((int)(totalbits % Mibibyte)))
                {
                    if (byteCounts.ContainsKey(b))
                        byteCounts[b]++;

                    else byteCounts.Add(b, 1);
                }
            }

            return byteCounts;
        }

        public HeaderInfo ParseHeader()
        {
            HeaderInfo header = new HeaderInfo();

            using (FileStream stream = File.Open(Path, FileMode.Open))
            {
                header.TrailingBits = (byte)stream.ReadByte();

                // Header is only valid if the first byte is less than 8.
                if (header.TrailingBits > 7) header.IsValid = false;

                while (header.IsValid)
                {
                    byte b = (byte)stream.ReadByte();
                    if (header.HuffmanTable.ContainsKey(b))
                    {
                        // Header is only valid if the first repeated key byte is
                        // equal to the last key byte.
                        if (b != header.HuffmanTable.Keys.Last())
                            header.IsValid = false;

                        break;
                    }

                    byte bitCount = (byte)stream.ReadByte();

                    bool[] bits = new bool[bitCount];

                    while (bitCount > 0)
                    {
                        byte next = (byte)stream.ReadByte();
                        for (int i = 0; i < 8; i++)
                        {
                            if (bitCount > 0)
                            {
                                bits[bits.Length - bitCount] = 
                                    (next & (1 << (7 - i))) != 0;

                                bitCount--;
                            }

                            else
                            {
                                // Header is only valid if the trailing bits of each
                                // encoded byte are all zeroes.
                                if ((next & (1 << (7 - i))) != 0)
                                {
                                    header.IsValid = false;
                                    break;
                                }
                            }
                        }
                    }

                    header.HuffmanTable.Add(b, bits);
                }

                header.EndPosition = stream.Position;
            }

            return header;
        }
    }
}
