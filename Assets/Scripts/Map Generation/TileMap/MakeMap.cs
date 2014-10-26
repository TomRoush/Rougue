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
    public GameObject UpStairs;
	public GameObject Enemy;
	public int xMax;
	public int yMax;
	public int nRooms;
	public int numEnemies;

	private TMDList dungeon = new TMDList(0);


    public int DungeonFloor;
    GameObject PlayerInstance;
    //Events to Handle Map clearings
    public delegate void DeleteTiles();
    public static event DeleteTiles OnDelete;


	
	void Start () 
	{
        DungeonFloor = 0;
        PlayerInstance = (GameObject) Instantiate(Player, new Vector3(0,0,0), Quaternion.identity);
		Invoke ("PlaceMap", 0f);
	}

	TileMapData genTMD()
	{
		TileMapData map = new TileMapData();

        if(Random.Range(0.0f,2.0f) > 1.0)
            map.GenCave(xMax,yMax);
        else
            map.GenClassic(xMax,yMax, nRooms);

        dungeon.add(map);
        Debug.Log("add");
        Debug.Log("dungeon size " + dungeon.floors.Length);


        return map;
	}

	void PlaceMap()
	{
		TileMapData map = new TileMapData();

        if(Random.Range(0.0f,2.0f) > 1.0)
            map.GenCave(xMax,yMax);
        else
            map.GenClassic(xMax,yMax, nRooms);

        dungeon.add(map);
        Debug.Log("add");
        Debug.Log("dungeon size " + dungeon.floors.Length);
        
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
					PlayerInstance.transform.position =   tilePos;
					Instantiate(UpStairs, tilePos, Quaternion.identity);
					Instantiate(Floor, tilePos, Quaternion.identity);
				}
				else if(map.GetTileAt(x,y) == eTile.Goal)
				{
					Instantiate(Goal, tilePos, Quaternion.identity);
				}
			}
		}
		Spawning.SpawnEnemies(map, numEnemies, Enemy);
	}

	public void PlaceMap(TileMapData tmd)
	{
		TileMapData map = tmd;

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
					PlayerInstance.transform.position =   tilePos;
					Instantiate(UpStairs, tilePos, Quaternion.identity);
					Instantiate(Floor, tilePos, Quaternion.identity);
				}
				else if(map.GetTileAt(x,y) == eTile.Goal)
				{
					Instantiate(Goal, tilePos, Quaternion.identity);
				}
			}
		}
		Spawning.SpawnEnemies(map, numEnemies, Enemy);
	}

    public void NextFloor()
    {
        PlayerInstance.SetActive(false);
        DungeonFloor++;
        ClearMap();
        PlaceMap();
        PlayerInstance.SetActive(true);
    }

    public void PreviousFloor()
    {
    	PlayerInstance.SetActive(false);
        DungeonFloor--;
        ClearMap();
        PlaceMap(dungeon.getTMD(DungeonFloor-1));
        PlayerInstance.SetActive(true);
    }

    void ClearMap()
    {
        if(OnDelete != null)
            OnDelete();

    }
}
