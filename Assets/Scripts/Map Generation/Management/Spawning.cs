using UnityEngine;
using System.Collections;

public class Spawning : MakeMap
{
	public static void SpawnEnemies(TileMapData map, int numEnemies, GameObject Enemy, GameObject Player)
	{
		int countEnemies = 0;
		int buffer = 5;
		while(countEnemies < numEnemies)
		{
			int x = Random.Range (0,map.mapData.GetLength (0)), y = Random.Range (0,map.mapData.GetLength (1));
			Vector3 tilePos = new Vector3(x, y, 0);
			if(map.GetTileAt(x,y).Equals(eTile.Floor) && (x<Player.transform.position.x-buffer || x>Player.transform.position.x+buffer || y<Player.transform.position.y-buffer || y>Player.transform.position.y+buffer))
			{
				Instantiate (Enemy, tilePos, Quaternion.identity);
				countEnemies++;
			}
		}
	}

	public static void SpawnItem(TileMapData map, GameObject Item)
	{
		int countItems = 0;
		while(countItems<1)
		{
			int x = Random.Range (0,map.mapData.GetLength (0)), y = Random.Range (0,map.mapData.GetLength (1));
			Vector3 tilePos = new Vector3(x, y, 0);
			if(map.GetTileAt(x,y).Equals(eTile.Floor))
			{
				Instantiate (Item, tilePos, Quaternion.identity);
				countItems++;
			}
		}
	}

	public static void RespawnEnemies(int floor)
	{
		for(int i = 0; i < MakeMap.inactiveEnemies.Length; i++)
		{
			if(MakeMap.inactiveEnemies[i]!=null && MakeMap.inactiveEnemies[i].GetComponent<AttackingMob>().initFloor == floor) MakeMap.inactiveEnemies[i].SetActive(true);
		}
	}
}
