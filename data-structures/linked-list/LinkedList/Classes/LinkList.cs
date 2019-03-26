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
            Node newNode = new Node(value) { Next = Head };
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

        /// <summary>
        /// Adds a new node at the end of the list.
        /// </summary>
        /// <param name="value">The value of the node being added.</param>
        public void Append(int value)
        {
            Node newnode = new Node(value);
            if (Head is null) Head = newnode;
            else
            {
                Current = Head;
                while (Current.Next != null)
                {
                    Current = Current.Next;
                }
                Current.Next = newnode;
            }
        }

        /// <summary>
        /// Adds a new node before a node in the list with a given target value.
        /// </summary>
        /// <param name="target">The value of the node that the new node will be placed before.</param>
        /// <param name="value">The value of the node being added.</param>
        public void InsertBefore(int target, int value)
        {
            if (Head != null)
            {
                if (target == Head.Value) Insert(value);
                else
                {
                    Current = Head;
                    while (Current.Next != null)
                    {
                        if (Current.Next.Value == target)
                        {
                            Node newnode = new Node(value) { Next = Current.Next };
                            Current.Next = newnode;
                            break;
                        }
                        Current = Current.Next;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a new node after a node in the list with a given target value.
        /// </summary>
        /// <param name="target">The value of the node that the new node will be placed after.</param>
        /// <param name="value">The value of the node being added.</param>
        public void InsertAfter(int target, int value)
        {
            Current = Head;
            while (Current != null)
            {
                if (Current.Value == target)
                {
                    Node newnode = new Node(value) { Next = Current.Next };
                    Current.Next = newnode;
                    break;
                }
                Current = Current.Next;
            }
        }

        /// <summary>
        /// Gets the value of the node that is k nodes away from the end of the list.
        /// </summary>
        /// <param name="k">The distance from the end of the list.</param>
        /// <returns>The value of the node.</returns>
        public int KthFromEnd(int k)
        {
            if (k < 0) throw new ArgumentOutOfRangeException("k cannot be negative");
            int counter = 0;
            Current = Head;

            while (Current.Next != null)
            {
                counter++;
                Current = Current.Next;
            }

            if (k > counter) throw new ArgumentOutOfRangeException("k cannot be greater than the lists length");
            Current = Head;

            while (k < counter)
            {
                counter--;
                Current = Current.Next;
            }
            return Current.Value;
        }
    }
}
