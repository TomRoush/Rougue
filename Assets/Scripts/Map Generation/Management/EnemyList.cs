using UnityEngine;
using System.Collections.Generic;

public class EnemyList : MonoBehaviour 
{
	public AttackinMob[] enemies;

	public EnemyList()
	{
		enemies = new AttackingMob[0];
	}

	public EnemyList(int n)
	{
		enemies = new AttackingMob[n];
	}

	public EnemyList(EnemyList old)
	{
		AttackingMobList result = new AttackingMobList(old.enemies.Length);
		for(int i = 0; i < enemies.Length; i++)
		{;;
			result.enemies[i] = old.enemies[i];
		}
	}

	public void add(AttackingMob en)
	{
		AttackingMob[] temp = new AttackingMob[enemies.Length+1];
		for(int i = 0; i < enemies.Length; i++)
			temp[i] = enemies[i];
		temp[temp.Length-1] = new AttackingMob(en.getGameObj(), en.getV());
		enemies = temp;
	}

	public AttackinMob getAttackinMob(int i)
	{
		return enemies[i];
	}

	public int length()
	{
		return enemies.Length;
	}
}
