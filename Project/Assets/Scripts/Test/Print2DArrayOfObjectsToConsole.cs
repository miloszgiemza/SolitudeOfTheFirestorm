using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Print2DArrayOfObjectsToConsole
{
    private static string[,] currentMapState;

    public static void Print<TDataType>(TDataType[,] array)
    {
        currentMapState = new string[array.GetLength(0), array.GetLength(1)];

        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                if (ReferenceEquals(array[x, y], null))
                {
                    currentMapState[x, y] = "0 ";
                }
                else
                {
                    currentMapState[x, y] = "E ";
                }
            }
        }

        Debug.Log("Start: ");
        for (int y = 0; y < currentMapState.GetLength(1); y++)
        {
            string row = "";
            for (int x = 0; x < currentMapState.GetLength(0); x++)
            {
                row = row + " " + currentMapState[x, y];
            }
            Debug.Log(row);
        }
        Debug.Log("End");
    }
}
