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
	
	void Start () 
	{
		Invoke ("GenMap", 0f);
	}

	void GenMap()
	{
		TileMapData1 map = new TileMapData1(xMax,yMax, nRooms);

		for(int y=0; y<map.sizeY; y++)
		{
			for(int x=0; x<map.sizeX; x++)
			{
				Vector3 tilePos = new Vector3(x, y, Floor.transform.position.z);
				if(map.GetTileAt(x,y) == 0)
					Instantiate(Unknown, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == 1)
					Instantiate(Floor, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == 2)
					Instantiate(Wall, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == 3)
					Instantiate(Filler, tilePos, Quaternion.identity);
				else if(map.GetTileAt(x,y) == 4)
				{
					Instantiate(Player, tilePos, Quaternion.identity);
					Instantiate(Floor, tilePos, Quaternion.identity);
				}
				else if(map.GetTileAt(x,y) == 5)
				{
					Instantiate(Goal, tilePos, Quaternion.identity);
				}
			}
		}
	}
}
