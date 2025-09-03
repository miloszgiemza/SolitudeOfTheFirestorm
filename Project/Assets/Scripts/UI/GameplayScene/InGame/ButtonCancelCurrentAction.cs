using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCancelCurrentAction : BaseButton
{
    protected override void DoThisOnClick()
    {
        Player.Instance.CancelCurrentAction();
    }
}
