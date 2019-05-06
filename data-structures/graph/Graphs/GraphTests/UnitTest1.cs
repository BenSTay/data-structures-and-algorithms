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

            Node<int> node = new Node<int>(10);

            graph.Add(node);

            Assert.Contains(node, graph.GetNodes());
        }

        [Fact]
        public void CanAddEdge()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node1 = new Node<int>(10);
            Node<int> node2 = new Node<int>(25);

            graph.Add(node1);
            graph.Add(node2);

            graph.AddTwoWayEdge(node1, node2);

            Assert.True(node1.HasConnection(node2));
        }

        [Fact]
        public void CanGetAllNodes()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node1 = new Node<int>(10);
            Node<int> node2 = new Node<int>(25);

            graph.Add(node1);
            graph.Add(node2);

            List<Node<int>> result = graph.GetNodes();

            Assert.True(result.Contains(node1) && result.Contains(node2));
        }

        [Fact]
        public void CanGetNeighbors()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node1 = new Node<int>(10);
            Node<int> node2 = new Node<int>(25);
            Node<int> node3 = new Node<int>(33);

            graph.Add(node1);
            graph.Add(node2);
            graph.Add(node3);

            graph.AddOneWayEdge(node1, node2);
            graph.AddTwoWayEdge(node3, node1);

            List<Edge<int>> result = graph.GetNeighbors(node1);
            List<Node<int>> neighbors = new List<Node<int>>();
            foreach (Edge<int> edge in result)
            {
                neighbors.Add(edge.Destination);
            }

            Assert.True(neighbors.Contains(node2) && neighbors.Contains(node3));
            
        }

        [Fact]
        public void CanGetNeighborsWithWeights()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node1 = new Node<int>(10);
            Node<int> node2 = new Node<int>(25);
            Node<int> node3 = new Node<int>(33);

            graph.Add(node1);
            graph.Add(node2);
            graph.Add(node3);

            graph.AddOneWayEdge(node1, node2, 2);
            graph.AddTwoWayEdge(node3, node1, 5);

            List<Edge<int>> result = graph.GetNeighbors(node1);
            List<Node<int>> neighbors = new List<Node<int>>();
            List<int> weights = new List<int>();
            foreach (Edge<int> edge in result)
            {
                neighbors.Add(edge.Destination);
                weights.Add(edge.Weight);
            }

            Assert.True(neighbors.Contains(node2)
                && neighbors.Contains(node3)
                && weights.Contains(2)
                && weights.Contains(5));
        }

        [Fact]
        public void CanGetSize()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node1 = new Node<int>(10);
            Node<int> node2 = new Node<int>(25);
            Node<int> node3 = new Node<int>(33);

            graph.Add(node1);
            graph.Add(node2);
            graph.Add(node3);

            Assert.Equal(3, graph.Size());
        }

        [Fact]
        public void NodeCanPointToItself()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node1 = new Node<int>(10);

            graph.Add(node1);

            graph.AddOneWayEdge(node1, node1);

            Assert.True(graph.Size() == 1
                && graph.GetNeighbors(node1).Count == 1);
        }

        [Fact]
        public void EmptyGraphIsEmpty()
        {
            Graph<int> graph = new Graph<int>();

            Assert.Equal(0, graph.Size());
        }

        [Fact]
        public void CanPerformBreadthFirst()
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
            bool containsAll = true;

            foreach (Node<int> node in nodes)
            {
                containsAll = result.Contains(node);
                if (!containsAll) break;
            }

            Assert.True(containsAll);
        }

        [Fact]
        public void BreadthFirstIgnoresIslands()
        {
            List<Node<int>> nodes = new List<Node<int>>
            {
                new Node<int>(3),
                new Node<int>(5),
                new Node<int>(7),
                new Node<int>(11)
            };

            Graph<int> graph = new Graph<int>(nodes);

            graph.AddOneWayEdge(nodes[0], nodes[1]);
            graph.AddOneWayEdge(nodes[0], nodes[3]);

            List<Node<int>> result = graph.BreadthFirst(nodes[0]);

            Assert.DoesNotContain(nodes[2], result);
        }

        [Fact]
        public void BreadthFirstOnEmptyGraphReturnsEmptyList()
        {
            Graph<int> graph = new Graph<int>();

            Node<int> node = new Node<int>(2);

            List<Node<int>> result = graph.BreadthFirst(node);

            Assert.Empty(result);
        }
    }
}
