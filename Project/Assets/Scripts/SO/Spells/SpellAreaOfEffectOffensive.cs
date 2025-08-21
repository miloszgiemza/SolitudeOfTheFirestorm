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
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.X, spellPosition.Y 
                    + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.Y, mapData, rangeY, modifierRange))
                {
                    affectedTiles.Add(mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.X, spellPosition.Y 
                        + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.Y]);
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
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.X, 
                    spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.Y, mapData, rangeY, modifierRange))
                {
                    affectedTilesPositions.Add((mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.X, spellPosition.Y 
                        + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.Y]).Position);
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
                affectedTiles[currentTile].MakeObjectsOnTileReact(this, ReturnDamage(affectedTiles[currentTile], mapData, cursorPos, modifierRange), modifierDamage, modifierRange, modifierEffectLength);
            }
        }

        return spellCastedSuccesfully;
    }

    public override int ReturnDamage(Tile tile, Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        int damage = 0;

        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos).X >= 0)
        {
            
            MapPosition spellPosition = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

            for (int spellCurrentDirection = 0; spellCurrentDirection < ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect).Length; spellCurrentDirection++)
            {
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.X, spellPosition.Y
                    + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.Y, mapData, rangeY, modifierRange))
                {
                    if (tile.Position.X == (spellPosition.X + ReturnRotatedUpToDateAreaOfEffect( SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.X) &&
                        tile.Position.Y == (spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Position.Y))
                    {
                        //damage = areaOfEffect[spellCurrentDirection].Damage;
                        damage = ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Damage;
                    }
                }
            }
        }

        return damage;
    }

    public override int ReturnDamage()
    {
        return 0;
    }

    public override TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        TooltipParagraph[] description = new TooltipParagraph[1];
        description[0] = new TooltipParagraph();

        string stats = "";

        switch (gameLanguage)
        {
            case GameLanguage.ENG:
                description[0].SetTitle(descriptionEN[0].Title);

                stats += "Cost: MAIN ACTION\n\n";
                stats += "Range: " + rangeY.ToString() + "\n\n";
                stats += "Damage:\n";

                int[,] shape = ReturnAreaOfEffectShapeAndDamageForTooltip();
                

                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    stats += "\n";
                    for(int j=0; j < shape.GetLength(1); j++)
                    {
                        if (shape[i, j] != 0) stats += shape[i, j].ToString();
                        else stats += " ";
                    }
                }

                stats += "\n\n";

                stats += "Statuses: ";

                if(ReferenceEquals(statuses, null) || statuses.Count < 1)
                {
                    stats += "None\n\n";
                }
                for(int i=0; i < statuses.Count; i++)
                {
                    stats += "\n\n";
                    stats += "Affected attribute: " + statuses[i].Attribute.ToString() + "\n";
                    stats += "Applied modifier: " + statuses[i].Modifier.ToString() + "\n";
                    stats += "Duration (turns): " + statuses[i].Duration.ToString() + "\n\n";
                }

               

                description[0].SetText(stats + "\n\n" + descriptionEN[0].Text);
                break;

            case GameLanguage.PL:
                description = descriptionPL;
                break;
        }

        return description;
    }

    public int[,] ReturnAreaOfEffectShapeAndDamageForTooltip()
    {
        int[,] shape = new int[0, 0];

        int maxPointY = int.MinValue;
        int minPointY = int.MaxValue;
        int maxPointX = int.MinValue;
        int minPointX = int.MaxValue;

        for (int i = 0; i < areaOfEffect.Length; i++)
        {
            if (areaOfEffect[i].Position.Y > maxPointY) maxPointY = areaOfEffect[i].Position.Y;
            if (areaOfEffect[i].Position.Y < minPointY)
            {
                minPointY = areaOfEffect[i].Position.Y;
            }
            if (areaOfEffect[i].Position.X > maxPointX) maxPointX = areaOfEffect[i].Position.X;
            if (areaOfEffect[i].Position.X < minPointX)
            {
                minPointX = areaOfEffect[i].Position.X;
            }
        }

        int arrayHeight = Mathf.Abs(maxPointY - minPointY) + 1;
        int arrayWidth = Mathf.Abs(maxPointX - minPointX) + 1;

        shape = new int[arrayHeight, arrayWidth];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                shape[i, j] = 0;
            }
        }

        for (int i = 0; i < areaOfEffect.Length; i++)
        {
            shape[areaOfEffect[i].Position.Y, areaOfEffect[i].Position.X] = areaOfEffect[i].Damage;
        }

        return shape;
    }
}
