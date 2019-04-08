# Binary Trees
**Author**: Benjamin Taylor

## Description
A C# implementation of a **binary tree**, including a **binary search tree**.
A binary tree is a data structure where each **node** in the tree has a one-way connection to up to two other nodes, a **left** node and a **right** node.

In a binary tree, nodes are **add**ed at the first available position in the tree.
In a binary search tree, nodes with values that are greater than existing nodes are placed to the right, and nodes with lesser values are placed to the left.
This means that a binary search tree is always sorted, allowing for quicker searches than a regular binary tree.

This implementation includes four different tree traversal methods: **InOrder**, **PreOrder**, **PostOrder**, and **BreadthFirst**.
The other methods in this implementation are **Add** and **Contains**.

## Methods
### Shared
| Method | Summary | Big O Time | Big O Space | Example |
| :----: | :-----: | :--------: | :---------: | :-----: |
| InOrder | A depth-first traversal where values are inserted in between the left and right nodes values. | O(n) | O(h) | tree.InOrder()
| PreOrder | A depth-first traversal where values are placed before the left and right nodes values. | O(n) | O(h) | tree.PreOrder()
| PostOrder | A depth-first traversal where values are placed after the left and right nodes values. | O(n) | O(h) | tree.PostOrder()
| BreadthFirst | A breadth-first traversal, where values are ordered from top to bottom, left to right. | O(n) | O(w) | tree.BreadthFirst()

### Binary Tree
| Method | Summary | Big O Time | Big O Space | Example |
| :----: | :-----: | :--------: | :---------: | :-----: |
| Add | Adds a new node in the first available spot in the tree (breadth-first) | O(n) | O(w) | binaryTree.Add(17)
| Contains | Checks if a value exists in the tree using a breath-first traversal | O(n) | O(w) | binaryTree.Contains(71)

### Binary Search Tree
| Method | Summary | Big O Time | Big O Space | Example |
| :----: | :-----: | :--------: | :---------: | :-----: |
| Add | Adds a new node to the tree (if the value isn't already present in the tree) in the appropriate position | O(h) | O(h) | searchTree.Add(71)
| Contains | Checks if a value exists in the tree using a depth-first traversal. | O(h) | O(h) | searchTree.Contains(17)
