# Graph
**Author**: Benjamin Taylor

## Description
A **graph** is a data structure where data is stored in a collection of **nodes**.
Each of these nodes has a **value** and a collection of nodes that can be accessed from that node.
A connection between two nodes in a graph is called an **edge**, and in a **weighted graph** each edge is given a **weight**.

A graph can be represented in two ways: through an **adjacency matrix**, or an **adjacency list**.
In an adjacency matrix, connections between nodes are represented in a 2-dimensional array, where the value at any given position is the weight of the edge between those two nodes (or zero if no edge exists).
In an adjacency list, connections between nodes are represented as a list of lists, where each sub-list represents all of the possible traversals from a specific node.

This implementation includes the following methods: **Add**, **AddRange**, **AddOneWayEdge**, **AddTwoWayEdge**, **GetNodes**, **GetNeighbors**, & **Size** 

### Types of Graphs
- In a **directed graph**, edges can only be traversed in a single direction (you can get to node B from node A, but you can't get to node A from node B).
- In an **undirected graph**, edges can be traversed in either direction.
- In a **complete graph**, all nodes are directly connected to every other node in the graph.
- In a **connected graph**, all nodes are connected to every other node in the graph, but not always directly.
- A **cyclic graph** is a directional graph where traversing through the graph from any given starting node may eventually lead back to itself.
- An **acyclic graph** is a directional graph where traversing through the graph from any given starting node will never lead back to itself.

All of these types of graphs can be created in this implementation.

## Methods
| Method | Summary | Big O Time | Big O Space | Example |
| :----: | :-----: | :--------: | :---------: | :-----: |
| Add | Adds a node to the graph | O(1) | O(1) | ```graph.Add(node)```
| AddRange | Adds a range of nodes to the graph | O(n) | O(n) | ```graph.Add(nodeList)```
| AddOneWayEdge | Creates a 1-way edge between two nodes | O(1) | O(1) | ```graph.AddOneWayEdge(node1, node2)```
| AddTwoWayEdge | Creates a 2-way edge between two nodes | O(1) | O(1) | ```graph.AddTwoWayEdge(node1, node2)```
| GetNodes | Gets all nodes in the graph | O(1) | O(1) | ```graph.GetNodes()```
| GetNeighbors | Gets all neighboring nodes of a given node | O(n) | O(n) | ```graph.GetNeighbors(node)```
| Size | Gets the number of nodes in the graph | O(1) | O(1) | ```graph.Size()```