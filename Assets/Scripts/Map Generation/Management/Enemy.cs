using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public GameObject enemy;
	public Vector3 v;

	public Enemy(GameObject en, Vector3 vv)
	{
		enemy = en;
		v = vv;
	}

	public GameObject getGameObj()
	{
		return enemy;
	}

	public Vector3 getV()
	{
		return enemy.transform.position;
	}
}
