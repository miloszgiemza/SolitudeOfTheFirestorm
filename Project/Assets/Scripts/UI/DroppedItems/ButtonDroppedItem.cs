using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDroppedItem : BaseDynamicallyGeneratedButton, IReturnObjectDataForTooltip
{
    public void Initialize(int buttonNumber, Sprite buttonIcon)
    {
        image.sprite = buttonIcon;
        this.buttonMethod = buttonMethod;
    }

    protected override IEnumerator WaitWithOnClickActionTillButtonReleased()
    {
        yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
        if(!ReferenceEquals(PlayerStateIdleEvents.OnPickUpItem, null))
        {
            PlayerStateIdleEvents.OnPickUpItem.Invoke(DroppedItemsController.Instance.ReturnItemToPlayerInventory(buttonNumber));
        }
    }

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        return DroppedItemsController.Instance.ReturnTooltipText(GameController.Instance.GameLanguage, buttonNumber);
    }
}
