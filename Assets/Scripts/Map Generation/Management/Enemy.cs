using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public GameObject enemy;
	public int x, y;

	public Enemy(int xx, int yy)
	{
		x = xx;
		y = yy;
	}
}
