using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTrees.Classes
{
    public class BinarySearchTree<T> : Tree<T> where T : IComparable<T>
    {
        public BinarySearchTree() { }
        public BinarySearchTree(T rootValue)
        {
            Root = new Node<T>(rootValue);
        }

        /// <summary>
        /// Adds a new value to the tree if the value is not already present.
        /// </summary>
        /// <param name="value">The value being added.</param>
        public override void Add(T value)
        {
            if (Root is null) Root = new Node<T>(value);
            else
            {
                Add(value, Root);
            }
        }

        /// <summary>
        /// Adds a new value to the tree if the value is not already present.
        /// </summary>
        /// <param name="value">The value being added.</param>
        /// <param name="node">The node potentially being added to.</param>
        private void Add(T value, Node<T> node)
        {
            if (node.Value.CompareTo(value) > 0)
            {
                if (node.Left is null) node.Left = new Node<T>(value);
                else Add(value, node.Left);
            }
            else if (node.Value.CompareTo(value) < 0)
            {
                if (node.Right is null) node.Right = new Node<T>(value);
                else Add(value, node.Right);
            }
        }

        /// <summary>
        /// Checks if a value exists in the tree.
        /// </summary>
        /// <param name="value">The value being searched for.</param>
        /// <returns>A boolean representing if the value was found.</returns>
        public override bool Contains(T value)
        {
            return Contains(Root, value);
        }

        /// <summary>
        /// Checks if a value exists in the tree.
        /// </summary>
        /// <param name="node">The node whose value is being compared to.</param>
        /// <param name="value">The value being searched for.</param>
        /// <returns>A boolean representing if the value was found.</returns>
        private bool Contains(Node<T> node, T value)
        {
            if (node is null) return false;
            else
            {
                if (node.Value.Equals(value)) return true;
                else if (node.Value.CompareTo(value) > 0)
                {
                    return Contains(node.Left, value);
                }
                else return Contains(node.Right, value);
            }
        }

        public override T GetMaxValue()
        {
            if (Root is null) throw new InvalidOperationException("Cannot get maximum value of an empty tree");
            else
            {
                Node<T> node = Root;
                while (node.Right != null)
                {
                    node = node.Right;
                }
                return node.Value;
            }
        }
    }
}
