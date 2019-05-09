using System;
using Xunit;
using Depth_First;
using Graphs.Classes;
using System.Collections.Generic;

namespace DepthFirstTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanPerformDepthFirst()
        {
            Graph<int> graph = Program.SetupTest();
            List<int> expected = new List<int> { 1, 5, 6, 3, 4, 2 };
            Assert.Equal(expected, Program.DepthFirst(graph, 1));
        }

        [Fact]
        public void DepthFirstOnEmptyGraphReturnsNull()
        {
            Graph<int> graph = new Graph<int>();
            Assert.Null(Program.DepthFirst(graph, 1));
        }

        [Fact]
        public void DepthFirstWithNodeNotInGraphReturnsNull()
        {
            Graph<int> graph = Program.SetupTest();
            Assert.Null(Program.DepthFirst(graph, 0));
        }
    }
}
