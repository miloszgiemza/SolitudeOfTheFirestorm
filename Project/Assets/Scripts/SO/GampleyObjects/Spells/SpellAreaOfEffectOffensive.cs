using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellID
{
    Fireball
}

[CreateAssetMenu(menuName = "ScriptableObjects/Spells/SpellAreaOfEffect", fileName ="NewSpellAreaOfEffect")]
public class SpellAreaOfEffectOffensive : BaseSpellAreaOfEffect
{
    public override List<Tile> ReturnAffectedTiles(Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
       List<Tile> affectedTiles = new List<Tile>();


        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos).X >= 0)
        {
            MapPosition spellPosition = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

            for (int spellCurrentDirection = 0; spellCurrentDirection < ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect).Length; spellCurrentDirection++)
            {
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, spellPosition.Y 
                    + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y, mapData, rangeY, modifierRange))
                {
                    affectedTiles.Add(mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, spellPosition.Y 
                        + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y]);
                }
            }
        }

        return affectedTiles;
    }

    public override List<MapPosition> ReturnAffectedTilesPositions(Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        List<MapPosition> affectedTilesPositions = new List<MapPosition>();

        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos).X >= 0)
        {
            MapPosition spellPosition = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

            for (int spellCurrentDirection = 0; spellCurrentDirection < ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect).Length; spellCurrentDirection++)
            {
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, 
                    spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y, mapData, rangeY, modifierRange))
                {
                    affectedTilesPositions.Add((mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, spellPosition.Y 
                        + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y]).Position);
                }
            }
        }

        return affectedTilesPositions;
    }

    public override bool TryCast(Tile[,] mapData, Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength)
    {
        bool spellCastedSuccesfully = false;

        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos).X >= 0
            && CheckIfPositionWithinMapAndSpellRange(GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos).X, GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos).Y, 
            Map.Instance.MapData, rangeY, modifierRange))
        {
            spellCastedSuccesfully = true;

            List<Tile> affectedTiles = ReturnAffectedTiles(mapData, cursorPos, modifierRange);

            for (int currentTile = 0; currentTile < affectedTiles.Count; currentTile++)
            {
                affectedTiles[currentTile].MakeObjectsOnTileReact(this, modifierDamage, modifierRange, modifierEffectLength);
            }
        }

        return spellCastedSuccesfully;
    }
}
