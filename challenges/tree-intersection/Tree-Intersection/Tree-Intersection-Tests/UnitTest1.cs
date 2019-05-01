using System;
using Xunit;
using Tree_Intersection;
using BinaryTrees.Classes;
using System.Collections.Generic;

namespace Tree_Intersection_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanFindMatchingValues()
        {
            Tree<int> tree1 = new BinaryTree<int>();
            Tree<int> tree2 = new BinarySearchTree<int>();

            int[] tree1Values = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            int[] tree2Values = new int[] { 8, 4, 12, 2, 6, 10, 14 };

            for (int i = 0; i < 7; i++)
            {
                tree1.Add(tree1Values[i]);
                tree2.Add(tree2Values[i]);
            }

            List<int> expected = new List<int>();
            expected.AddRange(new int[] { 4, 2, 6 });

            List<int> result = Program.TreeIntersection(tree1, tree2);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NoMatchingValuesResultsInEmptyList()
        {
            Tree<int> tree1 = new BinaryTree<int>();
            Tree<int> tree2 = new BinaryTree<int>();

            int[] tree1Values = new int[] { 1, 3, 5, 7, 9, 11, 13 };
            int[] tree2Values = new int[] { 0, 2, 4, 6, 8, 10, 12 };

            for (int i = 0; i < 7; i++)
            {
                tree1.Add(tree1Values[i]);
                tree2.Add(tree2Values[i]);
            }

            List<int> result = Program.TreeIntersection(tree1, tree2);

            Assert.Empty(result);
        }

        [Fact]
        public void EmptyOrNullReturnsEmptyList()
        {
            Tree<int> tree1 = new BinaryTree<int>();

            List<int> result = Program.TreeIntersection(tree1, null);

            Assert.Empty(result);
        }
    }
}
