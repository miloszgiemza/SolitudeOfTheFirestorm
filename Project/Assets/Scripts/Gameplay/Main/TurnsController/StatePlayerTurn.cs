using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerTurn : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.Player;

    public override void StartTurn(GameplayController game)
    {
        //w³¹cz ca³e ui gracza i odblokuj jego akcje
        ObstaclesController.Instance.RunObstacles();
        game.Player.StartPlayerTurn();
    }

    public override void RunUpdate(GameplayController game)
    {
        game.Player.RunUpdate(game);
    }

    public override void EndTurn(GameplayController game)
    {
        if(game.Player.CurrentState.State == PlayerState.Idle || game.Player.CurrentState.State == PlayerState.IdleDebugMode)
        {
            game.Player.DeactivatePlayerAtEndOfTurn();
            game.SwitchState(GameplayController.States.Enemy);

            PlayerInventoryUIController.Instance.HideWindow();
            


            EnemiesController.Instance.RunEnemiesActionAtEndOfPlayerTurn();
        }
        /*
        if (game.CheckIfPlayerWon(game.Spawner.CheckIfAllWavesSpawned(LevelLoader.Instance.EnemiesPreset), EnemiesController.Instance.CheckIfNoEnemiesOnMap(), Player.Instance.Health))
        {
            game.SwitchState(GameplayController.States.Victory);
        }
        else
        {
            game.SwitchState(GameplayController.States.Enemy);
        }
        */
    }
}
