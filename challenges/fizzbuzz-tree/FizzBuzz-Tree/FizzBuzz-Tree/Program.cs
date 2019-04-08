using System;
using System.Collections.Generic;
using BinaryTrees.Classes;

namespace FizzBuzz_Tree
{
    public class Program
    {
        /// <summary>
        /// Performs fizzbuzz on a tree.
        /// </summary>
        /// <param name="tree">The tree being modified.</param>
        /// <returns>The modified tree.</returns>
        public static Tree<object> FizzBuzzTree(Tree<object> tree)
        {
            Node<object> node = tree.Root;
            if (node != null)
            {
                Queue<Node<object>> breadth = new Queue<Node<object>>();
                breadth.Enqueue(node);
                while (breadth.TryDequeue(out node))
                {
                    if (node.Left != null) breadth.Enqueue(node.Left);
                    if (node.Right != null) breadth.Enqueue(node.Right);

                    if (node.Value is int)
                    {
                        int value = (int)node.Value;
                        string fizzbuzz = $"{(value % 3 == 0 ? "Fizz" : "")}{(value % 5 == 0 ? "Buzz": "")}";
                        if (fizzbuzz != "") node.Value = fizzbuzz;
                    }
                }
            }
            return tree;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
