using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInvertSpellsAreaOfEffect : BaseButton, IReturnObjectDataForTooltip
{
    [SerializeField] protected TooltipParagraph[] tooltipText; 

    protected override void DoThisOnClick()
    {
        SpellsController.Instance.InvertAreaOfEffect();
    }

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        return tooltipText;
    }
}
