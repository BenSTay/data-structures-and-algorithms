using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs.Classes
{
    public class Node<T>
    {
        public T Value { get; set; }
        public List<Edge<T>> Edges { get; set; }
        public bool Visited { get; set; }
        
        /// <summary>
        /// Constructs a Node with a given value.
        /// </summary>
        /// <param name="value">The Node's value.</param>
        public Node(T value)
        {
            Edges = new List<Edge<T>>();
            Value = value;
        }

        /// <summary>
        /// Adds an Edge with the given weight.
        /// </summary>
        /// <param name="destination">The Edge's destination.</param>
        /// <param name="weight">The Edge's weight.</param>
        public void AddEdge(Node<T> destination, int weight)
        {
            if (GetNeighbors().Contains(destination)) throw new ArgumentException("Edge already exists!");
            Edge<T> edge = new Edge<T>(destination, weight);
            Edges.Add(edge);
        }

        public bool HasConnection(Node<T> node)
        {
            foreach(Edge<T> edge in Edges)
            {
                if (edge.Destination == node) return true;
            }
            return false;
        }

        public List<Node<T>> GetNeighbors()
        {
            List<Node<T>> neighbors = new List<Node<T>>();
            foreach (Edge<T> edge in Edges)
            {
                neighbors.Add(edge.Destination);
            }
            return neighbors;
        }
    }
}
