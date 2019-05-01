using System;
using System.Collections.Generic;
using BinaryTrees.Classes;
using Hashtables.Classes;

namespace Tree_Intersection
{
    public class Program
    {
        /// <summary>
        /// Finds the common values of two binary trees.
        /// </summary>
        /// <param name="tree1">The first binary tree.</param>
        /// <param name="tree2">The second binary tree.</param>
        /// <returns>A list containing all matching values.</returns>
        public static List<int> TreeIntersection(Tree<int> tree1, Tree<int> tree2)
        {
            List<int> result = new List<int>();

            // If one or both of the trees are null or empty, no matching values will
            // be found, and therefore an empty list is returned.
            if (tree1 is null || tree2 is null) return result;
            if (tree1.Root is null || tree2.Root is null) return result;

            Hashtable<int> ht1 = new Hashtable<int>();
            Hashtable<int> ht2 = new Hashtable<int>();

            string valStr;
            // Adds values from tree1 to the hashtable
            foreach(int value in tree1.BreadthFirst())
            {
                valStr = value.ToString();
                if (!ht1.Contains(valStr)) ht1.Add(valStr, value);
            }

            // Adds matching values from tree1 to the list
            foreach(int value in tree2.BreadthFirst())
            {
                valStr = value.ToString();
                if (!ht2.Contains(valStr))
                {
                    if (ht1.Contains(valStr)) result.Add(value);
                    ht2.Add(valStr, value);
                }
            }

            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
