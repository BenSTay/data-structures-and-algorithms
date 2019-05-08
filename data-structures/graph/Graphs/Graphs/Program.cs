using Graphs.Classes;
using System;
using System.Collections.Generic;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>();

            graph.AddNode(1);
            graph.AddNode(2);
            graph.AddNode(3);
            graph.AddNode(4);
            graph.AddNode(5);

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);

            Console.WriteLine(string.Join(", ", graph.GetNodes()));
            Console.WriteLine(string.Join(", ", graph.BreadthFirst(2)));
        }
    }
}
