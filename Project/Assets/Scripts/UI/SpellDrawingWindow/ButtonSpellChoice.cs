using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

public class ButtonSpellChoice : BaseButton
{
    private Image image; 

    private int spellNumber;
    private Action<int> buttonMethod;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }

    protected override void DoThisOnClick()
    {
        StartCoroutine(WaitWithOnClickActionTillButtonReleased());   
    }

    public void Initialize(int spellNumber)
    {
        this.spellNumber = spellNumber;
    }

    public void UpdateButton(Sprite currentImage,   Action<int> buttonMethod)
    {
        image.sprite = currentImage;
        this.buttonMethod = buttonMethod;
    }

    public void UpdateButton(Sprite currentImage)
    {
        image.sprite = currentImage;
    }

    private IEnumerator WaitWithOnClickActionTillButtonReleased()
    {
        yield return new WaitUntil(() => InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
        buttonMethod(spellNumber);
    }
}
