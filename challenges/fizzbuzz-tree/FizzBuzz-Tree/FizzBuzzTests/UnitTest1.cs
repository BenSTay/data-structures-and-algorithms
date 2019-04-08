using System;
using Xunit;
using BinaryTrees.Classes;
using System.Collections.Generic;
using FizzBuzz_Tree;

namespace FizzBuzzTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanFizzBuzz()
        {
            Tree<object> tree = new BinaryTree<object>();
            List<int> values = new List<int>()
            {
                2, 4, 6, 8, 10, 13, 15
            };

            foreach (int value in values)
            {
                tree.Add(value);
            }

            tree = Program.FizzBuzzTree(tree);

            List<object> expected = new List<object>()
            {
                2, 4, "Fizz", 8, "Buzz", 13, "FizzBuzz"
            };

            Assert.Equal(expected, tree.BreadthFirst());
        }

        [Fact]
        public void CanNotFizzBuzz()
        {
            Tree<object> tree = new BinaryTree<object>();
            List<object> values = new List<object>()
            {
                2, 4, 8, 13
            };

            foreach (int value in values)
            {
                tree.Add(value);
            }

            tree = Program.FizzBuzzTree(tree);

            Assert.Equal(values, tree.BreadthFirst());
        }

        [Fact]
        public void CantFizzBuzzNull()
        {
            Tree<object> tree = new BinaryTree<object>();

            tree = Program.FizzBuzzTree(tree);

            Assert.Null(tree.Root);
        }
    }
}
