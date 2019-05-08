using System;
using Xunit;
using Graphs.Classes;
using System.Collections.Generic;

namespace GraphTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanAddNode()
        {
            Graph<int> graph = new Graph<int>();
            graph.AddNode(1);
            Assert.Contains(1, graph.GetNodes());
        }

        [Fact]
        public void CanAddEdge()
        {
            Graph<int> graph = new Graph<int>();
            graph.AddNode(1);
            graph.AddNode(10);
            graph.AddEdge(1, 10);
            Assert.True(graph.GetNeighbors(1).ContainsKey(10));
        }

        [Fact]
        public void CanGetNodes()
        {
            List<int> expected = new List<int> { 1, 2, 3, 4, 5 };
            Graph<int> graph = new Graph<int>();
            foreach (int i in expected)
            {
                graph.AddNode(i);
            }
            Assert.Equal(expected, graph.GetNodes());
        }

        [Fact]
        public void CanGetNeighbors()
        {
            Graph<int> graph = new Graph<int>();
            for (int i = 1; i < 6; i++)
            {
                graph.AddNode(i);
            }
            for (int i = 2; i < 6; i++)
            {
                graph.AddEdge(1, i);
            }
            Assert.Equal(4, graph.GetNeighbors(1).Count);
        }

        [Fact]
        public void CanGetEdgeWeights()
        {
            Graph<int> graph = new Graph<int>();
            graph.AddNode(1);
            graph.AddNode(10);
            graph.AddEdge(1, 10, 100);
            Assert.Equal(100, graph.GetNeighbors(1).GetValueOrDefault(10));
        }

        [Fact]
        public void CanGetSize()
        {
            Graph<int> graph = new Graph<int>();
            for (int i = 1; i < 6; i++)
            {
                graph.AddNode(i);
            }
            Assert.Equal(5, graph.Size());
        }

        [Fact]
        public void CanAddEdgeWithSameNode()
        {
            Graph<int> graph = new Graph<int>();
            graph.AddNode(1);
            graph.AddEdge(1, 1);
            Assert.Single(graph.GetNeighbors(1));
        }

        [Fact]
        public void GetNodesReturnsNullIfEmpty()
        {
            Graph<int> graph = new Graph<int>();
            Assert.Null(graph.GetNodes());
        }

        [Fact]
        public void CanPerformBreadthFirst()
        {
            Graph<int> graph = new Graph<int>();
            for (int i = 1; i < 8; i++)
            {
                graph.AddNode(i);
            }

            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);

            List<int> expected = new List<int> { 1, 2, 4, 5, 3, 7, 6 };
            Assert.Equal(expected, graph.BreadthFirst(1));
        }

        [Fact]
        public void BreadthFirstIgnoresIslands()
        {
            Graph<int> graph = new Graph<int>();
            for (int i = 1; i < 8; i++)
            {
                graph.AddNode(i);
            }

            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(5, 7);

            List<int> expected = new List<int> { 1, 2, 5, 3, 6, 7 };
            Assert.Equal(expected, graph.BreadthFirst(1));
        }

        [Fact]
        public void BreadthFirstOnEmptyGraphReturnsNull()
        {
            Graph<int> graph = new Graph<int>();
            Assert.Null(graph.BreadthFirst(1));
        }
    }
}
