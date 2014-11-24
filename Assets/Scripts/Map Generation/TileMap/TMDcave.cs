using UnityEngine;
using System.Collections.Generic;

public partial class TileMapData
{
    //Note: Removed most refrences to "rooms". May cause bugs.
    //Credit goes to http://www.roguebasin.com/index.php?title=Cellular_Automata_Method_for_Generating_Random_Cave-Like_Levels
    //for algorithm. Read it if you want to understand.
    public void GenCave(int sizeX, int sizeY, float initialWallProb) 
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
            mapData = createFilledMapArray(eTile.Filler);


            caveInitWalls(initialWallProb);        
            caveRunSmoothAutomata(3);
            caveRunFillGapsAutomata(2);
            caveConnectRegions();

            caveMakeSpawn();
            caveMakeGoal();
            good = MapUtilities.isGoodMap (mapData);
            MakeWalls();
        }
        if( attempts > 2)
            Debug.Log("Took " + attempts + " to generate caves");

    }

    private void caveInitWalls(float initialWallProb)
    {



        for(int i = 3; i < (sizeX-2); i++)
            for(int j = 3; j < (sizeY - 2); j++)
            {
                if(Random.Range(0,100) < initialWallProb)
                    mapData[i,j] = eTile.Filler;
                else
                    mapData[i,j] = eTile.Floor;
            }
    }   

    private void caveRunSmoothAutomata(int generations)
    {
        for(int k = 0; k < generations; k++)
        {
            eTile[,] newData = createFilledMapArray(eTile.Filler);

            for(int i = 1; i < sizeX-1; i++)
                for(int j = 1; j < sizeY-1; j++)
                {
                    if(countWallsSquare(i,j,1) >= 5)
                        newData[i,j] = eTile.Filler;
                    else
                        newData[i,j] = eTile.Floor;
                }
            mapData = newData;
        } 
    }

    private void caveRunFillGapsAutomata(int generations)
    {
        for(int k = 0; k < generations; k++)
        {
            eTile[,] newData = createFilledMapArray(eTile.Filler);

            for(int i = 1; i < sizeX-1; i++)
                for(int j = 1; j < sizeY-1; j++)
                {
                    if(countWallsSquare(i,j,1) >= 5 || countWallsSquare(i,j,2) == 0 )
                        newData[i,j] = eTile.Filler;
                    else
                        newData[i,j] = eTile.Floor;
                }
            mapData = newData;
        } 
    }

    private bool caveConnectRegionsIsConnected(eTile[,] tmp)
    {
        for(int i = 0; i < sizeX; i++)
            for(int j = 0; j < sizeY; j++)
            {
                if(tmp[i,j] == eTile.Floor)
                    return false;

            }
        return true;

    }

    private void caveConnectRegions()
    {
		Debug.Log ("Got this far");
        int x = -1;
        int y = -1;
        for(int i = 1; i < sizeX; i++)
            for(int j = 1; j < sizeY; j++)
                if(mapData[i,j] == eTile.Floor)
                {
                    x = i;
                    y = j;
                }

        if(x == -1 || y == -1)
            return;


        //Initial coordinates located at (x,y)

        eTile[,] tmp = copyMapArray();
        bool done = false;
        while (!done)
        {
            tmp[x,y] = eTile.dConnectedFloor;

            if(tmp[x-1,y] == eTile.Floor)
                tmp[x-1,y] = eTile.dConnectedFloor;
            if(tmp[x,y-1] == eTile.Floor)
                tmp[x,y-1] = eTile.dConnectedFloor;
            if(tmp[x,y+1] == eTile.Floor)
                tmp[x,y+1] = eTile.dConnectedFloor;
            if(tmp[x+1,y] == eTile.Floor)
                x++;
            else
                done = true;
        }


        while(!caveConnectRegionsIsConnected(tmp))
        {
            done = false;
            while(!done)
            {
                done = true;
                for(int i = 0; i < sizeX; i ++)
                    for(int j = 0; j < sizeY; j++)
                        if(tmp[i,j] == eTile.dConnectedFloor)
                        {
                            if(tmp[i-1,j] == eTile.Floor )
                            {
                                done = false;
                                tmp[i-1,j] = eTile.dConnectedFloor;
                            }
                            if(tmp[i,j-1] == eTile.Floor)
                            {
                                done = false;
                                tmp[i,j-1] = eTile.dConnectedFloor;
                            }
                            if(tmp[i,j+1] == eTile.Floor)
                            {
                                done = false;
                                tmp[i,j+1] = eTile.dConnectedFloor;
                            }
                            if(tmp[i+1,j] == eTile.Floor)
                            {
                                done = false;
                                tmp[i+1,j] = eTile.dConnectedFloor;
                            }


                        }
            } 
            if(caveConnectRegionsIsConnected(tmp))
                return;

            int srcX = -1;
            int srcY = -1;

            int dstX = -1;
            int dstY = -1;

            //1st connected region is now mapped. Now, we connect this region to the next.
            done = false;
            while(!done)
            {
                done = false;

                for(int i = 0; i < sizeX; i++)
                    for(int j = 0; j < sizeY; j++)
                    {
                        if(tmp[i,j] == eTile.Filler && adjacentTo(tmp,i,j,eTile.dConvertedFiller))
                        {
                            done = true;
                            srcX = i;
                            srcY = j;

                             
                        }
                    }
                if(done)
                    break;

                for(int i = 0; i < sizeX; i++)
                    for(int j = 0; j < sizeY; j++)
                    {
                  
                        if(tmp[i,j] == eTile.Filler && (adjacentTo(tmp,i,j,eTile.dConnectedFloor) || adjacentTo(tmp,i,j,eTile.dConvertedFiller)))
                            tmp[i,j] = eTile.Unknown;
                    }

                for(int i = 0; i < sizeX; i++)
                    for(int j = 0; j < sizeY; j++)
                    {
                        if(tmp[i,j] == eTile.Unknown)
                            tmp[i,j] = eTile.dConvertedFiller;
                    }

            }
            //Found X and Y of closest part of the closest disconnected region. Now, we find the corresponding points on the connected region.
            done = false;
            for(int r = 2; r < sizeX+sizeY && !done; r++)
                for(float t = 0; t < 2*Mathf.PI && !done; t+= Mathf.PI/12)
                {
                    dstX = (int) Mathf.Cos(t)*r;
                    dstY = (int) Mathf.Sin(t)*r; 
                    
                    if(dstX >= 0 && dstY < sizeX && dstY >= 0 && dstY < sizeY)
                        if(tmp[dstX,dstY] == eTile.dConnectedFloor)
                            done = true;
                }

            while(srcX != dstX)
            {
                    mapData[srcX,srcY] = eTile.Floor;
                    mapData[srcX,srcY+1] = eTile.Floor;
                if(srcX<dstX)
                {
                    srcX++;
                }
                else
                {
                    srcX--;
                }
            }
            while(srcY != dstY)
            {
                mapData[srcX,srcY] = eTile.Floor;
                mapData[srcX,srcY] = eTile.Floor;
               if(srcY<dstY)
                   srcY++;
               else
                   srcY--;
            }




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
                    if(mapData[i+x,j+y] == eTile.Filler)
                        numWalls++;
            }
        return numWalls;
    }





    //this method's function was moved to MapUtilities
    /*
       bool isGoodMap()
       {
       int x = -1;
       int y = -1;
       for(int i = 1; i < sizeX; i++)
       for(int j = 0; j < sizeY; j++)
       if(mapData[i,j] == eTile.Player)
       {
       x = i;
       y = j;
       }

       if(x == -1 || y == -1)
       return false;

       eTile[,] tmp = copyMapArray();
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
*/

void caveMakeSpawn()
{
    for(int i = 2; i < sizeX; i++)
        for(int j = 2; j < sizeY; j++)
            if(mapData[i,j] == eTile.Floor)
            {
                mapData[i,j] = eTile.Player;
                return;
            }
}    

void caveMakeGoal()
{
    for(int i = sizeX-1; i > 0; i--)
        for(int j = sizeX-1; j > 0; j--)
            if(mapData[i,j] == eTile.Floor)
            {
                mapData[i,j] = eTile.Goal;
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
