using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGameIdle : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.Idle;

    public override void StartTurn(GameplayController game)
    {
    }

    public override void RunUpdate(GameplayController game)
    {
    }

    public override void EndTurn(GameplayController gameplayController)
    {
    }
}
