using System;
using BinaryTrees.Classes;

namespace Find_Tree_Depth
{
    public class Program
    {
        public static int GetDepth(Tree<object> tree)
        {
            if (tree is null) throw new ArgumentNullException();
            return GetDepth(tree.Root, 0);
        }

        private static int GetDepth(Node<object> root, int depth)
        {
            if (root is null) return depth;

            int leftDepth = GetDepth(root.Left, depth + 1);
            int rightDepth = GetDepth(root.Right, depth + 1);

            return leftDepth > rightDepth ? leftDepth : rightDepth;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
