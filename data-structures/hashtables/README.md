# Hash Table
**Author**: Benjamin Taylor

## Description
A **hashtable** is a data structure that is essentially an array of **key/value pairs**.
When adding a new key/value pair to a hashtable, it uses a **hashing algorithm** on the key to determine which index the key/value pair will be stored.

When two keys cause the hashing algorithm to generate the same index, this is called a **collision**.
When a collision occurs, one of two things can happen, depending on the implementation: the second value is added to the **bucket** (a linked list, turning the hashtable into an array of linked lists of key/value pairs) at that index, or the table is resized.

This implementation of a hash table takes the resizing approach. This ensures that the time it takes to retrieve a key/value pair is always O(1), but can result in the hashtable growing quite large in the long run.
This implementation contains the following methods: *Add*, *Get*, *Contains*, *Hash*, *Resize*, & *Print*.

## Methods
| Method | Summary | Big O Time | Big O Space | Example |
| :----: | :-----: | :--------: | :---------: | :-----: |
| Add | Takes in a key and a value, and adds the value to the table at the index of the hashed key. | O(1) | O(1) | ```table.Add("potatoes", 72);```
| Get | Takes in a key and returns the value at the index of the hashed key. | O(1) | O(1) | ```table.Get("potatoes");```
| Contains | Takes in a key and checks if a value exists at the index of the hashed key. | O(1) | O(1) | ```table.Contains("potatoes");```
| Hash | Takes in a key and uses that key to generate a number. | O(1) | O(1) | ```table.Hash("potatoes");```
| Resize | Doubles the size of the table. | O(n) | O(n) | ```table.Resize()```
| Print | Logs the contents of the hashtable to the console for debugging purposes | O(n) | O(1) | ```table.Print()```