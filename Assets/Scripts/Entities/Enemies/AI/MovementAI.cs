using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementAI {

	eTile[,] mapdata;

	private PriorityQueue mapnodes;
	private Node current;
	private Location targetlocation;
	public AIPath aipath;
	public int fpscounter = 0;
    public int fpsreset;
	public Node currentNode;
    public static float lastRepathTime;

	public MovementAI(eTile[,] mapdata) {
        fpsreset = UnityEngine.Random.Range(-2,2)+40;
		this.mapdata = mapdata;
	}

	public AIPath getPath(Vector3 start, Vector3 target) {
		// Set current position
		current = new Node (null, new Location((int)start.x, (int)start.y), 999, NodeState.Open);
		
		// Set target position
		targetlocation = new Location ((int)target.x, (int)target.y);
		
		// Set up map nodes, all floors and the player spawn tile is added to the queue.
		mapnodes = new PriorityQueue ();
		for (int i = 0; i < mapdata.GetLength(0); i++) {
			for (int j = 0; j < mapdata.GetLength(1); j++) {
				if (mapdata[i, j] == eTile.Floor || mapdata[i, j] == eTile.Player) {
					mapnodes.add(new Node(new Location(i, j)));
				}
			}
		}

		// Repeat while we are not at the target location
		while (current != null && targetlocation != null && !current.loc.equals(targetlocation)) {
			checkNeighbors(current);
			
			current.state = NodeState.Closed;
			current = mapnodes.getNextOpen();
		}
		
		// Get the path and spawn balls to indicate the path
		aipath = new AIPath (current);
        if(aipath != null)
            lastRepathTime = Time.time;
		return aipath;
	}

	void checkNeighbors(Node n) {;
		// Loop through all neighbors
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				// Check conditions
				bool isValid = true;
				// If this block is a corner block, check if there are walls right next to it.
				// This prevents cutting corners.
				if (Math.Abs(i * j) == 1) {
					if (mapdata[n.loc.x + i, n.loc.y] == eTile.Wall || 
					    mapdata[n.loc.x, n.loc.y + j] == eTile.Wall) {
						isValid = false;
					} else {
						isValid = true;
					}
				}
				// Ignore center
				if (i == 0 && j == 0) {
					isValid = false;
				}

				if (isValid) {
					// Get valid adjacent node
					Node adjacent = mapnodes.getNodeAt(new Location(n.loc.x + i, n.loc.y + j));
					if (adjacent != null) {
						// Diagonals cost 14, adjacents cost 10
						int gscore = Math.Abs(i * j) * 4 + 10;
						// Heuristic score of distance between target location and this node
						int hscore = (Math.Abs(adjacent.loc.x - targetlocation.x) + Math.Abs(adjacent.loc.y - targetlocation.y)) * 10;
						int fscore = gscore + hscore;
						// If node is neither open nor closed
						if (adjacent.state == NodeState.None) {
							adjacent.parent = n;
							adjacent.state = NodeState.Open;
							adjacent.gscore = n.gscore + gscore;
							adjacent.hscore = hscore;
							adjacent.cost = adjacent.gscore + adjacent.hscore;
							mapnodes.nodes.Remove(adjacent);
							mapnodes.add(adjacent);
						// If node is open
						} else if (adjacent.state == NodeState.Open) {
							if (adjacent.gscore > n.gscore + gscore) {
								adjacent.parent = n;
								adjacent.gscore = n.gscore + gscore;
								adjacent.cost = adjacent.gscore + adjacent.hscore;
								mapnodes.nodes.Remove(adjacent);
								mapnodes.add(adjacent);
							}
						}
					}
				}
			}
		}
	}
}
