using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellAreaOfEffect : BaseSpell
{
    public MapPosition[] AreaOfEffect => areaOfEffect;

    [SerializeField] protected MapPosition[] areaOfEffect;

    [SerializeField] protected int rangeY = 10;

    public static bool CheckIfPositionWithinMapAndSpellRange(MapPosition position, Tile[,] mapData, int rangeY, int modifierRange)
    {
        return (position.X >= 0 && position.X < mapData.GetLength(0) && position.Y >= 0 && position.Y < mapData.GetLength(1) && position.Y <= rangeY + modifierRange);
    }

    public static bool CheckIfPositionWithinMapAndSpellRange(int x, int y, Tile[,] mapData, int rangeY, int modifierRange)
    {
        return (x >= 0 && x < mapData.GetLength(0) && y >= 0 && y < mapData.GetLength(1) && y <= rangeY + modifierRange);
    }

    public override List<MapPosition> ReturnTilesInRange(Tile[,] mapData, int modifierRange)
    {
        List<MapPosition> tilesInRange = new List<MapPosition>();

        for (int x = 0; x < mapData.GetLength(0); x++)
        {
            for (int y = 0; y < mapData.GetLength(1) && y <= rangeY + modifierRange; y++)
            {
                tilesInRange.Add(mapData[x, y].Position);
            }
        }

        return tilesInRange;
    }

    protected MapPosition[] ReturnRotatedUpToDateAreaOfEffect(bool inverted)
    {
        MapPosition[] invertedAreaOfEffect = areaOfEffect;

        if(inverted)
        {
            invertedAreaOfEffect = new MapPosition[areaOfEffect.Length];

            for(int i = 0; i < areaOfEffect.Length; i++)
            {
                invertedAreaOfEffect[i] = new MapPosition(-areaOfEffect[areaOfEffect.Length - 1 - i].X, -areaOfEffect[areaOfEffect.Length - 1 - i].Y);
            }
        }

        return invertedAreaOfEffect;
    }
}
