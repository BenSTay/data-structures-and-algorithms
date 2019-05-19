# All Balanced Parentheses Combinations
A C# algorithm challenge implementation.

## Challenge
Given an integer n, generate an array of all possible balanced parentheses combinations for that length.

## Approach & Efficiency
This solution uses a queue to temporarily store each combination of parentheses. At the start, "()" is added to the queue.
To generate each combination, a previously generated combination is dequeued from the queue, and new combinations with an additional pair to the left of the original, to the right of the original, and encapsulating the original are added to the queue (if identical combinations don't already exist in the queue).
This continues until the length of the front item in the queue is exactly 2 * n. Once this is over, a new array of strings with a length equal to the number of strings in the queue is created.
Each string in the queue is then dequeued and added to the array. The array is now returned.

### Big O
- **Time**: O(4<sup>n</sup>)
  - The time taken by this algorithm is roughly equal to the sum of all of the possible combinations up to depth n, or 3<sup>1</sup> + ... + 3<sup>n</sup>, which comes out to around 4<sup>n</sup>.
- **Space**: O(3<sup>n</sup>)
  - The number of combinations for a given number of pairs is equal to 2 * the number of combinations for n - 1 + the sum of all of the previous numbers of combinations. Each of these are stored in the resulting array. This roughly equates to 3<sup>n</sup>.