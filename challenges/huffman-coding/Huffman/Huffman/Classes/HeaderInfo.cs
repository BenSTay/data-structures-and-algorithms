using System.Collections.Generic;

namespace Huffman.Classes
{
    class HeaderInfo
    {
        public byte TrailingBits { get; set; }
        public Dictionary<byte, bool[]> HuffmanTable { get; set; }
        public long EndPosition { get; set; }

        public HeaderInfo()
        {
            HuffmanTable = new Dictionary<byte, bool[]>();
        }
    }
}
