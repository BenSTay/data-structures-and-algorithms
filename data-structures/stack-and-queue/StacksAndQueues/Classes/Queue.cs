using System;
using System.Collections.Generic;
using System.Text;

namespace StacksAndQueues.Classes
{
    public class Queue<T>
    {
        public Node<T> Front { get; set; }
        public Node<T> Rear { get; set; }

        /// <summary>
        /// Adds a new node to the back of the queue.
        /// </summary>
        /// <param name="value">The new nodes value.</param>
        public void Enqueue(T value)
        {
            Node<T> node = new Node<T>(value);
            if (Rear != null) Rear.Next = node;
            Rear = node;
            if (Front is null) Front = Rear;
        }

        /// <summary>
        /// Removes a node from the front of the queue.
        /// </summary>
        /// <returns>The removed nodes value.</returns>
        public T Dequeue()
        {
            if (Front is null) throw new InvalidOperationException("Cannot dequeue from an empty queue!");
            else
            {
                Node<T> node = Front;
                Front = Front.Next;
                return node.Value;
            }
        }

        /// <summary>
        /// Retrieves the value of the node at the front of the queue.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (Front is null) throw new InvalidOperationException("Cannot peek on an empty queue!");
            else return Front.Value;
        }
    }
}
