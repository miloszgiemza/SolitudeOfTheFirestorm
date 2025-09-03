using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCastSpell : BaseButton
{
    private GameplayController gameplayController;

    protected override void Awake()
    {
        base.Awake();
        gameplayController = GetComponentInParent<GameplayController>();
    }

    protected override void DoThisOnClick()
    {
        StartCoroutine(WaitWithOnClickActionTillButtonReleased());
    }

    private IEnumerator WaitWithOnClickActionTillButtonReleased()
    {
        yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
        if(!ReferenceEquals(PlayerStateIdleEvents.OnCastSpell, null))
        {
           // PlayerStateIdleEvents.OnCastSpell.Invoke();
        }
    }
}
