using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChooseSpellForThisTurn : BasePlayerState
{
    public override PlayerState State => PlayerState.ChooseSpellForThisTurn;

    private Player player;

    public override void StartState(Player player)
    {
        this.player = player;
        SpellsController.Instance.DrawSpellsForThisAndNextRound();
        SpellDrawingUIController.Instance.ShowWindowAndUpdateButtons(ChooseSpellForThisTurn, ReDrawSpells, SpellsController.Instance.AllAvaliableSpells);
        NextTurnSpellsDisplayer.Instance.ShowAndUpdate(SpellsController.Instance.SpellsAvaliableForNextTurn);

        if(Player.Instance.GameplayController.DebugModeOn) player.SwitchState(PlayerState.IdleDebugMode);
        else player.SwitchState(PlayerState.Idle);
    }

    public override void EndState(Player player)
    {
        //SpellDrawingUIController.Instance.HideWindow();
        //NextTurnSpellsDisplayer.Instance.Hide();
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
    }

    public void ChooseSpellForThisTurn(int chosenSpell)
    {
        SpellsController.Instance.SetCurrentSpellForThisTurnFromUnlockedSpells(chosenSpell);
        //this.player.SwitchState(PlayerState.Casting);
    }

    public void ReDrawSpells()
    {
        SpellsController.Instance.ReDrawSpells();
        SpellDrawingUIController.Instance.ShowWindowAndUpdateButtons(ChooseSpellForThisTurn, ReDrawSpells, SpellsController.Instance.AllAvaliableSpells);
    }
}
