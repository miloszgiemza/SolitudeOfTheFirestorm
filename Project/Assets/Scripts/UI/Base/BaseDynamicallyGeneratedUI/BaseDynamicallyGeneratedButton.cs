using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseDynamicallyGeneratedButton : BaseButton
{
    protected Image image;

    protected Action<int> buttonMethod;

    protected int buttonNumber;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }

    public virtual void Initialize(int buttonNumber, Sprite buttonIcon, Action<int> buttonMethod)
    {
        image.sprite = buttonIcon;
        this.buttonMethod = buttonMethod;
    }

    protected override void DoThisOnClick()
    {
        StartCoroutine(WaitWithOnClickActionTillButtonReleased());
    }

    protected virtual IEnumerator WaitWithOnClickActionTillButtonReleased()
    {
        yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
        buttonMethod(buttonNumber);
    }
}
