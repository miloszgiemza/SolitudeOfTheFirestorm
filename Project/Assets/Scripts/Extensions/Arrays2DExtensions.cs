using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Arrays2DExtensions
{
    public static bool CheckIfPositionIsWithinBoundsOfArray<T>(MapPosition position, T[,] array)
    {

        return (position.X >= 0 && position.X < array.GetLength(0) && position.Y >= 0 && position.Y < array.GetLength(1));
    }

    public static bool CheckIfPositionIsWithinBoundsOfArray<T>(int x, int y, T[,] array)
    {

        return (x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1));
    }

    public static bool CheckIfPositionInArrayAndWithinGivenY<T>(MapPosition position, T[,] array, int containingY)
    {
        return (position.X >= 0 && position.X < array.GetLength(0) && position.Y >= 0 && position.Y < array.GetLength(1) && position.Y <= containingY);
    }
}
