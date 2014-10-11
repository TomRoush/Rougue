using UnityEngine;
using System.Collections;

public enum eTile {Unknown, Floor, Wall, Filler, Player, Goal, Enemy};

public class MakeMap : MonoBehaviour 
{
	public GameObject Unknown;
	public GameObject Floor;
	public GameObject Wall;
	public GameObject Filler;
	public GameObject Player;
	public GameObject Goal;
	public GameObject Enemy;
	public int xMax;
	public int yMax;
	public int nRooms;
	public int numEnemies;


	
	void Start () 
	{
		Invoke ("PlaceMap", 0f);
	}

	void PlaceMap()
	{
		TileMapData map = new TileMapData();

        if(Random.Range(0.0f,2.0f) > 1.0)
            map.GenCave(xMax,yMax);
        else
            map.GenClassic(xMax,yMax, nRooms);

		for(int y=0; y<map.sizeY; y++)
		{
			for(int x=0; x<map.sizeX; x++)
			{
				Vector3 tilePos = new Vector3(x, y, Floor.transform.position.z);
				//if logic instantiates the proper prefab
				if(map.GetTileAt(x,y) == eTile.Unknown)
					Instantiate(Unknown, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == eTile.Floor)
					Instantiate(Floor, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == eTile.Wall)
					Instantiate(Wall, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == eTile.Filler)
					Instantiate(Filler, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == eTile.Player)
				{
					Instantiate(Player, tilePos, Quaternion.identity);//Instantiate Player first or the player will be invisible when spawned
					Instantiate(Floor, tilePos, Quaternion.identity);
				}
				else if(map.GetTileAt(x,y) == eTile.Goal)
				{
					Instantiate(Goal, tilePos, Quaternion.identity);
				}
			}
		}

		int countEnemies = 0;
		while(countEnemies < numEnemies)
		{
			int x = Random.Range (0,xMax), y = Random.Range (0,yMax);
			Vector3 tilePos = new Vector3(x, y, Floor.transform.position.z);
			//if(countEnemies == numEnemies) break;
			if(map.GetTileAt(x,y).Equals(eTile.Floor))
			{
				Instantiate (Enemy, tilePos, Quaternion.identity);
				countEnemies++;
			}
		}
	}
}
