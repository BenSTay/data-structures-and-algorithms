using System;
using Xunit;
using Find_Tree_Depth;
using BinaryTrees.Classes;
using System.Collections.Generic;

namespace FindTreeDepthTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanGetDepth()
        {
            Tree<object> tree = new BinaryTree<object>(0);

            Queue<Node<object>> breadth = new Queue<Node<object>>();
            breadth.Enqueue(tree.Root);

            int i = 0;
            while (i < 14)
            {
                Node<object> node = breadth.Dequeue();

                node.Left = new Node<object>(i++);
                node.Right = new Node<object>(i++);

                breadth.Enqueue(node.Left);
                breadth.Enqueue(node.Right);
            }

            int result = Program.GetDepth(tree);

            Assert.Equal(4, result);
        }

        [Fact]
        public void EmptyTreeHasDepthOfZero()
        {
            Tree<object> tree = new BinaryTree<object>();

            int result = Program.GetDepth(tree);

            Assert.Equal(0, result);
        }

        [Fact]
        public void NullTreeThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Program.GetDepth(null));
        }
    }
}
