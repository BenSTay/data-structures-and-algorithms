using System;
using System.Collections.Generic;
using Graphs.Classes;

namespace Depth_First
{
    public class Program
    {
        /// <summary>
        /// Performs a depth-first traversal on a graph starting at a specified node.
        /// </summary>
        /// <param name="graph">The graph being traversed.</param>
        /// <param name="root">The start node.</param>
        /// <returns>A list of nodes.</returns>
        public static List<int> DepthFirst(Graph<int> graph, int root)
        {
            if (graph is null) return null;
            if (graph.Size() < 1) return null;
            if (graph.GetNeighbors(root) is null) return null;

            Dictionary<int, bool> visited = new Dictionary<int, bool>();
            Stack<int> stack = new Stack<int>();
            List<int> result = new List<int>();

            stack.Push(root);
            while (stack.TryPop(out int node))
            {
                if (!visited.ContainsKey(node))
                {
                    result.Add(node);
                    visited.Add(node, true);

                    if (graph.GetNeighbors(node).Keys.Count < 1) continue;
                    foreach (int neighbor in graph.GetNeighbors(node).Keys)
                    {
                        if (!visited.ContainsKey(neighbor))
                            stack.Push(neighbor);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Sets up a default graph for testing.
        /// </summary>
        /// <returns>A graph</returns>
        public static Graph<int> SetupTest()
        {
            Graph<int> graph = new Graph<int>();
            for(int i = 1; i < 7; i++)
            {
                graph.AddNode(i);
            }

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 6);
            graph.AddEdge(2, 3);
            graph.AddEdge(6, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(3, 4);

            return graph;
        }

        static void Main(string[] args)
        {
            Graph<int> graph = SetupTest();

            Console.WriteLine(string.Join(", ", DepthFirst(graph, 1)));
        }
    }
}
