using System;
using System.Collections.Generic;
using BinaryTrees.Classes;

namespace BinaryTrees
{
    class Program
    {
        static void Main(string[] args)
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
        }
    }
}
