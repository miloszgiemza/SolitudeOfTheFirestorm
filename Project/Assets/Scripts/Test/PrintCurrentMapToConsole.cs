using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class PrintCurrentMapToConsole
{
    /*
    private static string[,] currentMapState;

    public static void PrintMap(Tile[,] map)
    {
        currentMapState = new string[map.GetLength(0), map.GetLength(1)];

        for(int x = 0; x < map.GetLength(0); x++)
        {
            for(int y =0; y < map.GetLength(1); y++)
            {
                if(ReferenceEquals(map[x, y].ObjectOnTile, null))
                {
                    currentMapState[x, y] = "0 ";
                }
                else
                {
                    currentMapState[x, y] = "E ";
                }
            }
        }

        Debug.Log("Current map state: ");
        for(int y = 0; y < currentMapState.GetLength(1); y++)
        {
            string row = "";
            for (int x = 0; x < currentMapState.GetLength(0); x++)
            {
                row = row + " " + currentMapState[x, y]; 
            }
            Debug.Log(row);
        }
    }
    */
}
