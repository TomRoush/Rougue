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

    private int maxFloors=0, maxWalls=0;


	
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
		float startTime = Time.realtimeSinceStartup;
		TileMapData map = new TileMapData();

        if(Random.Range(0.0f,2.0f) > 1.0)
            map.GenCave(xMax,yMax);
        else
            map.GenClassic(xMax,yMax, nRooms);

        dungeon.add(map);
		NOEDITPlaceMap(map);
		float endTime = Time.realtimeSinceStartup;
		Debug.Log(endTime-startTime + "seconds loadtime");
	}

	public void NOEDITPlaceMap(TileMapData tmd)
	{
		TileMapData map = tmd;
		int numOldFloors = MapUtilities.getNumTile(map.mapData, eTile.Floor);
		int numOldWalls = MapUtilities.getNumTile(map.mapData, eTile.Wall);
		Debug.Log("numOldFloors = " + numOldFloors);
		Debug.Log("numOldWalls = " + numOldWalls);
		if(numOldFloors>maxFloors) maxFloors = numOldFloors;
		if(numOldWalls>maxWalls) maxWalls = numOldWalls;

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

	public void MoveMap(TileMapData tmd)
	{
		TileMapData map = tmd;
		int floorIndex = 0, wallIndex = 0;
		GameObject[] activeFloorTiles = GameObject.FindGameObjectsWithTag("Floor");
		GameObject[] activeWallTiles = GameObject.FindGameObjectsWithTag("Wall");
		if(activeFloorTiles.Length>maxFloors) maxFloors = activeFloorTiles.Length;
		if(activeWallTiles.Length>maxWalls) maxWalls = activeWallTiles.Length;
		GameObject[] allFloorTiles = new GameObject[maxFloors];
		GameObject[] allWallTiles = new GameObject[maxWalls];
		Debug.Log("maxFloors = "+ maxFloors + "; maxWalls = "+ maxWalls);
		for(int i = 0; i<activeFloorTiles.Length; i++)
			allFloorTiles[i]=activeFloorTiles[i];
		for(int i = 0; i<activeWallTiles.Length; i++)
			allWallTiles[i] = activeWallTiles[i];
		for(int y=0; y<map.sizeY; y++)
		{
			for(int x=0; x<map.sizeX; x++)
			{
				Vector3 tilePos = new Vector3(x, y, Floor.transform.position.z);
				if(map.GetTileAt(x,y) == eTile.Goal)
				{
					if(toPrevFloor) PlayerInstance.transform.position =  tilePos;
					GameObject.FindGameObjectWithTag("goal").transform.position = tilePos;
				}
				if(map.GetTileAt(x,y) == eTile.Player)
				{
					if(!toPrevFloor) PlayerInstance.transform.position = tilePos;
					GameObject.FindGameObjectWithTag("UpStairs").transform.position = tilePos;
				}
				if(map.GetTileAt(x,y) == eTile.Floor)
				{
					if(floorIndex<allFloorTiles.Length && allFloorTiles[floorIndex]!=null)
					{
						allFloorTiles[floorIndex].transform.position = tilePos;
						allFloorTiles[floorIndex].active = true;
						floorIndex++;
					}
					else
					{
						Instantiate(Floor, tilePos, Quaternion.identity);
						if(floorIndex<allFloorTiles.Length)
						{
							allFloorTiles[floorIndex] = Floor; 
							floorIndex++;
						}
					}
				}
				if(map.GetTileAt(x,y) == eTile.Wall)
				{
					if(wallIndex<allWallTiles.Length && allWallTiles[wallIndex]!=null)
					{
						allWallTiles[wallIndex].transform.position = tilePos;
						allWallTiles[wallIndex].active = true;
						wallIndex++;
					}
					else
					{
						Instantiate(Wall, tilePos, Quaternion.identity);
						if(wallIndex<allWallTiles.Length)
						{
							allWallTiles[wallIndex] = Wall; 
							wallIndex++;
						}
					}
				}
			}
		}
		for(int i = floorIndex; i<allFloorTiles.Length; i++)
		{
			if(allFloorTiles[i]!=null) allFloorTiles[i].active = false;
			//Destroy(allFloorTiles[i]);
		}
		for(int i = wallIndex; i<allWallTiles.Length; i++)
		{
			if(allWallTiles[i]!=null) allWallTiles[i].active = false;
			//Destroy(allWallTiles[i]);
		}

		if(!toPrevFloor) Spawning.SpawnEnemies(map, numEnemies, Enemy);
	}

    public void NextFloor()//called when player hits action on downstairs
    {
    	float startTime = Time.realtimeSinceStartup;
        PlayerInstance.SetActive(false);
        toPrevFloor = false;
        DungeonFloor++;
        ClearEnemies();

        if(DungeonFloor>=dungeon.length())//if the player hasn't been here before, generate a new floor
        {
        	TileMapData generated = genTMD();
        	MoveMap(generated);
        	dungeon.add(generated);
    	}
        else 
        {
        	MoveMap(dungeon.getTMD(DungeonFloor));//if the player has been here, load it from the list
		}
        PlayerInstance.SetActive(true);
        float endTime = Time.realtimeSinceStartup;
		Debug.Log(endTime-startTime + "seconds loadtime");
    }

    public void PreviousFloor()//called when player hits action on upstairs
    {
    	if(DungeonFloor>0)
    	{
	    	float startTime = Time.realtimeSinceStartup;
			PlayerInstance.SetActive(false);
	    	toPrevFloor = true;
	        DungeonFloor--;
	        ClearEnemies();
	        MoveMap(dungeon.getTMD(DungeonFloor));
	        PlayerInstance.SetActive(true);
	        float endTime = Time.realtimeSinceStartup;
			Debug.Log(endTime-startTime + "seconds loadtime");
	    }
	    else Debug.Log("You are on the top floor");
    }

    void ClearEnemies()
    {
    	GameObject[] enemies;
 		enemies =  GameObject.FindGameObjectsWithTag ("Enemy");
        for(int i = 0; i<enemies.Length; i++)
        {
        	Destroy(enemies[i]);
        }
    }

    public eTile[,] currentFloor()
    {
    	return dungeon.getTMD(DungeonFloor).copyMapArray();
    }
}
