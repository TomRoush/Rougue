using UnityEngine;
using System.Collections;

public class Location {

	public int x;
	public int y;

	public Location(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public bool equals(Location l) {
		return l.x == x && l.y == y;
	}
}
