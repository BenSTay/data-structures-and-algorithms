using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTrees.Classes
{
    public class BinaryTree<T> : Tree<T>
    {
        public BinaryTree() { }
        public BinaryTree(T rootValue)
        {
            Root = new Node<T>(rootValue);
        }

        public override void Add(T value)
        {
            Node<T> node = new Node<T>(value);
            if (Root is null) Root = node;
            else
            {
                Queue<Node<T>> breadth = new Queue<Node<T>>();
                breadth.Enqueue(Root);
                while (breadth.TryDequeue(out Node<T> treeNode))
                {
                    if (treeNode.Left == null)
                    {
                        treeNode.Left = node;
                        break;
                    }
                    else if (treeNode.Right == null)
                    {
                        treeNode.Right = node;
                        break;
                    }
                    else
                    {
                        breadth.Enqueue(treeNode.Left);
                        breadth.Enqueue(treeNode.Right);
                    }
                }
            }
        }
    }
}
