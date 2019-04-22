# Insertion Sort
A C# sorting algorithm implementation

## Challenge
Sort an array in place using the Insertion Sort algorithm.

## Approach & Efficiency
This implementation uses two, nested for loops to perform the insertion sort.
The outer loop increments through each index of the array, and assigns to a variable the value of the element at that index.
The inner loop then decrements through each index in the array, starting at the index before the position of the outer loop.
Each iteration, the value at that index is compared to the value of the next index. If its value is greater than the value at the next index, they are swapped.
This loop either runs until the start of the array is reached, or the comparison evaluates as false, in which case the loop is broken out of.

### Big O
- **Time**: O(n<sup>2</sup>)
  - For each index being evaluated, potentially every index that preceeds that index will also need to be checked to perform the insertion. This results in computation time that scales in a roughly quadratic fashion.
- **Space**: O(1)
  - Because the sort is being performed in place, the amount of new memory created to perform the sort is constant.