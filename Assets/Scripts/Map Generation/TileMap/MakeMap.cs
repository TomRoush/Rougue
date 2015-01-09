using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum eTile {Unknown, dConnectedFloor, dConvertedFiller, Floor, Wall, Filler, Player, Goal, Enemy, Potion};
public enum TileSet {Classic, Cave};

public class MakeMap : MonoBehaviour 
{
	public GameObject Unknown;
	public GameObject Floor;
	public GameObject Wall;
	public GameObject Filler;
	public GameObject Player;
	public GameObject Goal;
    public GameObject UpStairs;
	public GameObject Sword;
	public GameObject Necklace;
	public GameObject Armor;
	public GameObject Helmet;
    public GameObject Potion;

	public GameObject eGhost;
    public GameObject eRat;
    public GameObject eDragon;
	public int xMax;
	public int yMax;
	public int nRooms;
	public int numEnemies;
 	private float enemySpawnTimer;

	private TMDList dungeon = new TMDList(0);
	private bool toPrevFloor = false;

	private Text floorText;
    public int DungeonFloor;
    GameObject PlayerInstance;

    private int maxFloors=0, maxWalls=0;

	public static GameObject[] inactiveEnemies = new GameObject[0], inactiveEquip = new GameObject[0], inactiveItems = new GameObject[0];	
	
	void Start () 
	{
		floorText = GameObject.Find ("FloorText").GetComponent<Text>();
        DungeonFloor = 0;
        enemySpawnTimer = Time.realtimeSinceStartup;
        PlayerInstance = (GameObject) Instantiate(Player, new Vector3(0,0,0), Quaternion.identity);
		Invoke ("PlaceMap", 0f);
	}

	private void Update()
    {
    	if(Time.realtimeSinceStartup>=enemySpawnTimer+30 && GameObject.FindGameObjectsWithTag("Enemy").Length<10)
    	{
    		if(dungeon.getTMD(DungeonFloor)!=null && DungeonFloor<dungeon.length()) EnemySpawningDifficulty(dungeon.getTMD(DungeonFloor), 1);
    		enemySpawnTimer = Time.realtimeSinceStartup;
    	}
    }

    private GameObject SpawnAppropriateEnemy(int floor)
    {
        float ratMax = (6-floor)/6f;
        float ghostMax = ((1/8f) * (6-floor))+1;
       // float dragonMax;


        float val = Random.Range(0f,1f);
        if(val < ratMax)
            return eRat;
        else if(val < ghostMax)
            return eGhost;
        else 
            return eDragon;
            
    }


    public void EnemySpawningDifficulty(TileMapData map, int numEnemies)
    {
        for(int i = 0; i < numEnemies; i++)
        {
            Spawning.SpawnEnemies(map,1,SpawnAppropriateEnemy(DungeonFloor),PlayerInstance);
        }
    }

    public void SpawnPotion(TileMapData map, int x, int y, float z) //x and y of tile
    {
       if(map.GetTileAt(x-1,y) == eTile.Floor)
    	Instantiate(Potion, new Vector3( x-1,y ,z ), Quaternion.identity);
         else if(map.GetTileAt(x+1,y) == eTile.Floor)
    	Instantiate(Potion, new Vector3( x+1,y ,z ), Quaternion.identity);
        
        
    }

        /*
            if(DungeonFloor <= 4)
                Spawning.SpawnEnemies(map, numEnemies, eRat, PlayerInstance);
            if(DungeonFloor == 4)
                Spawning.SpawnEnemies(map, 1, eGhost, PlayerInstance);
            if(DungeonFloor > 4)
                Spawning.SpawnEnemies(map, numEnemies, eGhost, PlayerInstance);
			if(DungeonFloor == 9)
				Spawning.SpawnEnemies(map, 1, eDragon, PlayerInstance);
			if(DungeonFloor > 9)
				Spawning.SpawnEnemies(map, numEnemies, eDragon, PlayerInstance);
                */

	TileMapData genTMD()
	{
		TileMapData map = new TileMapData();
		if(DungeonFloor%5==0 && DungeonFloor!=0) 
		{
			map.GenArena();
			map.set = TileSet.Classic;
		}
        else if(Random.Range(0.0f,2.0f) > 1.0) {
            map.GenCave(xMax,yMax,40);
			map.set = TileSet.Cave;
        } else {
            map.GenClassic(xMax,yMax, nRooms);
			map.set = TileSet.Classic;
		}
    	return map;
	}

	void PlaceMap()
	{
		float startTime = Time.realtimeSinceStartup;
		TileMapData map = genTMD();

        dungeon.add(map);
		PlaceMap(map);
		float endTime = Time.realtimeSinceStartup;
		Debug.Log(endTime-startTime + "seconds loadtime");
	}

