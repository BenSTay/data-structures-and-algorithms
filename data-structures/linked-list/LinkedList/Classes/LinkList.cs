using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList.Classes
{
    public class LinkList
    {
        public Node Head { get; set; }
        public Node Current { get; set; }

        /// <summary>
        /// Adds a new node at the front of the list.
        /// </summary>
        /// <param name="value">The node's value.</param>
        public void Insert(int value)
        {
            Node newNode = new Node(value);
            newNode.Next = Head;
            Head = newNode;
        }

        /// <summary>
        /// Determines if a value is present in the list.
        /// </summary>
        /// <param name="value">The value being searched for.</param>
        /// <returns>true if the value is found, else false.</returns>
        public bool Includes(int value)
        {
            Current = Head;
            while (Current != null)
            {
                if (Current.Value == value) return true;
                else Current = Current.Next;
            }
            return false;
        }

        /// <summary>
        /// Prints the list to a string.
        /// </summary>
        /// <returns>The list represented as a string.</returns>
        public string Print()
        {
            StringBuilder builder = new StringBuilder();
            Current = Head;
            while (Current != null)
            {
                builder.Append(Current.Value);
                if (Current.Next != null) builder.Append(" -> ");
                Current = Current.Next;
            }
            return builder.ToString();
        }
    }
}
