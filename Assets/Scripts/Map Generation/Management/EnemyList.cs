using UnityEngine;
using System.Collections.Generic;

public class EnemyList : MonoBehaviour 
{
	public GameObject[] enemies;

	public EnemyList()
	{
		enemies = new GameObject[0];
	}

	public EnemyList(int n)
	{
		enemies = new GameObject[n];
	}

	public EnemyList(EnemyList old)
	{
		EnemyList result = new EnemyList(old.enemies.Length);
		for(int i = 0; i < enemies.Length; i++)
		{
			result.enemies[i] = old.enemies[i];
		}
	}

	public void add(GameObject en)
	{
		GameObject[] temp = new GameObject[enemies.Length+1];
		for(int i = 0; i < enemies.Length; i++)
			temp[i] = enemies[i];
		temp[temp.Length-1] = en;
		enemies = temp;
	}

	public GameObject getEnemy(int i)
	{
		return enemies[i];
	}

	public int length()
	{
		return enemies.Length;
	}
}
