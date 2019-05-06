using Graphs.Classes;
using System;
using System.Collections.Generic;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node<int>> nodes = new List<Node<int>>
            {
                new Node<int>(3),
                new Node<int>(5),
                new Node<int>(7),
                new Node<int>(11)
            };

            Graph<int> graph = new Graph<int>(nodes);

            graph.AddTwoWayEdge(nodes[0], nodes[1]);
            graph.AddTwoWayEdge(nodes[2], nodes[3]);
            graph.AddTwoWayEdge(nodes[0], nodes[3]);

            List<Node<int>> result = graph.BreadthFirst(nodes[0]);
        }
    }
}
