# Find Maximum Value In Binary Tree
A C# code challenge implementation.

## Challenge
Write a function that finds and returns the maximum value in a binary tree.

## Approach & Efficiency
If the tree is empty, an exception is thrown.
If the tree is a binary search tree, navigate to the right-most node and return its value.
If not, create a variable to hold the largest value and set it to the root node's value. perform a traversal on the tree (which one doesn't particularly matter).
For each node, check if its value is greater than the current largest value. If it is larger, set the largest value to the current node's value.
After the traversal has been completed, return the greatest value.

### Big O
- **Time**: O(n)
  - If the tree is not a binary search tree, all nodes will need to be checked to determine which has the largest value, meaning that the amount of time the algorithm will take will scale linearly with the number of nodes in the tree.
- **Space**: O(w) (breadth-first) or O(h) (depth-first)
  - If a breadth-first traversal is used, the amount of memory used by the algorithm scales with the width of the input tree (which determines the maximum length of the queue) . If a depth-first traversal is used, the amount of memory used by the algorithm scales with the height of the input tree (and therefore the height of the call stack).

## Solution
![Whiteboard](../../assets/findmaximumbinarytree.webp)