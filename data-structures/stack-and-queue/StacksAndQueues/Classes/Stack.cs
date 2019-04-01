using System;
using System.Collections.Generic;
using System.Text;

namespace StacksAndQueues.Classes
{
    public class Stack<T>
    {
        public Node<T> Top { get; set; }

        /// <summary>
        /// Places a new node at the top of the stack.
        /// </summary>
        /// <param name="value">The new nodes value.</param>
        public void Push(T value)
        {
            Node<T> node = new Node<T>(value);
            node.Next = Top;
            Top = node;
        }

        /// <summary>
        /// Removes a node from the top of the stack.
        /// </summary>
        /// <returns>The removed nodes value.</returns>
        public T Pop()
        {
            if (Top is null) throw new InvalidOperationException("Cannot pop from an empty stack!");
            else
            {
                Node<T> node = Top;
                Top = Top.Next;
                return node.Value;
            }
        }

        /// <summary>
        /// Retrieves the value of the node at the top of the stack.
        /// </summary>
        /// <returns>The top nodes value.</returns>
        public T Peek()
        {
            if (Top is null) throw new InvalidOperationException("Cannot peek on an empty stack!");
            else return Top.Value;
        }
    }
}
