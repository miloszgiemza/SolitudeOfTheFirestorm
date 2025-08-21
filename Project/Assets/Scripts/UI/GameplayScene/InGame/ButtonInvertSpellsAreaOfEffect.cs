using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInvertSpellsAreaOfEffect : BaseButton
{
    [SerializeField] protected TooltipParagraph[] tooltipText; 

    protected override void DoThisOnClick()
    {
        SpellsController.Instance.InvertAreaOfEffect();
    }
}
