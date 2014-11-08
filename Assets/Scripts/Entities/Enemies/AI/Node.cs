using UnityEngine;
using System.Collections;

public enum NodeState {
	Open, Closed, None,
}
// A node represents a location on the map.
public class Node {

	public int cost;
	public int gscore;
	public int hscore;
	public Node parent;
	public Location loc;
	public NodeState state;

	public Node(Location l) : this(null, l, -1, NodeState.None) {

	}
	public Node(Node parent, Location l, int cost, NodeState state) {
		this.cost = cost;
		this.parent = parent;
		this.state = state;
		loc = l;
	}
}
