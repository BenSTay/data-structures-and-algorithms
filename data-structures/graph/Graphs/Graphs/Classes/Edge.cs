using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs.Classes
{
    public class Edge<T>
    {
        public Node<T> Destination { get; set; }
        public int Weight { get; set; } = 1;

        /// <summary>
        /// Default edge constructor.
        /// </summary>
        /// <param name="destination">The destination Node.</param>
        public Edge(Node<T> destination)
        {
            Destination = destination;
        }

        /// <summary>
        /// Edge constructor with weight input.
        /// </summary>
        /// <param name="destination">The destination Node.</param>
        /// <param name="weight">The edge's weight.</param>
        public Edge(Node<T> destination, int weight)
        {
            Destination = destination;
            Weight = weight;
        }
    }
}
