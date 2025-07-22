using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEndTurn : BaseButton
{
    private GameplayController gameplayController;

    protected override void Awake()
    {
        base.Awake();
        gameplayController = GetComponentInParent<GameplayController>();
    }

    protected override void DoThisOnClick()
    {
        StartCoroutine(WaitTillButtonReleased());
    }

    protected IEnumerator WaitTillButtonReleased()
    {
        yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
        gameplayController.CurrentState.EndTurn(gameplayController);
    }
}
