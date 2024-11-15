using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateVictory : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.Victory;

    public override void StartTurn(GameplayController game)
    {
        if (!ReferenceEquals(game.OnVictory, null)) game.OnVictory.Invoke();
    }

    public override void RunUpdate(GameplayController game)
    {
    }

    public override void EndTurn(GameplayController game)
    {
    }
}
