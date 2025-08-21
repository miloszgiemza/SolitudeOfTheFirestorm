using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DatabaseItemType
{
    Spell,
    Item,
    EnemyTypeData
}

public abstract class BaseGameDatabaseItem : ScriptableObject, IReturnObjectDataForTooltip
{
    public string IDGameDatabase => iDGameDatabase;

    public abstract DatabaseItemType DatabaseItemType { get; }

    public TooltipParagraph[] DescriptionEN => descriptionEN;
    public TooltipParagraph[] DescriptionPL => descriptionPL;

    [SerializeField][DatabaseIDField] protected string iDGameDatabase;

    [SerializeField] protected TooltipParagraph[] descriptionEN;
    [SerializeField] protected TooltipParagraph[] descriptionPL;

    public virtual TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
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