	public void PlaceMap(TileMapData tmd)
	{
		TileMapData map = tmd;
		int numOldFloors = MapUtilities.getNumTile(map.mapData, eTile.Floor);
		int numOldWalls = MapUtilities.getNumTile(map.mapData, eTile.Wall);
		//Debug.Log("numOldFloors = " + numOldFloors);
		//Debug.Log("numOldWalls = " + numOldWalls);
		if(numOldFloors>maxFloors) maxFloors = numOldFloors;
		if(numOldWalls>maxWalls) maxWalls = numOldWalls;

		for(int y=0; y<map.sizeY; y++)
		{
			for(int x=0; x<map.sizeX; x++)
			{
				Vector3 tilePos = new Vector3(x, y, Floor.transform.position.z);
				//if logic instantiates the proper prefab
				GameObject tile = null;
				if(map.GetTileAt(x,y) == eTile.Unknown)
					tile = Instantiate(Unknown, tilePos, Quaternion.identity) as GameObject;
				else if(map.GetTileAt(x,y) == eTile.Floor)
					tile = Instantiate(Floor, tilePos, Quaternion.identity) as GameObject;
				else if(map.GetTileAt(x,y) == eTile.Wall)
					tile = Instantiate(Wall, tilePos, Quaternion.identity) as GameObject;
				else if(map.GetTileAt(x,y) == eTile.Player)
				{
					if(!toPrevFloor) PlayerInstance.transform.position =  tilePos;
					tile = Instantiate(UpStairs, tilePos, Quaternion.identity) as GameObject;
					/*if(tile != null) {*/  tile.GetComponent<TileSetChanger>().setTile(map.set); //}
					tile = Instantiate(Floor, tilePos, Quaternion.identity) as GameObject;
				}
				else if(map.GetTileAt(x,y) == eTile.Goal)
				{
					if(toPrevFloor) PlayerInstance.transform.position =  tilePos;
                    else {


                        SpawnPotion(map,x,y,Floor.transform.position.z);
                    }
					tile = Instantiate(Goal, tilePos, Quaternion.identity) as GameObject;
				}
				
				if(tile != null) { tile.GetComponent<TileSetChanger>().setTile(map.set); }
			}
		}
		if(!toPrevFloor) 
        {
            EnemySpawningDifficulty(map, numEnemies);
            SpawnItem(map);
        }
	}

