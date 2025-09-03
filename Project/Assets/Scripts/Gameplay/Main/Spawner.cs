using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Spawner : MonoBehaviour
{
    private int currentWave = 0;

    private bool CheckForPlaceToSpawn(int mapSpawnYRow, int mapXSzie, Tile[,] mapData, GameplayObject[,] waves)
    {
        bool avaliable = true;
        
        for(int x = 0; x < mapXSzie && avaliable; x++)
        {
            if(!ReferenceEquals(waves[x, currentWave], null))
            {
                switch (waves[x, currentWave].GameplayObjectType)
                {
                    case GameplayObjectType.Enemy:

                        if (!mapData[x, mapSpawnYRow].CheckIfPossibleToMoveOn((EnemyNormal)waves[x, currentWave]))
                        {
                            avaliable = false;
                        }

                        break;

                    case GameplayObjectType.EnemyFlying:

                        if (mapData[x, mapSpawnYRow].CheckIfPossibleToMoveOn((EnemyFlying)waves[x, currentWave]))
                        {
                            avaliable = false;
                        }

                        break;
                }
            }
        }
            
        return avaliable;
    }

    public void SpawnSingleEnemy(Tile tile, GameplayObject enemy)
    {
        switch (enemy.GameplayObjectType)
        {
            case GameplayObjectType.Enemy:
                tile.UpdateTile((EnemyNormal) enemy);
                break;

            case GameplayObjectType.EnemyRanged:
                tile.UpdateTile((EnemyRanged) enemy);
                break;

            case GameplayObjectType.EnemyFlying:
                tile.UpdateTile((EnemyFlying) enemy);
                break;
        }

        EnemiesController.Instance.AddSpawnedEnemy(enemy);
        EnemiesController.Instance.EnemiesOnMap[EnemiesController.Instance.EnemiesOnMap.Count - 1].SetValues(tile);
    }

    private void Spawn(Map map, int mapSpawnYRow, int mapXSzie, int mapYSize, GameplayObject[,] waves)
    {
        for (int x = 0; x < waves.GetLength(0); x++)
        {
            if (!ReferenceEquals(waves[x, currentWave], null))
            {
               switch(waves[x, currentWave].GameplayObjectType)
                {
                    case GameplayObjectType.Enemy:
                        map.MapData[x, mapSpawnYRow].UpdateTile((EnemyNormal)waves[x, currentWave]);
                        break;

                    case GameplayObjectType.EnemyRanged:
                        map.MapData[x, mapSpawnYRow].UpdateTile((EnemyRanged)waves[x, currentWave]);
                        break;

                    case GameplayObjectType.EnemyFlying:
                        map.MapData[x, mapSpawnYRow].UpdateTile((EnemyFlying)waves[x, currentWave]);
                        break;
                }

                EnemiesController.Instance.AddSpawnedEnemy(waves[x, currentWave]);
                EnemiesController.Instance.EnemiesOnMap[EnemiesController.Instance.EnemiesOnMap.Count-1].SetValues(map.MapData[x, mapSpawnYRow]);
            }
        }
    }

    public void RunSpawner(Map map, int mapSpawnYRow, int mapXSize, int mapYSize, GameplayObject[,] waves)
    {
        if(currentWave < waves.GetLength(1))
        {
            if (CheckForPlaceToSpawn(mapSpawnYRow, mapXSize, map.MapData, LevelLoader.Instance.EnemiesPreset))
            {
                Spawn(map, mapSpawnYRow, mapXSize, mapYSize, waves);
                currentWave++;
            }
        }
    }

    public bool CheckIfAllWavesSpawned(GameplayObject[,] waves)
    {
        bool allSpawned = false;

        if(currentWave >= waves.GetLength(1))
        {
            allSpawned = true;
        }

        return allSpawned;
    }
}
