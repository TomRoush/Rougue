using UnityEngine;
using System.Collections;

public class MakeMap : MonoBehaviour 
{
	public GameObject Unknown;
	public GameObject Floor;
	public GameObject Wall;
	public GameObject Filler;
	public GameObject Player;
	public GameObject Goal;
	public int xMax;
	public int yMax;
	public int nRooms;

	public enum Tiles {Unknown, Floor, Wall, Filler, Player, Goal};

	
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
				if(map.GetTileAt(x,y) == (int)Tiles.Unknown)
					Instantiate(Unknown, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == (int)Tiles.Floor)
					Instantiate(Floor, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == (int)Tiles.Wall)
					Instantiate(Wall, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == (int)Tiles.Filler)
					Instantiate(Filler, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == (int)Tiles.Player)
				{
					Instantiate(Player, tilePos, Quaternion.identity);//Instantiate Player first or the player will be invisible when spawned
					Instantiate(Floor, tilePos, Quaternion.identity);
				}
				else if(map.GetTileAt(x,y) == (int)Tiles.Goal)
				{
					Instantiate(Goal, tilePos, Quaternion.identity);
				}
			}
		}
	}
}