	public void MoveMap(TileMapData tmd)
	{
		TileMapData map = tmd;
		int floorIndex = 0, wallIndex = 0;
		GameObject[] activeFloorTiles = GameObject.FindGameObjectsWithTag("Floor");
		GameObject[] activeWallTiles = GameObject.FindGameObjectsWithTag("Wall");
		
		foreach(GameObject tile in activeFloorTiles) {
			tile.GetComponent<TileSetChanger>().setTile(map.set);
		}
		foreach(GameObject tile in activeWallTiles) {
			tile.GetComponent<TileSetChanger>().setTile(map.set);
		}
		
		if(activeFloorTiles.Length>maxFloors) maxFloors = activeFloorTiles.Length;
		if(activeWallTiles.Length>maxWalls) maxWalls = activeWallTiles.Length;
		GameObject[] allFloorTiles = new GameObject[maxFloors];
		GameObject[] allWallTiles = new GameObject[maxWalls];
		//Debug.Log("maxFloors = "+ maxFloors + "; maxWalls = "+ maxWalls);
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
                    else SpawnPotion(map,x,y,Floor.transform.position.z);
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
						allFloorTiles[floorIndex].SetActive(true);
						floorIndex++;
					}
					else
					{
						(Instantiate(Floor, tilePos, Quaternion.identity) as GameObject).GetComponent<TileSetChanger>().setTile(map.set);
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
						allWallTiles[wallIndex].SetActive(true);
						wallIndex++;
					}
					else
					{
						(Instantiate(Wall, tilePos, Quaternion.identity) as GameObject).GetComponent<TileSetChanger>().setTile(map.set);
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
			if(allFloorTiles[i]!=null) allFloorTiles[i].SetActive(false);
			//Destroy(allFloorTiles[i]);
		}
		for(int i = wallIndex; i<allWallTiles.Length; i++)
		{
			if(allWallTiles[i]!=null) allWallTiles[i].SetActive(false);
			//Destroy(allWallTiles[i]);
		}
		RefreshEnemies();
		if(!toPrevFloor) SpawnItem(map);
	}

    public void NextFloor()//called when player hits action on downstairs
    {
    	float startTime = Time.realtimeSinceStartup;
        PlayerInstance.SetActive(false);
        toPrevFloor = false;
        DungeonFloor++;
        ClearEnemies();
        ClearItems();

        if(DungeonFloor>=dungeon.length())//if the player hasn't been here before, generate a new floor
        {
        	TileMapData generated = genTMD();
        	MoveMap(generated);
        	dungeon.add(generated);
            EnemySpawningDifficulty(generated, numEnemies);
    	}
        else 
        {
        	MoveMap(dungeon.getTMD(DungeonFloor));//if the player has been here, load it from the list
        	Spawning.RespawnEnemies(DungeonFloor);
		}
        PlayerInstance.SetActive(true);
        float endTime = Time.realtimeSinceStartup;
		Debug.Log(endTime-startTime + "seconds loadtime");
		floorText.text = "Floor: " + (1 + DungeonFloor);
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
	        ClearItems();
	        MoveMap(dungeon.getTMD(DungeonFloor));
	        PlayerInstance.SetActive(true);
	        float endTime = Time.realtimeSinceStartup;
			Debug.Log(endTime-startTime + "seconds loadtime");
			Spawning.RespawnEnemies(DungeonFloor);
			floorText.text = "Floor: " + (1 + DungeonFloor);
	    }
	    else Debug.Log("You are on the top floor");
    }

    void ClearEnemies()
    {
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
    	GameObject[] temp = new GameObject[enemies.Length + inactiveEnemies.Length];
    	for(int i =0; i<inactiveEnemies.Length; i++)
    	{
    		if(inactiveEnemies[i]!=null) temp[i] = inactiveEnemies[i];
    		//Debug.Log("inactiveEnemies["+i+"].initFloor = "+ inactiveEnemies[i].GetComponent<AttackingMob>().initFloor);
    	}
        for(int i = 0; i<enemies.Length; i++)
        {
        	enemies[i].SetActive(false);
        	temp[i+inactiveEnemies.Length] = enemies[i];
        }
        inactiveEnemies = temp;
    }

	void ClearItems()
	    {
	    	GameObject[] equip = GameObject.FindGameObjectsWithTag("Equip");
	    	GameObject[] tempEquip = new GameObject[equip.Length + inactiveEquip.Length];
	    	for(int i = 0; i<inactiveEquip.Length; i++)
	    	{
	    		if(inactiveEquip[i]!=null) tempEquip[i] = inactiveEquip[i];
	    	}
	    	for(int i = 0; i<equip.Length; i++)
	    	{
	    		equip[i].SetActive(false);
	    		tempEquip[i+inactiveEquip.Length] = equip[i];
	    	}
	    	inactiveEquip = tempEquip;

	    	GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
	    	GameObject[] tempItems = new GameObject[items.Length + inactiveItems.Length];
	    	for(int i = 0; i<inactiveItems.Length; i++)
	    	{
	    		if(inactiveItems[i]!=null) tempItems[i] = inactiveItems[i];
	    	}
	    	for(int i = 0; i<items.Length; i++)
	    	{
	    		items[i].SetActive(false);
	    		tempItems[i+inactiveItems.Length] = items[i];
	    	}
	    	inactiveItems = tempItems;

	    }

    void RefreshEnemies()
    {
    	for(int i = 0; i<inactiveEnemies.Length;i++)
        {
        	//Debug.Log(inactiveEnemies.Length);
        	if(inactiveEnemies[i]!=null && inactiveEnemies[i].GetComponent<AttackingMob>().initFloor < DungeonFloor-5) Destroy(inactiveEnemies[i]);
        	//Debug.Log("inactiveEnemies["+i+"].initFloor = " + inactiveEnemies[i].GetComponent<AttackingMob>().initFloor);
        }
    }

    public eTile[,] currentFloor()
    {
    	return dungeon.getTMD(DungeonFloor).copyMapArray();
    }

    void SpawnItem(TileMapData map)
    {
    	int r = (int)Random.Range(0,4);
		if(r==0)
		{
			Spawning.SpawnItem(map, Sword);
			GameObject.FindGameObjectWithTag("Equip").GetComponent<Weapon>().setStats(DungeonFloor+1, DungeonFloor/2, 0);
		}
		else if(r==1)
		{
			Spawning.SpawnItem(map, Helmet);
			GameObject.FindGameObjectWithTag("Equip").GetComponent<Helmet>().setStats(DungeonFloor/2, DungeonFloor+1, 0);
		}
		else if(r==2)
		{
			Spawning.SpawnItem(map, Necklace);
			GameObject.FindGameObjectWithTag("Equip").GetComponent<Necklace>().setStats(DungeonFloor+1, DungeonFloor+1, DungeonFloor+1);
		}
		else if(r==3)
		{
			Spawning.SpawnItem(map, Armor);
			GameObject.FindGameObjectWithTag("Equip").GetComponent<Armor>().setStats(DungeonFloor/2, 0, DungeonFloor+1);
		}

    }
}
