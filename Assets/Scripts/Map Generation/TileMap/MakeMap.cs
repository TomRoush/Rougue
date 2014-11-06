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
	private bool toPrevFloor = false;

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
		PlaceMap(map);
	}

	public void PlaceMap(TileMapData tmd)
	{
		TileMapData map = tmd;
		int numOldFloors = 0;//MapUtilities.getNumTile(tmd, Floor);
		int numOldWalls = 1;//MapUtilities.getNumTile(tmd, Wall);
		int numOldFillers = 2;//MapUtilities.getNumTile(tmd, Fillers);
		Debug.Log("numOldFloors = " + numOldFloors + "\n" + "numOldWalls = " + numOldWalls + "\n" + "numOldFillers = " + numOldFillers);

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
					if(!toPrevFloor) PlayerInstance.transform.position =  tilePos;
					Instantiate(UpStairs, tilePos, Quaternion.identity);
					Instantiate(Floor, tilePos, Quaternion.identity);
				}
				else if(map.GetTileAt(x,y) == eTile.Goal)
				{
					if(toPrevFloor) PlayerInstance.transform.position =  tilePos;
					Instantiate(Goal, tilePos, Quaternion.identity);
				}
			}
		}
		if(!toPrevFloor) Spawning.SpawnEnemies(map, numEnemies, Enemy);
	}

    public void NextFloor()//called when player hits action on downstairs
    {
        PlayerInstance.SetActive(false);
        toPrevFloor = false;
        DungeonFloor++;
        ClearMap();

        if(DungeonFloor>=dungeon.length())//if the player hasn't been here before, generate a new floor
        {
        	TileMapData generated = genTMD();
        	PlaceMap(generated);
        	dungeon.add(generated);
    	}
        else PlaceMap(dungeon.getTMD(DungeonFloor));//if the player has been here, load it from the list
        PlayerInstance.SetActive(true);
    }

    public void PreviousFloor()//called when player hits action on upstairs
    {
    	if(DungeonFloor>0)
    	{
	    	PlayerInstance.SetActive(false);
	    	toPrevFloor = true;
	        DungeonFloor--;
	        ClearMap();
	        PlaceMap(dungeon.getTMD(DungeonFloor));
	        PlayerInstance.SetActive(true);
	    }
	    else Debug.Log("You are on the top floor");
    }

    void ClearMap()
    {
        if(OnDelete != null)
            OnDelete();
 		GameObject[] enemies;
 		enemies =  GameObject.FindGameObjectsWithTag ("Enemy");
        for(int i = 0; i<enemies.Length; i++)
        {
        	Destroy(enemies[i]);
        }
    }
}
