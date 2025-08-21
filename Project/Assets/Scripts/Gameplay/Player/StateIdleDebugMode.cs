using UnityEngine;

using GameDatabase;

public class StateIdleDebugMode : BasePlayerState
{
    public override PlayerState State => PlayerState.IdleDebugMode;

    protected Player player;

    public override void StartState(Player player)
    {
        this.player = player;
        PlayerStateIdleEvents.OnCastSpell += StartCasting;
        PlayerStateIdleEvents.OnCastSpellFromBigScroll += StartCastingSpellFromBigScroll;
        PlayerStateIdleEvents.OnCastSpellFromSmallScroll += StartCastingSpellFromSmallScroll;
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
        if (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.EndTurnOrCancelAction.WasReleasedThisFrame())
        {
            player.DeactivatePlayerAtEndOfTurn();
            gameplayController.SwitchState(GameplayController.States.Enemy);
        }
    }

    public override void EndState(Player player)
    {
        PlayerStateIdleEvents.OnCastSpell -= StartCasting;
        PlayerStateIdleEvents.OnCastSpellFromBigScroll -= StartCastingSpellFromBigScroll;
        PlayerStateIdleEvents.OnCastSpellFromSmallScroll -= StartCastingSpellFromSmallScroll;
    }

    public void StartCasting(int spellIndexInGameDatabase)
    {
        SpellsController.Instance.SetCurrentSpellFromAllGameSpells(Database.Instance.DatabaseSpells.SpellsAll[spellIndexInGameDatabase]);
        Player.Instance.SwitchState(PlayerState.CastingSpellInDebugMode);
    }

    public void StartCastingSpellFromSmallScroll()
    {
        player.SwitchState(PlayerState.CastingSpellFromSmallScrollInDebugMode);
    }

    public void StartCastingSpellFromBigScroll()
    {
        player.SwitchState(PlayerState.CastingSpellFromBigScrollDebugMode);
    }
}
