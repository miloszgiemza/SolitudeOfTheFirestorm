using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShowHideInventory : BaseButton
{
    protected override void DoThisOnClick()
    {
        PlayerInventoryUIController.Instance.ShowHideWindow();
    }
}
