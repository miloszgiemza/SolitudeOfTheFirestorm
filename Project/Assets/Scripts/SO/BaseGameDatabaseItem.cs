using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReturnObjectDataForTooltip
{
    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage);
}

[Serializable]
public class TooltipParagraph
{
    public string Title => title;
    public string Text => text;

    [SerializeField] string title;
    [SerializeField] string text;
}

public enum DatabaseItemType
{
    Spell,
    Item
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

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
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
