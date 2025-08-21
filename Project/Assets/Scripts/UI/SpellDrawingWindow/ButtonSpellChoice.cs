using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using TMPro;

public class ButtonSpellChoice : BaseButton, IReturnObjectDataForTooltip
{
    private Image image;
    private TextMeshProUGUI textUI;

    private int spellNumber;
    private Action<int> buttonMethod;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        textUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void DoThisOnClick()
    {
        StartCoroutine(WaitWithOnClickActionTillButtonReleased());   
    }

    public void Initialize(int spellNumber)
    {
        this.spellNumber = spellNumber;
    }

    public void UpdateButton(Sprite currentImage,   Action<int> buttonMethod, string text)
    {
        image.sprite = currentImage;
        this.buttonMethod = buttonMethod;
        this.textUI.text = text;
    }

    public void UpdateButton(Sprite currentImage)
    {
        image.sprite = currentImage;
    }

    private IEnumerator WaitWithOnClickActionTillButtonReleased()
    {
        yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
        //buttonMethod(spellNumber);
        if(!ReferenceEquals(PlayerStateIdleEvents.OnCastSpell, null))
        {
            PlayerStateIdleEvents.OnCastSpell.Invoke(spellNumber);
        }
    }

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        Debug.Log("Spell to: " + SpellsController.Instance.SpellsAvaliableForThisTurn[spellNumber].SpellName);
        return SpellsController.Instance.SpellsAvaliableForThisTurn[spellNumber].ReturnTooltipText(GameController.Instance.GameLanguage);
    }
}
