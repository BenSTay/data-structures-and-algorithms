using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs.Classes
{
    public class Graph<T>
    {
        private List<Node<T>> Nodes { get; set; }

        /// <summary>
        /// Creates an empty graph.
        /// </summary>
        public Graph()
        {
            Nodes = new List<Node<T>>();
        }

        /// <summary>
        /// Creates a graph with a single node.
        /// </summary>
        /// <param name="node">The node being inserted.</param>
        public Graph(Node<T> node)
        {
            Nodes = new List<Node<T>> { node };
        }

        /// <summary>
        /// Creates a graph with a given list of nodes.
        /// </summary>
        /// <param name="nodes">The graph's list of nodes.</param>
        public Graph(List<Node<T>> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Adds a node to the graph.
        /// </summary>
        /// <param name="node">The node being added.</param>
        public void Add(Node<T> node)
        {
            Nodes.Add(node);
        }

        /// <summary>
        /// Adds multiple nodes to the graph.
        /// </summary>
        /// <param name="nodes">The list of nodes being added.</param>
        public void AddRange(List<Node<T>> nodes)
        {
            Nodes.AddRange(nodes);
        }

        /// <summary>
        /// Adds a unidirectional edge to the graph.
        /// </summary>
        /// <param name="n1">The start node.</param>
        /// <param name="n2">The destination node.</param>
        /// <param name="weight">The edge's weight (default = 1).</param>
        public void AddOneWayEdge(Node<T> n1, Node<T> n2, int weight = 1)
        {
            if (!Nodes.Contains(n1) || !Nodes.Contains(n2)) throw new ArgumentException(
                "Both nodes must be in the graph!");

            n1.AddEdge(n2, weight);
        }

        /// <summary>
        /// Adds a bidirectional edge to the graph.
        /// </summary>
        /// <param name="n1">The first node.</param>
        /// <param name="n2">The second node.</param>
        /// <param name="weight">The edge's weight (default = 1).</param>
        public void AddTwoWayEdge(Node<T> n1, Node<T> n2, int weight = 1)
        {
            if (!Nodes.Contains(n1) || !Nodes.Contains(n2)) throw new ArgumentException(
                "Both nodes must be in the graph!");

            if (n1 == n2)
            {
                AddOneWayEdge(n1, n1, weight);
                return;
            }

            n1.AddEdge(n2, weight);
            n2.AddEdge(n1, weight);
        }

        /// <summary>
        /// Gets all nodes in the graph.
        /// </summary>
        /// <returns>A list of all nodes in the graph.</returns>
        public List<Node<T>> GetNodes()
        {
            return Nodes;
        }

        /// <summary>
        /// Gets all neighbor nodes for a given node.
        /// </summary>
        /// <param name="node">The node whose neighbors are being found.</param>
        /// <returns>A list of neighboring nodes.</returns>
        public List<Edge<T>> GetNeighbors(Node<T> node)
        {
            if (!Nodes.Contains(node)) throw new ArgumentException("Node must be in the graph!");

            return node.Edges;
        }

        /// <summary>
        /// Gets the number of nodes in the graph.
        /// </summary>
        /// <returns>The size of the graph as an integer.</returns>
        public int Size()
        {
            return Nodes.Count;
        }
    }
}
