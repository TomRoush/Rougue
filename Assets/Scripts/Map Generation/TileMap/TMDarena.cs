using UnityEngine;
using System.Collections.Generic;


public partial class TileMapData
{
	public void GenArena()
	{
		this.sizeX = 50;
		this.sizeY = 50;

		mapData = createFilledMapArray(eTile.Filler);

		for(int i = 12; i < 38; i++)
		{
			for(int j = 20; j < 30; j++)
			mapData[i,j] = eTile.Floor;
		}
		for(int i = 20; i < 30; i++) 
		{
			for(int j = 12; j < 38; j++)
			mapData[i,j] = eTile.Floor;
		}
		for(int i = 30; i < 39; i ++)//bottom right 
		{
			for(int j = 20; j >i-18; j--)
			mapData[i,j] = eTile.Floor;
		}
		for(int i = 12; i < 21; i++)//top left
		{
			for(int j = 30; j<i+18; j++)
			mapData[i,j] = eTile.Floor;
		}
		for(int i = 30; i < 38; i++)//top right
		{
			for(int j = 30; j<67-i; j++)
			mapData[i,j] = eTile.Floor;
		}
		for(int i = 12; i < 21; i++)//bottom left
		{
			for(int j = 12; j < 21; j++)
			if(j+i>31) mapData[i,j] = eTile.Floor;
		}

		for(int i = 21; i < 29; i++)
		{
			for(int j = 3; j<10 ;j++)
			{
				mapData[i,j] = eTile.Floor;
				mapData[i,j+39] = eTile.Floor;
			}
		}

		for(int i = 24; i<26; i++)
		{
			for(int j = 10; j<43; j++)
			mapData[i,j] = eTile.Floor;
		}

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

		mapData[27,7] = eTile.Player;
		mapData[27,47] = eTile.Goal;
	}	
}
