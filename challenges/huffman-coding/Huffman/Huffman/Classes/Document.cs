using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Huffman.Classes
{
    class Document
    {
        public byte[] Bytes { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Ext { get; set; }

        public Document(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            Path = path;
            Bytes = File.ReadAllBytes(path);
            Ext = path.Substring(path.LastIndexOf('.'));
            Name = path.Substring(path.LastIndexOf('/') + 1);
            Name = Name.Substring(0, Name.Length - Ext.Length);

        }

        public Dictionary<byte, ulong> GetByteCounts()
        {
            Dictionary<byte, ulong> byteCounts = 
                new Dictionary<byte, ulong>();

            for (int i = 0; i < Bytes.Length; i++)
            {
                if (byteCounts.ContainsKey(Bytes[i]))
                    byteCounts[Bytes[i]]++;

                else byteCounts.Add(Bytes[i], 1);
            }

            return byteCounts;
        }

        public HeaderInfo ParseHeader()
        {
            HeaderInfo header = new HeaderInfo();
            ulong pos = 0;

            header.TrailingBits = Bytes[pos++];
            while (true)
            {
                byte b = Bytes[pos++];
                if (header.HuffmanTable.ContainsKey(b))
                    break;

                byte bitCount = Bytes[pos++];
                bool[] bits = new bool[bitCount];

                while (bitCount > 0)
                {
                    byte next = Bytes[pos++];
                    for (int i = 0; i < 8; i++)
                    {
                        bits[bits.Length - bitCount] = 
                            (next & (1 << (7 - i))) != 0;

                        bitCount--;
                        if (bitCount == 0) break;
                    }
                }

                header.HuffmanTable.Add(b, bits);
            }

            header.Length = pos;

            return header;
        }
    }
}
