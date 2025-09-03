using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGameLevelIntro : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.LevelIntro;

    private int enemiesWavesToSpawn = 5;
    private float enemiesWavesDelay = 0.8f;

    private float timer = 0f;

    private int enemiesMaxSpeedDuringIntro = 1;

    public override void StartTurn(GameplayController game)
    {
    }

    public override void RunUpdate(GameplayController game)
    {
        SpawnEnemiesWaves(game);
    }

    public override void EndTurn(GameplayController game)
    {
    }

    private void SpawnEnemiesWaves(GameplayController game)
    {
        if(timer < enemiesWavesDelay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;

            if (enemiesWavesToSpawn > 0)
            {
                EnemiesController.Instance.RunEnemies(game, enemiesMaxSpeedDuringIntro);
                game.Spawner.RunSpawner(Map.Instance, Map.Instance.EnemiesSpawningRowOnY, Map.Instance.MapXSize, Map.Instance.MapYSize, LevelLoader.Instance.EnemiesPreset);

                EnemiesDisplayControler.Instance.Display(EnemiesController.Instance.EnemiesOnMap);
                EnemiesNormalAtributesUIDisplayController.Instance.UpdateUI(Map.Instance.MapData);

                enemiesWavesToSpawn--;
            }
            else
            {
                game.DisableRaycastBlockers();
                game.SwitchState(GameplayController.States.Player);
            }
        }
    }
}
