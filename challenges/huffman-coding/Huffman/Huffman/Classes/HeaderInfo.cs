using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Classes
{
    class HeaderInfo
    {
        public byte TrailingBits { get; set; }
        public Dictionary<byte, bool[]>  HuffmanTable { get; set; }
        public ulong Length { get; set; }

        public HeaderInfo()
        {
            HuffmanTable = new Dictionary<byte, bool[]>();
        }
    }
}
