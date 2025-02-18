using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spells/SpellGlobal", fileName = "NewSpellGlobal")]
public class SpellGlobal : BaseSpell
{
    public override List<Tile> ReturnAffectedTiles(Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        List<Tile> affectedTiles = new List<Tile>();

        for(int x=0; x < mapData.GetLength(0); x++)
        {
            for(int y=0; y < mapData.GetLength(1); y++)
            {
                affectedTiles.Add(mapData[x, y]);
            }
        }

        return affectedTiles;
    }

    public override List<MapPosition> ReturnAffectedTilesPositions(Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        List<MapPosition> affectedTilesPositions = new List<MapPosition>();

        for (int x = 0; x < mapData.GetLength(0); x++)
        {
            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                affectedTilesPositions.Add(mapData[x, y].Position);
            }
        }

        return affectedTilesPositions;
    }

    public override bool TryCast(Tile[,] mapData, Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength)
    {
        List<Tile> affectedTiles = ReturnAffectedTiles(mapData, cursorPos, modifierRange);

        for (int currentTile = 0; currentTile < affectedTiles.Count; currentTile++)
        {
            affectedTiles[currentTile].MakeObjectsOnTileReact(this, ReturnDamage(), modifierDamage, modifierRange, modifierEffectLength);
        }

        return true;
    }

    public override List<MapPosition> ReturnTilesInRange(Tile[,] mapData, int modifierRange)
    {
        return new List<MapPosition>();
    }

    public override int ReturnDamage()
    {
        return damage;
    }

    public override int ReturnDamage(Tile tile, Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        return damage;
    }
}
