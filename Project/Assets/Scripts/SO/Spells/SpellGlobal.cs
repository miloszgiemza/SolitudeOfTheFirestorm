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

    public override TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        TooltipParagraph[] description = new TooltipParagraph[1];
        description[0] = new TooltipParagraph();

        string stats = "";

        switch (gameLanguage)
        {
            case GameLanguage.ENG:
                description[0].SetTitle(descriptionEN[0].Title);

                stats += "Cost:  MAIN ACTION\n\n";
                stats += "Area of effect: GLOBAL\n\n";
                stats += "Damage: " + damage.ToString() + "\n\n";

                stats += "Statuses: ";

                if (ReferenceEquals(statuses, null) || statuses.Count < 1)
                {
                    stats += "None\n\n";
                }
                else
                {
                    stats += "\n\n";

                    for (int i = 0; i < statuses.Count; i++)
                    {
                        stats += "Affected attribute: " + statuses[i].Attribute.ToString() + "\n";
                        stats += "Applied modifier: " + statuses[i].Modifier.ToString() + "\n";
                        stats += "Duration (turns): " + statuses[i].Duration.ToString() + "\n\n";
                    }
                }

                description[0].SetText(stats + "\n\n" + descriptionEN[0].Text);
                break;

            case GameLanguage.PL:
                description = descriptionPL;
                break;
        }

        return description;
    }
}
