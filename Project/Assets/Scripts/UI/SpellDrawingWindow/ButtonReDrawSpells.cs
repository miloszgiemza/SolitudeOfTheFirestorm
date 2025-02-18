using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using TMPro;

public class ButtonReDrawSpells : BaseButton
{
    private Action DoThis;
    private TextMeshProUGUI text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void DoThisOnClick()
    {
        DoThis();
    }

    public void UpdateButtonMethod(Action ReDrawSpells)
    {
        DoThis = ReDrawSpells;
        text.text = "ReDraws\n" + SpellsController.Instance.SpellReDraws.ToString() + " Left"; 
    }
}
