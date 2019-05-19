# Find Tree Depth
A C# algorithm challenge implementation.

## Challenge
Write a function that determines the depth of a given binary tree.

## Approach & Efficiency
This solution uses a recursive, depth first traversal to find the tree's depth. 
Each time the method is run, the root node of the sub-tree is checked if it is null. If it is, the current depth is returned.
If not, the depth of the left and right sub trees are analyzed. The greater of the two depths is then returned.

### Big O
- **Time**: O(n)
  - Because this algorithm has to traverse the entire tree, the time it takes scales linearly with the number of nodes in the tree.
- **Space**: O(h)
  - Because this algorithm uses recursion to solve the problem, the space taken is equal to the height of the call stack.