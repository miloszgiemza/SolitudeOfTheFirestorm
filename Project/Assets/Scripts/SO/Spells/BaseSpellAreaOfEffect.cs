using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellAreaOfEffectPoint
{
    public MapPosition Position => position;
    public int Damage => damage;

    [SerializeField] protected MapPosition position;
    [SerializeField] protected int damage;

    public SpellAreaOfEffectPoint(MapPosition mapPosition, int damage)
    {
        position = mapPosition;
        this.damage = damage;
    }
}


public abstract class BaseSpellAreaOfEffect : BaseSpell
{
    public SpellAreaOfEffectPoint[] AreaOfEffect => areaOfEffect;

    [SerializeField] protected SpellAreaOfEffectPoint[] areaOfEffect;

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

    protected SpellAreaOfEffectPoint[] ReturnRotatedUpToDateAreaOfEffect(bool inverted)
    {
        SpellAreaOfEffectPoint[] invertedAreaOfEffect = areaOfEffect;

        if(inverted)
        {
            invertedAreaOfEffect = new SpellAreaOfEffectPoint[areaOfEffect.Length];

            for(int i = 0; i < areaOfEffect.Length; i++)
            {
                invertedAreaOfEffect[i] = new SpellAreaOfEffectPoint(new MapPosition(-areaOfEffect[areaOfEffect.Length - 1 - i].Position.X, -areaOfEffect[areaOfEffect.Length - 1 - i].Position.Y), areaOfEffect[areaOfEffect.Length - 1 - i].Damage);
            }
        }

        return invertedAreaOfEffect;
    }
}
