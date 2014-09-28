using UnityEngine;
using System.Collections.Generic;

public partial class TileMapData
{
	public void GenClassic(int sizeX, int sizeY, int nRooms) 
	{
		RoomData r;
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		this.nRooms = nRooms;
		
		mapData = createFilledMapArray();;
		
		
		rooms = new List<RoomData>();
		
		int maxFails = 20;
		while(rooms.Count<nRooms)
		{
			int roomSizeX = Random.Range (7,12);
			int roomSizeY = Random.Range (7,12);
			
			r = new RoomData();
			r.left = Random.Range(1,sizeX-roomSizeX);
			r.bottom = Random.Range(1,sizeY-roomSizeY);
			r.width = roomSizeX;
			r.height = roomSizeY;
			r.roomNum = rooms.Count+1;
			
			if(!RoomCollides(r))
			{
				rooms.Add(r);
				MakeRoom (r);
				if(r.roomNum==1)
				{
					MakeSpawn(r);
				}
				if(rooms.Count==3)
				{
					MakeGoal(r);
				}
			}
			else
			{
				maxFails--;
				if (maxFails <= 0)
					break;
			}
		}
		//make the corridors
		for(int i=0; i < rooms.Count; i++) 
		{
			/* if i=0, numRooms = 4, j = 1(min)
			 * makecorridor b/n room index 0(spawn), and room 1%4 = 1 (one more than i)
			 * if i = 7, numRooms = 10, j = 9(max)
			 * makeCorridor b/n room index 7, and room 7+9=16%10 = 6 (one less than i)
			 * */
			//if(!rooms[i].isConnected) 
			{
				while(!rooms[i].isConnected)
				{
					int j = Random.Range(1, rooms.Count);
					MakeCorridor(rooms[i], rooms[(i + j) % rooms.Count]);
				}
			}
		}
		
		MakeWalls ();
		
	}
	
	bool RoomCollides(RoomData r)
	{
		foreach(RoomData r2 in rooms)
		{
			if(r.CollidesWith(r2))
			{
				return true;
			}
		}
		return false;
	}
	
	
	void MakeRoom(RoomData r)
	{
		for(int x=0; x< r.width; x++)
		{
			for(int y = 0; y < r.height; y++)
			{
				if(x == r.width-1 || y == r.height-1)
					mapData[r.left+x,r.bottom+y] = 3;
				else
				mapData[r.left+x,r.bottom+y] = 1;
			}
		}
	}
	
