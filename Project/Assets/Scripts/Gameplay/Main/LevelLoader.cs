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

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
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

    public GameplayObject GenerateRightEnemy(EnemyData enemyType)
    {
        GameplayObject newEnemy = null;

        switch(enemyType.EnemyBehaviourType)
        {
            case EnemyBahaviourType.EnemyJustGoingForward:
                newEnemy = new EnemyJustGoingForward(enemyType.Image, enemyType.DescriptionEN, enemyType.DescriptionPL, enemyType.Health, enemyType.Speed, enemyType.Damage, 
                    enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier);
                break;

            case EnemyBahaviourType.EnemyBypassingObstaclesAndAvoidingDangersMovementAllDirections:
                newEnemy = new EnemyBypassingObstaclesAndAvoidingDangersMovementAllDirections(enemyType.Image, enemyType.DescriptionEN, enemyType.DescriptionPL,
                    enemyType.Health, enemyType.Speed, enemyType.Damage, enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier);
                break;

            case EnemyBahaviourType.EnemyAreaAura:
                newEnemy = new EnemyWithAreaAura(enemyType.Image, enemyType.DescriptionEN, enemyType.DescriptionPL, enemyType.Health, enemyType.Speed, enemyType.Damage, 
                    enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier, enemyType.Statuses, enemyType.AreaOfEffect);
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
