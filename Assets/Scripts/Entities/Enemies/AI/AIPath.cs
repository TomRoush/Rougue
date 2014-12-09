using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Representation of a path
public class AIPath {

	public List<Node> pathnodes;
	Node startnode;

	public AIPath(Node startNode) {
		this.startnode = startNode;
		pathnodes = new List<Node> ();
		pathnodes.Add (startnode);

		getPath ();
	}

	// Gets the path starting from the given node.
	public List<Node> getPath() {
        if(startnode == null)
            return null; 
		Node n = startnode.parent;
		while (n != null) {
			pathnodes.Add (n);
			n = n.parent;
		}
		pathnodes.Reverse ();
		return pathnodes;
	}

	// Gets the next node at the beginning of this path.
	// The node is removed from the list, and returned.
	public Node pop() {
		if (pathnodes.Count == 0) {
			return null;
		} else {
			Node temp = pathnodes [0];
			pathnodes.RemoveAt (0);
			return temp;
		}
	}

	// Length of the current path.
	public int length() {
		return pathnodes.Count;
	}
}
