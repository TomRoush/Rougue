using UnityEngine;
using System.Collections;

public class MapUtilities : MonoBehaviour {

	//the first 3 methods stem from the isGoodMap on TMDcave. The third one is changed slightly to be more global and work for any tile, and not just player.



	//might combine the findPlayer methods into one and return an array of 2 coodinate ints instead of having two separate methods. Still deciding which would be more convenient.

	//finds the x coordinate of a player on a given map array, returns -1 if no player is present
	public static int findPlayerX(eTile[,] mapData, int sizeX, int sizeY)
	{
		for(int i = 1; i < sizeX; i++)
			for(int j = 0; j < sizeY; j++)
				if(mapData[i,j] == eTile.Player)
			{
				return i;
			}
		return  -1;
	}

	//finds the y coordinate of a player on a given map array, returns -1 if no player is present
	public static int findPlayerY(eTile[,] mapData, int sizeX, int sizeY)
	{
		for(int i = 1; i < sizeX; i++)
			for(int j = 0; j < sizeY; j++)
				if(mapData[i,j] == eTile.Player)
			{
				return j;
			}
		return -1;
	}


	//similar to what was in the isGoodMap method in TMDcave, but with one significant change: the x and y are now parameters. This means this can check if any floor, player, or misc. tile connects to the goal.
	public static bool connectsToGoal(eTile[,] mapData, int x, int y, int sizeX, int sizeY)
	{
		if(x == -1 || y == -1)
			return false;

		eTile[,] tmp = (eTile[,]) mapData.Clone();
		bool done = false;
		while (!done)
		{
			tmp[x,y] = eTile.Unknown;
			
			if(tmp[x-1,y] == eTile.Floor)
				tmp[x-1,y] = eTile.Unknown;
			if(tmp[x,y-1] == eTile.Floor)
				tmp[x,y-1] = eTile.Unknown;
			if(tmp[x,y+1] == eTile.Floor)
				tmp[x,y+1] = eTile.Unknown;
			if(tmp[x+1,y] == eTile.Floor)
				x++;
			else
				done = true;
		}
		
		done = false;
		while(!done)
		{
			done = true;
			for(int i = 0; i < sizeX; i ++)
				for(int j = 0; j < sizeY; j++)
					if(tmp[i,j] == eTile.Unknown)
				{
					if(tmp[i-1,j] == eTile.Floor)
					{
						done = false;
						tmp[i-1,j] = eTile.Unknown;
					}
					if(tmp[i,j-1] == eTile.Floor)
					{
						done = false;
						tmp[i,j-1] = eTile.Unknown;
					}
					if(tmp[i,j+1] == eTile.Floor)
					{
						done = false;
						tmp[i,j+1] = eTile.Unknown;
					}
					if(tmp[i+1,j] == eTile.Floor)
					{
						done = false;
						tmp[i+1,j] = eTile.Unknown;
					}
					
					if(tmp[i-1,j] == eTile.Goal)
						return true;
					if(tmp[i,j-1] == eTile.Goal)
						return true;
					if(tmp[i,j+1] == eTile.Goal)
						return true;
					if(tmp[i+1,j] == eTile.Goal)
						return true;
					
				}
		} 
		return false;
	}

	public static bool isGoodMap(eTile[,] mapData, int sizeX, int sizeY)
	{

		for(int i = 1; i < sizeX; i ++)
			for(int j = 1; j < sizeY; j++)
				if(mapData[i,j] == eTile.Floor)
					if(!connectsToGoal(mapData, i, j,sizeX,sizeY))
						return false;
		return true;
				
	}
}
