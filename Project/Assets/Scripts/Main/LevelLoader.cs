using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance => instance;
    private static LevelLoader instance;

    public bool Initialised => enemiesInitiliased;
    public GameplayObject[,] EnemiesPreset => enemiesPreset;

    [SerializeField] private bool randomMap = true;
    private bool enemiesInitiliased = false;

    private GameplayObject[,] enemiesPreset = new GameplayObject[8, 2];

    [SerializeField] private LevelEnemiesPreset levelEnemiesPresetToLoad;

    private void Awake()
    {
        if(!ReferenceEquals(LevelLoader.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (!randomMap)
        {
            enemiesPreset = new GameplayObject[8, levelEnemiesPresetToLoad.WavesNumber];

            for (int y = 0; y < levelEnemiesPresetToLoad.WavesNumber; y++)
            {
                for (int x = 0; x < enemiesPreset.GetLength(0); x++)
                {
                    if (ReferenceEquals(levelEnemiesPresetToLoad.Waves.rows[y].row[x], null))
                    {
                        enemiesPreset[x, y] = null;
                    }
                    else
                    {
                        enemiesPreset[x, y] = GenerateRightEnemy(levelEnemiesPresetToLoad.Waves.rows[y].row[x]);
                    }
                }
            }

            enemiesInitiliased = true;
        }
        else
        {
            StartCoroutine(WaitForRandomLevelGeneratorToFinish());
        }
    }

    private GameplayObject GenerateRightEnemy(EnemyDataAndStats enemyType)
    {
        GameplayObject newEnemy = null;

        switch(enemyType.EnemyBehaviourType)
        {
            case EnemyBahaviourType.EnemyJustGoingForward:
                newEnemy = new EnemyJustGoingForward(enemyType.Image, enemyType.Health, enemyType.Speed, enemyType.Damage, enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier);
                break;

            case EnemyBahaviourType.EnemyTryingToBypassObstacle:
                newEnemy = new EnemyTryingToBypassObstacle(enemyType.Image, enemyType.Health, enemyType.Speed, enemyType.Damage, enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier);
                break;

            case EnemyBahaviourType.EnemyAreaAura:
                newEnemy = new EnemyWithAreaAura(enemyType.Image, enemyType.Health, enemyType.Speed, enemyType.Damage, enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier, enemyType.Statuses, enemyType.AreaOfEffect);
                break;
        }

        return newEnemy;
    }

    private IEnumerator WaitForRandomLevelGeneratorToFinish()
    {
        yield return new WaitUntil(() => LevelEnemiesPresetGenerator.Instance.Initiliase);
        enemiesPreset = LevelEnemiesPresetGenerator.Instance.Preset;
        enemiesInitiliased = true;
    }
}
