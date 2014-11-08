using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Implementation of a priority queue, with nodes ordered with increasing cost.
public class PriorityQueue {

	public List<Node> nodes;

	public PriorityQueue() {
		nodes = new List<Node> ();
	}

	// Add a node to this priority queue.
	// nodes is assumed to be already ordered with increasing cost.
	public void add(Node n) {
		if (nodes.Count == 0) {
			nodes.Add (n);
		} else {
			bool success = false;
			for (int i = nodes.Count - 1; i >= 0; i--) {
				if (n.cost >= nodes[i].cost && !success) {
					nodes.Insert(i + 1, n);
					success = true;
				}
			}
			if (!success) {
				nodes.Insert(0, n);
			}
		}
	}

	// Gets the next open node in the queue.
	public Node getNextOpen() {
		for (int i = 0; i < nodes.Count; i++) {
			if (nodes[i].state == NodeState.Open) {
				return nodes[i];
			}
		}
		return null;
	}

	// Gets the node at the given position.
	// Returns null if none are found
	public Node getNodeAt(Location loc) {
		for (int i = 0; i < nodes.Count; i++) {
			if (nodes[i].loc.equals(loc)) return nodes[i];
		}
		return null;
	}
}