	void MakeCorridor(RoomData r1, RoomData r2)//moves in y first then x direction eventually make other corridor types
	{
		int x = r1.centerX;
		int y = r1.centerY;
		
		while(x!=r2.centerX)
		{
			//if(mapData[x,y] != 4 && mapData[x,y] != 5 && mapData[x,y+1] != 4 && mapData[x,y+1] != 5)
			{
				if(x<r2.centerX && mapData[x+2,y] != 4 && mapData[x+2,y] != 5 && mapData[x+2,y+1] != 4 && mapData[x+2,y+1] != 5)
				{
					mapData[x+2,y] = 1;
					mapData[x+2,y+1] = 1;
				}
				else if (mapData[x-1,y] != 4 && mapData[x-1,y] != 5 && mapData[x-1,y+1] != 4 && mapData[x-1,y+1] != 5)
				{
					mapData[x-1,y] = 1;
					mapData[x-1,y+1] = 1;
				}
			}
			if(x<r2.centerX)
				x++;
			else 			
				x--;			
		}
		while(y!=r2.centerY)
		{
			if(mapData[x,y+1] != 4 && mapData[x,y+1] != 5 && mapData[x+1,y+1] != 4 && mapData[x+1,y+1] != 5)//will not put tiles over the player spawn or goal
			{
				mapData[x,y+1] = 1;
				mapData[x+1,y+1] = 1;
			}
			//if(x>r2.centerX) //90% sure this wasn't supposed to be here
			{
				if(mapData[x,y+2] != 4 && mapData[x,y+2] != 5 && mapData[x,y-1] != 4 && mapData[x,y-1] != 5)
				{
					if(y<r2.centerY)
						mapData[x,y+2] = 1;
					else
						mapData[x,y-1] = 1;
				}
			}
			if(y<r2.centerY)
				y++;
			else 
				y--;
		}
		if(r1.roomNum == 1 || r2.roomNum == 1 || r1.isConnected || r2.isConnected)
		{
			r1.isConnected = true;
			r1.connectedWith.Add(r2);
			//Debug.Log ("Added room " + r1.connectedWith[(r1.connectedWith.Count-1)].roomNum + " to the list for room" + r1.roomNum);
			//Debug.Log ("Room " + r1.roomNum + " isConnected = true");
			r2.isConnected = true;
			r2.connectedWith.Add (r1);
			//Debug.Log ("Added room " + r2.connectedWith[(r2.connectedWith.Count-1)].roomNum + " to the list for room" + r2.roomNum);
			//Debug.Log ("Room " + r2.roomNum + " isConnected = true");
			if(r1.connectedWith.Count>0)
			{
				for(int i = 0; i<r1.connectedWith.Count; i++)
				{
					r1.connectedWith[i].isConnected = true;
					//Debug.Log ("Room " + r1.connectedWith[i].roomNum + " isConnected = true");
				}
			}
			if(r2.connectedWith.Count>0)
			{
				for(int i = 0; i<r2.connectedWith.Count; i++)
				{
					r2.connectedWith[i].isConnected = true;
					//Debug.Log ("Room " + r2.connectedWith[i].roomNum + " isConnected = true");
				}
			}
		}
	}
	
	void MakeWalls()
	{
		for(int x=0; x < sizeX; x++)
		{
			for(int y=0; y < sizeY; y++)
			{
				if(mapData[x,y] == 3 && HasAdjacentFloors(x,y))
				{
					mapData[x,y] = 2;
				}	
			}
		}
	}
	
	bool HasAdjacentFloors(int x, int y)
	{
		if(x>0 && (mapData[x-1,y] == 1 || mapData[x-1,y] == 4 || mapData[x-1,y] == 5))
			return true;
		if(x<sizeX-1 && (mapData[x+1,y] == 1 || mapData[x+1,y] == 4 || mapData[x+1,y] == 5))
			return true;
		if(y>0 && (mapData[x,y-1] == 1 || mapData[x,y-1] == 4 || mapData[x,y-1] == 5))
			return true;
		if(y<sizeY-1 && (mapData[x,y+1] == 1 || mapData[x,y+1] == 4 || mapData[x,y+1] == 5))
			return true;
		
		if(x>0 && y>0 && (mapData[x-1, y-1] == 1 || mapData[x-1, y-1] == 4 || mapData[x-1, y-1] == 5))
			return true;
		if(x<sizeX-1 && y>0 && (mapData[x+1, y-1] == 1 || mapData[x+1, y-1] == 4 || mapData[x+1, y-1] == 5))
			return true;
		if(x>0 && y<sizeY-1 && (mapData[x-1, y+1] == 1 || mapData[x-1, y+1] == 4 || mapData[x-1, y+1] == 5))
			return true;
		if(x<sizeX-1 && y<sizeY-1 && (mapData[x+1, y+1] == 1 || mapData[x+1, y+1] == 4 || mapData[x+1, y+1] == 5))
			return true;
		
		return false;
	}
	
	void MakeSpawn(RoomData r)
	{
		int x = Random.Range (r.left+1, r.left+r.width-1);
		int y = Random.Range (r.bottom+1, r.bottom+r.height-1);
		mapData[x, y] = 4;
	}
	
	void MakeGoal(RoomData r)
	{
		int x = Random.Range (r.left+1, r.left+r.width-1);
		int y = Random.Range (r.bottom+1, r.bottom+r.height-1);
		mapData[x, y] = 5;
	}
}
