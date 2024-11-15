using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class StateIdle : BasePlayerState
{
    public override PlayerState State => PlayerState.Idle;

    protected Player player;

    public override void StartState(Player player)
    {
        this.player = player;
        PlayerStateIdleEvents.OnCastSpell += StartCasting;
        PlayerStateIdleEvents.OnCastSpellFromBigScroll += StartCastingSpellFromBigScroell;
        PlayerStateIdleEvents.OnCastSpellFromSmallScroll += StartCastingSpellFromSmallScroll;
    }

    public override void EndState(Player player)
    {
        PlayerStateIdleEvents.OnCastSpell -= StartCasting;
        PlayerStateIdleEvents.OnCastSpellFromBigScroll -= StartCastingSpellFromBigScroell;
        PlayerStateIdleEvents.OnCastSpellFromSmallScroll -= StartCastingSpellFromSmallScroll;
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
        if(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.EndTurnOrCancelAction.WasReleasedThisFrame())
        {
            player.DeactivatePlayerAtEndOfTurn();
            gameplayController.SwitchState(GameplayController.States.Enemy);
        }
    }

    public void StartCasting()
    {
        player.SwitchState(PlayerState.Casting);
    }

    public void StartCastingSpellFromBigScroell()
    {
        player.SwitchState(PlayerState.CastingSpellFromBigScroll);
    }

    public void StartCastingSpellFromSmallScroll()
    {
        player.SwitchState(PlayerState.CastingSpellFromSmallScroll);
    }
}

public static class PlayerStateIdleEvents
{
    public static Action OnCastSpell;
    public static Action OnCastSpellFromBigScroll;
    public static Action OnCastSpellFromSmallScroll;
}
