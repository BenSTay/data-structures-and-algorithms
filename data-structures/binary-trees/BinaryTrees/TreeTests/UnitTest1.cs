using System;
using Xunit;
using BinaryTrees.Classes;
using System.Collections.Generic;

namespace TreeTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanMakeEmptyTree()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.Null(tree.BreadthFirst());
        }

        [Fact]
        public void CanMakeTreeWithRoot()
        {
            BinaryTree<int> tree = new BinaryTree<int>(256);
            Assert.NotEmpty(tree.BreadthFirst());
        }

        [Fact]
        public void CanAddLeftAndRightToRoot()
        {
            BinaryTree<int> tree = new BinaryTree<int>(1);
            List<int> expected = new List<int>()
            {
                2, 1, 3
            };
            tree.Add(2);
            tree.Add(3);
            Assert.Equal(expected, tree.InOrder());
        }

        [Fact]
        public void CanGetPreOrder()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            List<int> expected = new List<int>()
            {
                1, 2, 4, 5, 3, 6, 7
            };
            for (int i = 1; i < 8; i++)
            {
                tree.Add(i);
            }
            List<int> result = tree.PreOrder();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanGetInOrder()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            List<int> expected = new List<int>()
            {
                4, 2, 5, 1, 6, 3, 7
            };
            for (int i = 1; i < 8; i++)
            {
                tree.Add(i);
            }
            List<int> result = tree.InOrder();
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CanGetPostOrder()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            List<int> expected = new List<int>()
            {
                4, 5, 2, 6, 7, 3, 1
            };
            for (int i = 1; i < 8; i++)
            {
                tree.Add(i);
            }
            List<int> result = tree.PostOrder();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanGetBreadthFirst()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            List<int> expected = new List<int>();
            for (int i = 1; i < 8; i++)
            {
                expected.Add(i);
                tree.Add(i);
            }
            List<int> result = tree.BreadthFirst();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BreadthFirstOnEmptyTreeReturnsNull()
        {
            Tree<int> tree = new BinarySearchTree<int>();
            Assert.Null(tree.BreadthFirst());
        }

        [Fact]
        public void BreadthFirstWorksOnTreeWithNoBranches()
        {
            Tree<int> tree = new BinaryTree<int>(0);
            Node<int> node = tree.Root;
            List<int> expected = new List<int>() { 0 };
            Random rng = new Random();
            for (int i = 1; i < 10; i++)
            {
                expected.Add(i);
                if (rng.Next(2) == 0)
                {
                    node.Left = new Node<int>(i);
                    node = node.Left;
                }
                else
                {
                    node.Right = new Node<int>(i);
                    node = node.Right;
                }
            }
            Assert.Equal(expected, tree.BreadthFirst());
        }

        [Fact]
        public void BinarySearchTreeSortsProperly()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            List<int> expected = new List<int>()
            {
                4, 2, 6, 1, 3, 5, 7
            };
            foreach (int value in expected)
            {
                tree.Add(value);
            }
            Assert.Equal(expected, tree.BreadthFirst());
        }

        [Fact]
        public void BinarySearchTreeCanFindValue()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            List<int> expected = new List<int>()
            {
                4, 2, 6, 1, 3, 5, 7
            };
            foreach (int value in expected)
            {
                tree.Add(value);
            }
            Assert.True(tree.Contains(3));
        }
    }
}
