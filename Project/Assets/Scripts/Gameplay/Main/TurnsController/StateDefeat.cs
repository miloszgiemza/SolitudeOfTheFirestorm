using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDefeat : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.Defeat;

    public override void StartTurn(GameplayController game)
    {
        if (!ReferenceEquals(game.OnDefeat, null)) game.OnDefeat.Invoke();
    }

    public override void RunUpdate(GameplayController game)
    {
    }

    public override void EndTurn(GameplayController game)
    {
    }
}
