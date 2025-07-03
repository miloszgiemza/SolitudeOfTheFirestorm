using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGampelayObjectDataAndStats : ScriptableObject
{
    public Sprite Image => image;
    public bool Walkable => walkable;
    public int Damage => damage;

    public TooltipParagraph[] DescriptionEN => descriptionEN;
    public TooltipParagraph[] DescriptionPL => descriptionPL; 

    [SerializeField] protected Sprite image;
    [SerializeField] protected bool walkable = false;
    [SerializeField] protected int damage = 1;

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
