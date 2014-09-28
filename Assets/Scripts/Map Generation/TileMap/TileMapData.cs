using UnityEngine;
using System.Collections.Generic;

public partial class TileMapData 
{
	public int sizeX;
	public int sizeY; 
	public int nRooms;
	
	public int[,] mapData;
	
	List<RoomData> rooms;
	
	/*
	 * 0=unknown
	 * 1=floor
	 * 2=wall
	 * 3=filler
	 * 4=playerspawn
	 * 5=goal
	 * */
	
	protected class RoomData
	{
		public int left;
		public int bottom;
		public int width;
		public int height;
		public int roomNum;
		public List<RoomData> connectedWith = new List<RoomData>();

		public bool isConnected = false;

		public int right 
		{
			get{return left + width - 1;}
		}
		
		public int top
		{
			get{return bottom + height - 1;}
		}
		
		public int centerX
		{
			get{return left + width/2 - 1;}
		}
		
		public int centerY
		{
			get{return bottom + height/2 - 1;}
		}
		
		public bool CollidesWith(RoomData other) 
		{
			if(left > other.right || right < other.left-1 || bottom > other.top || top < other.bottom-1)
				return false;
			else 
				return true;
		}
	}
    
    

    public int GetTileAt(int x, int y)
    {
        return mapData[x,y];
    }

    private int[,] createFilledMapArray(int Filler = 3)
    {
        int[,] tmp = new int[this.sizeX,this.sizeY];
        for(int i = 0; i < sizeX; i++)
            for(int j = 0; j < sizeY; j++)
                tmp[i,j] = Filler;

        return tmp;
    }
} 

