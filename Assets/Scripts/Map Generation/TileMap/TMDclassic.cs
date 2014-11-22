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
		
		mapData = createFilledMapArray(eTile.Filler);
		
		rooms = new List<RoomData>();
		
		int maxFails = 20;//will stop an infinite loop from trying to create rooms that won't physically fit in the map
		while(rooms.Count<nRooms)
		{
			int roomSizeX = Random.Range (7,12);//width will be b/n these numbers
			int roomSizeY = Random.Range (7,12);//height will be b/n these numbers

			//creates new room
			r = new RoomData();
			r.left = Random.Range(1,sizeX-roomSizeX);
			r.bottom = Random.Range(1,sizeY-roomSizeY);
			r.width = roomSizeX;
			r.height = roomSizeY;

			r.roomNum = rooms.Count+1;//used to choose spawn/goal and for debugging
			
			if(!RoomCollides(r))//makes sure the room won't collide with the existing list of rooms
			{
				rooms.Add(r);
				MakeRoom (r);
				if(r.roomNum==1)//creates the spawn if this is the first room
				{
					MakeSpawn(r);
				}
				if(rooms.Count==3)//creates the goal if this is the third room
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
			//(i + j) % rooms.Count
			/* if i=0, numRooms = 4, j = 1(min)
			 * makecorridor b/n room index 0(spawn), and room 1%4 = 1 (one more than i)
			 * if i = 7, numRooms = 10, j = 9(max)
			 * makeCorridor b/n room index 7, and room 7+9=16%10 = 6 (one less than i)
			 * */
			while(!rooms[i].isConnected)//repeat untill the room is connected to the spawn
			{
				int j = Random.Range(1, rooms.Count);
				MakeCorridor(rooms[i], rooms[(i + j) % rooms.Count]);//(i + j) % rooms.Count will choose a room to connect to excluding room i (proven above)
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
				if(x == r.width-1 || y == r.height-1)//checks if this is the outermost tile of the room
					mapData[r.left+x,r.bottom+y] = eTile.Wall;//makes a wall tile
				else
				mapData[r.left+x,r.bottom+y] = eTile.Floor;//makes a floor tile
			}
		}
	}
	
	void MakeCorridor(RoomData r1, RoomData r2)//moves in x first then y direction eventually will make other corridor types
	{
		int x = r1.centerX;
		int y = r1.centerY;

		while(x!=r2.centerX)//creates the corridor by moving the x coordinate from the center of room r1 to the center of r2
		{
			if(x<r2.centerX && mapData[x+2,y] != eTile.Player && mapData[x+2,y] != eTile.Goal && mapData[x+2,y+1] != eTile.Player && mapData[x+2,y+1] != eTile.Goal)//makes sure this tile isn't the player or exit
			{
				mapData[x+2,y] = eTile.Floor;//these are the floor tiles of the corridors same for the code below (lines 189-190, 201-202, etc)
				mapData[x+2,y+1] = eTile.Floor;
			}
			else if (mapData[x-1,y] != eTile.Player && mapData[x-1,y] != eTile.Goal && mapData[x-1,y+1] != eTile.Player && mapData[x-1,y+1] != eTile.Goal)
			{
				mapData[x-1,y] = eTile.Floor;
				mapData[x-1,y+1] = eTile.Floor;
			}
			if(x<r2.centerX)
				x++;
			else		
				x--;
		}
		while(y!=r2.centerY)//creates the corridor by moving the y coordinate from the center of room r1 to the center of r2
		{
			if(mapData[x,y+1] != eTile.Player && mapData[x,y+1] != eTile.Goal && mapData[x+1,y+1] != eTile.Player && mapData[x+1,y+1] != eTile.Goal)//will not put tiles over the player spawn or goal
			{
				mapData[x,y+1] = eTile.Floor;
				mapData[x+1,y+1] = eTile.Floor;
			}
			if(mapData[x,y+2] != eTile.Player && mapData[x,y+2] != eTile.Goal && mapData[x,y-1] != eTile.Player && mapData[x,y-1] != eTile.Goal)
			{
				if(y<r2.centerY)
					mapData[x,y+2] = eTile.Floor;
				else
					mapData[x,y-1] = eTile.Floor;
			}
			if(y<r2.centerY)
				y++;
			else 
				y--;
		}

		//make sure the room is connected 
		//uncomment debug statements to see the process for setting isConnected
		if(r1.roomNum == 1 || r2.roomNum == 1 || r1.isConnected || r2.isConnected)//if one of these rooms is #1(spawn room) or is connected to the spawn room
		{
			//set both rooms to connected and add them to each others connectedWith list
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
				for(int i = 0; i<r1.connectedWith.Count; i++)//runs through the list of rooms connected with this one and sets isConnected to true
				{
					r1.connectedWith[i].isConnected = true;
					//Debug.Log ("Room " + r1.connectedWith[i].roomNum + " isConnected = true");
				}
			}
			if(r2.connectedWith.Count>0)
			{
				for(int i = 0; i<r2.connectedWith.Count; i++)//same as above
				{
					r2.connectedWith[i].isConnected = true;
					//Debug.Log ("Room " + r2.connectedWith[i].roomNum + " isConnected = true");
				}
			}
		}
	}

	//MakeWalls() finds all filler tiles (darker gray) next to floor tiles and makes them wall tiles
	//I found this to be better than making them as you make the corridors/rooms so you don't block off certain areas
	void MakeWalls()
	{
		for(int x=0; x < sizeX; x++)
		{
			for(int y=0; y < sizeY; y++)
			{
				if(mapData[x,y] == eTile.Filler && HasAdjacentFloors(x,y))
				{
					mapData[x,y] = eTile.Wall;
				}	
			}
		}
	}

	bool IsFloor(int x, int y)
	{
		return mapData[x,y] == eTile.Floor;
	}
	
	bool HasAdjacentFloors(int x, int y)// used in the MakeWall() method to determine if a certain filler tile has a floor adjacent to it (includes diagonals)
	{
		if(x>0 && (mapData[x-1,y] == eTile.Floor || mapData[x-1,y] == eTile.Player || mapData[x-1,y] == eTile.Goal))
			return true;
		if(x<sizeX-1 && (mapData[x+1,y] == eTile.Floor || mapData[x+1,y] == eTile.Player || mapData[x+1,y] == eTile.Goal))
			return true;
		if(y>0 && (mapData[x,y-1] == eTile.Floor || mapData[x,y-1] == eTile.Player || mapData[x,y-1] == eTile.Goal))
			return true;
		if(y<sizeY-1 && (mapData[x,y+1] == eTile.Floor || mapData[x,y+1] == eTile.Player || mapData[x,y+1] == eTile.Goal))
			return true;

		//diagonals
		if(x>0 && y>0 && (mapData[x-1, y-1] == eTile.Floor || mapData[x-1, y-1] == eTile.Player || mapData[x-1, y-1] == eTile.Goal))
			return true;
		if(x<sizeX-1 && y>0 && (mapData[x+1, y-1] == eTile.Floor || mapData[x+1, y-1] == eTile.Player || mapData[x+1, y-1] == eTile.Goal))
			return true;
		if(x>0 && y<sizeY-1 && (mapData[x-1, y+1] == eTile.Floor || mapData[x-1, y+1] == eTile.Player || mapData[x-1, y+1] == eTile.Goal))
			return true;
		if(x<sizeX-1 && y<sizeY-1 && (mapData[x+1, y+1] == eTile.Floor || mapData[x+1, y+1] == eTile.Player || mapData[x+1, y+1] == eTile.Goal))
			return true;
		return false;
	}
	
	void MakeSpawn(RoomData r)//chooses a random location in the spawn room to spawn the player
	{
		int x = Random.Range (r.left+1, r.left+r.width-1);
		int y = Random.Range (r.bottom+1, r.bottom+r.height-1);
		mapData[x, y] = eTile.Player;
	}
	
	void MakeGoal(RoomData r)//chooses a random location in the goal room for the exit
	{
		int x = Random.Range (r.left+1, r.left+r.width-1);
		int y = Random.Range (r.bottom+1, r.bottom+r.height-1);
		mapData[x, y] = eTile.Goal;
	}
}
