using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGampelayObjectDataAndStats : BaseGameDatabaseItem
{
    public Sprite Image => image;
    public bool Walkable => walkable;
    public int Damage => damage;

    [SerializeField] protected Sprite image;
    [SerializeField] protected bool walkable = false;
    [SerializeField] protected int damage = 1;

    public override TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        TooltipParagraph[] description = new TooltipParagraph[0];

        switch (gameLanguage)
        {
            case GameLanguage.ENG:
                description = descriptionEN;
                break;

            case GameLanguage.PL:
                description = descriptionPL;
                break;
        }

        return description;
    }
}
