using UnityEngine;
using System.Collections.Generic;

public partial class TileMapData
{
    //Note: Removed most refrences to "rooms". May cause bugs.
    //Credit goes to http://www.roguebasin.com/index.php?title=Cellular_Automata_Method_for_Generating_Random_Cave-Like_Levels
    //for algorithm. Read it if you want to understand.
    public void GenCave(int sizeX, int sizeY, float initialWallProb = 40) 
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;


        if(sizeX < 10 || sizeY < 10)
            Debug.Log("Error! Map for GenCave is too small");

        int attempts = 0;
        bool good = false;
        while(!good)
        {
        attempts++;
        mapData = createFilledMapArray(2);


        caveInitWalls(initialWallProb);        
        caveRunSmoothAutomata();
        caveRunFillGapsAutomata();

        caveMakeSpawn();
        caveMakeGoal();
        good = isGoodMap();
        }
        if( attempts > 2)
            Debug.Log("Took " + attempts + " to generate caves");

    }

    private void caveInitWalls(float initialWallProb = 40)
    {



        for(int i = 3; i < (sizeX-2); i++)
            for(int j = 3; j < (sizeY - 2); j++)
            {
                if(Random.Range(0,100) < initialWallProb)
                    mapData[i,j] = 2;
                else
                    mapData[i,j] = 1;
            }
    }   

    private void caveRunSmoothAutomata(int generations = 3)
    {
        for(int k = 0; k < generations; k++)
        {
            int[,] newData = createFilledMapArray(2);

            for(int i = 1; i < sizeX-1; i++)
                for(int j = 1; j < sizeY-1; j++)
                {
                    if(countWallsSquare(i,j,1) >= 5)
                        newData[i,j] = 2;
                    else
                        newData[i,j] = 1;
                }
            mapData = newData;
        } 
    }

    private void caveRunFillGapsAutomata(int generations = 2)
    {
        for(int k = 0; k < generations; k++)
        {
            int[,] newData = createFilledMapArray(2);

            for(int i = 1; i < sizeX-1; i++)
                for(int j = 1; j < sizeY-1; j++)
                {
                    if(countWallsSquare(i,j,1) >= 5 || countWallsSquare(i,j,2) == 0 )
                        newData[i,j] = 2;
                    else
                        newData[i,j] = 1;
                }
            mapData = newData;
        } 
    }
    /*
       private int countWallsSteps(int x, int y, int steps)
       {
       int numWalls = 0;
       for( int i = -steps; i <= steps; i++)
       for( int j = -steps; j <= steps; j++)
       {
       if( i+x > 0 && i+x < sizeX && j+y > 0 && j+y < sizeY)
       if(Math.Abs(i) + Math.Abs(j) <= steps)
       if(mapData[i+x,j+y] == 2)
       numWalls++;

       }
       return numWalls;
       }
       */
    private int countWallsSquare(int x, int y, int offset)
    {
        int numWalls = 0;
        for( int i = -offset; i <= offset; i++)
            for( int j = -offset; j <= offset; j++)
            {
                if( i+x >= 0 && i+x < sizeX && j+y >= 0 && j+y < sizeY)
                    if(mapData[i+x,j+y] == 2)
                        numWalls++;
            }
        return numWalls;
    }

    bool isGoodMap()
    {
        int x = -1;
        int y = -1;
        for(int i = 1; i < sizeX; i++)
            for(int j = 0; j < sizeY; j++)
                if(mapData[i,j] == 4)
                {
                    x = i;
                    y = j;
                }

        if(x == -1 || y == -1)
            return false;

        int[,] tmp = copyMapArray();
        bool done = false;
        while (!done)
        {
            tmp[x,y] = -1;

            if(tmp[x-1,y] == 1)
                tmp[x-1,y] = -1;
            if(tmp[x,y-1] == 1)
                tmp[x,y-1] = -1;
            if(tmp[x,y+1] == 1)
                tmp[x,y+1] = -1;
            if(tmp[x+1,y] == 1)
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
                  if(tmp[i,j] == -1)
                  {
                      if(tmp[i-1,j] == 1)
                      {
                          done = false;
                          tmp[i-1,j] = -1;
                      }
                      if(tmp[i,j-1] == 1)
                      {
                          done = false;
                          tmp[i,j-1] = -1;
                      }
                      if(tmp[i,j+1] == 1)
                      {
                          done = false;
                          tmp[i,j+1] = -1;
                      }
                      if(tmp[i+1,j] == 1)
                      {
                          done = false;
                          tmp[i+1,j] = -1;
                      }

                      if(tmp[i-1,j] == 5)
                          return true;
                      if(tmp[i,j-1] == 5)
                          return true;
                      if(tmp[i,j+1] == 5)
                          return true;
                      if(tmp[i+1,j] == 5)
                          return true;

                  }
      } 
      return false;

    }

    void caveMakeSpawn()
    {
        for(int i = 2; i < sizeX; i++)
            for(int j = 2; j < sizeY; j++)
                if(mapData[i,j] == 1)
                {
                    mapData[i,j] = 4;
                    return;
                }
    }    

    void caveMakeGoal()
    {
        for(int i = sizeX-1; i > 0; i--)
            for(int j = sizeX-1; j > 0; j--)
                if(mapData[i,j] == 1)
                {
                    mapData[i,j] = 5;
                    return;
                }
    }    
    //	void MakeCorridor(RoomData r1, RoomData r2)//moves in y first then x direction eventually make other corridor types

    //	void MakeWalls()

    //	bool HasAdjacentFloors(int x, int y)
    /*	
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
        */
}
