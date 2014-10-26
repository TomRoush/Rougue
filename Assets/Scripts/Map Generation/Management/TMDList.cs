public class TMDList 
{
	public TileMapData[] floors;
	private static int nFloors = 1;

	public TMDList()
	{
		floors = new TileMapData[0];
	}

	public TMDList(int n)
	{
		floors = new TileMapData[n];
	}

	public TMDList(TMDList old)
	{
		TMDList result = new TMDList(old.floors.Length);
		for(int i = 0; i < floors.Length; i++)
		{
			result.floors[i] = old.floors[i];
		}
	}

	public void add(TileMapData tmd)
	{
		TileMapData[] temp = new TileMapData[floors.Length+1];
		for(int i = 0; i < floors.Length; i++)
			temp[i] = floors[i];
		temp[temp.Length-1] = tmd;
		floors = temp;
	}

	public TileMapData getTMD(int i)
	{
		return floors[i];
	}

	public int length()
	{
		return floors.Length;
	}
}
