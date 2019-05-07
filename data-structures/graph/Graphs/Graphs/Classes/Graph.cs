using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs.Classes
{
    public class Graph<T>
    {
        readonly Dictionary<T, Dictionary<T, int>> Nodes;

        /// <summary>
        /// Creates an empty graph.
        /// </summary>
        public Graph()
        {
            Nodes = new Dictionary<T, Dictionary<T, int>>();
        }

        /// <summary>
        /// Adds a node to the graph.
        /// </summary>
        /// <param name="value">The node's value.</param>
        /// <returns>A boolean representing whether the operation was successful.</returns>
        public bool AddNode(T value)
        {
            if (Nodes.ContainsKey(value)) return false;

            Nodes.Add(value, new Dictionary<T, int>());
            return true;
        }

        /// <summary>
        /// Connects two nodes in the graph.
        /// </summary>
        /// <param name="v1">The value of the first node being connected.</param>
        /// <param name="v2">The value of the second node being connected.</param>
        /// <param name="weight">Sets the weight of the edge (Default: 1)</param>
        /// <param name="oneWay">Determines if the edge can only be traversed from a single direction (Default: false)</param>
        /// <returns>A boolean representing if the operation was successful.</returns>
        public bool AddEdge(T v1, T v2, int weight = 1, bool oneWay = false)
        {
            if (!Nodes.ContainsKey(v1) || !Nodes.ContainsKey(v2)) return false;

            Dictionary<T, int> n1 = Nodes.GetValueOrDefault(v1);
            Dictionary<T, int> n2 = Nodes.GetValueOrDefault(v2);

            if (n1.ContainsKey(v2)) return false;

            if (!oneWay && !v1.Equals(v2))
            {
                if (n2.ContainsKey(v1)) return false;
                n2.Add(v1, weight);
            }

            n1.Add(v2, weight);
            return true;
        }

        /// <summary>
        /// Gets all of the node values in the graph.
        /// </summary>
        /// <returns>A list of node values.</returns>
        public List<T> GetNodes()
        {
            if (Nodes.Keys.Count < 1) return null;
            return Nodes.Keys.ToList();
        }

        /// <summary>
        /// Gets all nodes neighboring a given node.
        /// </summary>
        /// <param name="value">The value of the node being checked.</param>
        /// <returns>A list of node values and edge weights.</returns>
        public List<(T, int)> GetNeighbors(T value)
        {
            if (!Nodes.ContainsKey(value)) return null;

            Dictionary<T, int> node = Nodes.GetValueOrDefault(value);
            List<(T, int)> neighbors = new List<(T, int)>();

            foreach (T key in node.Keys)
            {
                neighbors.Add((key, node.GetValueOrDefault(key)));
            }

            return neighbors;
        }

        /// <summary>
        /// Gets the number of nodes in the graph.
        /// </summary>
        /// <returns>An integer representing the size of the graph.</returns>
        public int Size()
        {
            return Nodes.Keys.Count;
        }

        /// <summary>
        /// Performs a breadth-first traversal of the graph.
        /// </summary>
        /// <param name="value">The value of the root node for the traversal.</param>
        /// <returns>A list of node values.</returns>
        public List<T> BreadthFirst(T value)
        {
            if (!Nodes.ContainsKey(value)) return null;

            Dictionary<T, bool> visited = new Dictionary<T, bool>();
            visited.Add(value, true);

            List<T> output = new List<T> { value };
            Queue<List<T>> breadth = new Queue<List<T>>();

            breadth.Enqueue(
                Nodes.GetValueOrDefault(value).Keys.ToList()
            );

            while (breadth.TryDequeue(out List<T> neighbors))
            {
                foreach (T node in neighbors)
                {
                    if (!visited.ContainsKey(node))
                    {
                        visited.Add(node, true);
                        output.Add(node);
                        breadth.Enqueue(
                            Nodes.GetValueOrDefault(node).Keys.ToList()    
                        );
                    }
                }
            }

            return output;
        }
    }
}
