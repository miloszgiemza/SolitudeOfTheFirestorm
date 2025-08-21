using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemiesTurn : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.Enemy;

    public override void StartTurn(GameplayController game)
    {
        EnemiesController.Instance.RunEnemies(game);


        game.Spawner.RunSpawner(Map.Instance, Map.Instance.EnemiesSpawningRowOnY, Map.Instance.MapXSize, Map.Instance.MapYSize, LevelLoader.Instance.EnemiesPreset);

        EnemiesDisplayControler.Instance.Display(EnemiesController.Instance.EnemiesOnMap);
        EnemiesNormalAtributesUIDisplayController.Instance.UpdateUI(Map.Instance.MapData);

        if(game.DebugModeOn)
        {
            game.SwitchState(GameplayController.States.Player);
        }
        else
        {
            if (game.CheckIfPlayerWon(game.Spawner.CheckIfAllWavesSpawned(LevelLoader.Instance.EnemiesPreset), EnemiesController.Instance.CheckIfNoEnemiesOnMap(), Player.Instance.Health))
            {
                game.SwitchState(GameplayController.States.Victory);
            }
            else if (CheckIfPlayerLost(Player.Instance.Health))
            {
                game.SwitchState(GameplayController.States.Defeat);
            }
            else
            {
                game.SwitchState(GameplayController.States.Player);
            }
        }
    }

    public override void RunUpdate(GameplayController game)
    {
    }

    public override void EndTurn(GameplayController game)
    {
    }

    private bool CheckIfPlayerLost(int playerHealth)
    {
        bool playerLost = false;
        if (playerHealth <= 0) playerLost = true;
        return playerLost;
    }
}
