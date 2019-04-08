using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTrees.Classes
{
    public abstract class Tree<T>
    {
        protected Node<T> Root { get; set; }

        public abstract void Add(T value);

        /// <summary>
        /// Checks if a value exists in the tree.
        /// </summary>
        /// <param name="value">The value being searched for.</param>
        /// <returns>A boolean representing if the tree contains the given value.</returns>
        public virtual bool Contains(T value)
        {
            if (Root is null) return false;
            else
            {
                List<T> values = BreadthFirst();
                return values.Contains(value);
            }
        }

        /// <summary>
        /// Gets all of the values in the tree using an in-order traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        public List<T> InOrder()
        {
            return InOrder(Root);
        }

        /// <summary>
        /// Gets all of the values in the tree using a pre-order traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        public List<T> PreOrder()
        {
            return PreOrder(Root);
        }

        /// <summary>
        /// Gets all of the values in the tree using an post-order traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        public List<T> PostOrder()
        {
            return PostOrder(Root);
        }

        /// <summary>
        /// Gets all of the values in the tree using a breadth-first traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        public List<T> BreadthFirst()
        {
            if (Root is null) return null;
            else
            {
                Queue<Node<T>> breadth = new Queue<Node<T>>();
                breadth.Enqueue(Root);
                List<T> result = new List<T>();

                while (breadth.TryDequeue(out Node<T> node))
                {
                    result.Add(node.Value);
                    if (node.Left != null) breadth.Enqueue(node.Left);
                    if (node.Right != null) breadth.Enqueue(node.Right);
                }
                return result;
            }
        }

        /// <summary>
        /// Gets all of the values in the tree using an in-order traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        private List<T> InOrder(Node<T> node)
        {
            if (node is null) return null;
            else
            {
                List<T> left = InOrder(node.Left);
                List<T> right = InOrder(node.Right);
                List<T> result = new List<T>();

                if (left != null) result.AddRange(left);
                result.Add(node.Value);
                if (right != null) result.AddRange(right);

                return result;
            }
        }

        /// <summary>
        /// Gets all of the values in the tree using a pre-order traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        private List<T> PreOrder(Node<T> node)
        {
            if (node is null) return null;
            else
            {
                List<T> left = PreOrder(node.Left);
                List<T> right = PreOrder(node.Right);
                List<T> result = new List<T>();

                result.Add(node.Value);
                if (left != null) result.AddRange(left);
                if (right != null) result.AddRange(right);

                return result;
            }
        }

        /// <summary>
        /// Gets all of the values in the tree using an post-order traversal.
        /// </summary>
        /// <returns>A list of all of the values in the tree.</returns>
        private List<T> PostOrder(Node<T> node)
        {
            if (node is null) return null;
            else
            {
                List<T> left = PostOrder(node.Left);
                List<T> right = PostOrder(node.Right);
                List<T> result = new List<T>();

                if (left != null) result.AddRange(left);
                if (right != null) result.AddRange(right);
                result.Add(node.Value);

                return result;
            }
        }
    }
}
