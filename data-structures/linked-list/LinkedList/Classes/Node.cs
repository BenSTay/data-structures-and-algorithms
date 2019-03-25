using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList.Classes
{
    public class Node
    {
        public Node Next { get; set; }
        public int Value { get; set; }

        public Node(int value)
        {
            Value = value;
        }
    }
}
