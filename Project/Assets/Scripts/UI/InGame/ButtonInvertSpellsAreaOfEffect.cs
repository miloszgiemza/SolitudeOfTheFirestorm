using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInvertSpellsAreaOfEffect : BaseButton
{
    protected override void DoThisOnClick()
    {
        SpellsController.Instance.InvertAreaOfEffect();
    }
}
