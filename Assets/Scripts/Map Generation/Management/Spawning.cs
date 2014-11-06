using UnityEngine;
using System.Collections;

public class Spawning : MakeMap
{
	public static void SpawnEnemies(TileMapData map, int numEnemies, GameObject Enemy)
	{
		int countEnemies = 0;
		while(countEnemies < numEnemies)
		{
			int x = Random.Range (0,map.mapData.GetLength (0)), y = Random.Range (0,map.mapData.GetLength (1));
			Enemy e = new Enemy(x,y);
			Vector3 tilePos = new Vector3(x, y, 0);
			if(map.GetTileAt(x,y).Equals(eTile.Floor))
			{
				Instantiate (Enemy, tilePos, Quaternion.identity);
				countEnemies++;
				//map.enemies.add(Enemy);
			}
		}
	}

	public static void RespawnEnemies(TileMapData map, EnemyList enemies)
	{
		for(int i =0; i<enemies.length(); i++)
		{

		}
	}
}
