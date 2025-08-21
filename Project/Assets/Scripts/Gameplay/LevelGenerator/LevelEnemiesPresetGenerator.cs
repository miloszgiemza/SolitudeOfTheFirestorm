using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class CountOfEnemiesOfTier
{
    public EnemyTier Tier => tier;
    public int Count => count;

    [SerializeField] private EnemyTier tier;
    [SerializeField] private int count;

    public CountOfEnemiesOfTier(EnemyTier tier, int count)
    {
        this.tier = tier;
        this.count = count;
    }

    public void Set(EnemyTier tier)
    {
        this.tier = tier;
    }

    public void Set(int count)
    {
        this.count = count;
    }

    public void Set(EnemyTier tier, int count)
    {
        this.tier = tier;
        this.count = count;
    }
}

public class LevelEnemiesPresetGenerator : MonoBehaviour
{
    public static LevelEnemiesPresetGenerator Instance => instance;
    private static LevelEnemiesPresetGenerator instance;

    public GameplayObject[,] Preset => preset;
    public bool Initiliase => initialised;

    [SerializeField] private bool loadParametersFromGameController;
    private bool parametersUpdated = false;

    [SerializeField] private EnemyData[] avaliableEnemiesTypes;
    [SerializeField] private int minHorizotalSpacingBeetwenEnemies;
    [SerializeField] private int maxHorizontalSpacingBeetwenEnemies;
    [SerializeField] private CountOfEnemiesOfTier[] countOfEnemiesOfTier;

    private Dictionary<EnemyTier, int> tiersGroupSizes = new Dictionary<EnemyTier, int>();

    private Dictionary<EnemyTier, List<EnemyData>> enemiesAvaliableFromEachTier = new Dictionary<EnemyTier, List<EnemyData>>();
    private bool enemiesAvaliableFromEachTierInitialised = false;

    private Dictionary<EnemyTier, List<GameplayObject>> enemyPool = new Dictionary<EnemyTier, List<GameplayObject>>();
    private bool enemyPoolInitialised = false;

    private GameplayObject[,] preset;
    private bool initialised = false;

    private void Awake()
    {
        if(!ReferenceEquals(LevelEnemiesPresetGenerator.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        if(loadParametersFromGameController)
        {
            LoadParametersFromOutsidePreset(GameController.Instance.CurrenLevelPresetToLoad);
        }
        else parametersUpdated = true;

        StartCoroutine(WaitTillParametersAreUpdated());
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void LoadParametersFromOutsidePreset(LevelEnemiesRandomisedPreset preset)
    {
        avaliableEnemiesTypes = preset.AvaliableEnemiesTypes;
        minHorizotalSpacingBeetwenEnemies = preset.MinHorizotalSpacingBeetwenEnemies;
        maxHorizontalSpacingBeetwenEnemies = preset.MaxHorizontalSpacingBeetwenEnemies;
        countOfEnemiesOfTier = preset.CountOfEnemiesOfTier;

        parametersUpdated = true;
    }

    private IEnumerator WaitTillParametersAreUpdated()
    {
        yield return new WaitUntil(() => parametersUpdated);

        CalculateGroupSizesOfEachTier(countOfEnemiesOfTier, tiersGroupSizes);
        GroupEnemiesByTierAndAddToDcitionary(countOfEnemiesOfTier, enemiesAvaliableFromEachTier, avaliableEnemiesTypes);
        StartCoroutine(GenerateEnemyPool(enemyPool, countOfEnemiesOfTier, enemiesAvaliableFromEachTier));

        StartCoroutine(GenerateLevelPreset(Map.Instance.MapData.GetLength(0), avaliableEnemiesTypes, minHorizotalSpacingBeetwenEnemies, maxHorizontalSpacingBeetwenEnemies, countOfEnemiesOfTier));
    }

    #region Initialize
    private void CalculateGroupSizesOfEachTier(CountOfEnemiesOfTier[] countOfEnemiesOfTier, Dictionary<EnemyTier, int> tiersGroupSizes)
    {
        CountOfEnemiesOfTier leastNumerousTier = countOfEnemiesOfTier[0];

        for (int i = 0; i < countOfEnemiesOfTier.Length; i++)
        {
            if (countOfEnemiesOfTier[i].Count < leastNumerousTier.Count)
            {
                leastNumerousTier = countOfEnemiesOfTier[i];
            }
        }

        tiersGroupSizes.Add(leastNumerousTier.Tier, leastNumerousTier.Count);

        for (int i = 0; i < countOfEnemiesOfTier.Length; i++)
        {
            if (!tiersGroupSizes.ContainsKey(countOfEnemiesOfTier[i].Tier))
            {
                tiersGroupSizes.Add(countOfEnemiesOfTier[i].Tier, countOfEnemiesOfTier[i].Count / leastNumerousTier.Count);
            }
        }

        tiersGroupSizes[leastNumerousTier.Tier] = 1;
    }

    private void GroupEnemiesByTierAndAddToDcitionary(CountOfEnemiesOfTier[] countOfEnemiesOfTier, Dictionary<EnemyTier, List<EnemyData>> enemiesAvaliableFromEachTier, EnemyData[] avaliableEnemiesTypes)
    {
        for (int i = 0; i < countOfEnemiesOfTier.Length; i++)
        {
            if (!enemiesAvaliableFromEachTier.ContainsKey(countOfEnemiesOfTier[i].Tier))
            {
                enemiesAvaliableFromEachTier.Add(countOfEnemiesOfTier[i].Tier, new List<EnemyData>());
            }
        }

        for (int i = 0; i < avaliableEnemiesTypes.Length; i++)
        {
            if (enemiesAvaliableFromEachTier.ContainsKey(avaliableEnemiesTypes[i].Tier))
            {
                enemiesAvaliableFromEachTier[avaliableEnemiesTypes[i].Tier].Add(avaliableEnemiesTypes[i]);
            }
        }

        enemiesAvaliableFromEachTierInitialised = true;
    }

    private IEnumerator GenerateEnemyPool(Dictionary<EnemyTier, List<GameplayObject>> enemyPool, CountOfEnemiesOfTier[] countOfEnemiesOfTier, Dictionary<EnemyTier, List<EnemyData>> enemiesAvaliableFromEachTier)
    {
        yield return new WaitUntil(() => enemiesAvaliableFromEachTierInitialised);

        for (int i = 0; i < countOfEnemiesOfTier.Length; i++)
        {
            if (!enemyPool.ContainsKey(countOfEnemiesOfTier[i].Tier))
            {
                enemyPool.Add(countOfEnemiesOfTier[i].Tier, new List<GameplayObject>());
            }

            for (int currentEnemy = 0; currentEnemy < countOfEnemiesOfTier[i].Count; currentEnemy++)
            {
                Debug.Log("Enemies count: " + enemiesAvaliableFromEachTier.Count);

                if(enemiesAvaliableFromEachTier[countOfEnemiesOfTier[i].Tier].Count > 0)
                {
                    enemyPool[countOfEnemiesOfTier[i].Tier].Add(
                    GenerateRightEnemy(enemiesAvaliableFromEachTier[countOfEnemiesOfTier[i].Tier][GetRandomIntFromRange.Get(0, enemiesAvaliableFromEachTier[countOfEnemiesOfTier[i].Tier].Count - 1)]));
                }
            }
        }

        enemyPoolInitialised = true;
    }

    private GameplayObject GenerateRightEnemy(EnemyData enemyType)
    {
        GameplayObject newEnemy = null;

        if(enemyType.GetType() == typeof(EnemyRangedData))
        {
            EnemyRangedData enemyRangedDaya = (EnemyRangedData)enemyType;

            newEnemy = new EnemyRanged(enemyType.Image, enemyType.DescriptionEN, enemyType.DescriptionPL, enemyType.Health, enemyType.Speed, enemyType.Damage, enemyType.NotImmobilized, 
                enemyType.LootSpawner, enemyType.Defence, enemyType.Tier, enemyRangedDaya.Range);
        }
        else
        {
            switch (enemyType.EnemyBehaviourType)
            {
                case EnemyBahaviourType.EnemyJustGoingForward:
                    newEnemy = new EnemyJustGoingForward(enemyType.Image, enemyType.DescriptionEN, enemyType.DescriptionPL, enemyType.Health, enemyType.Speed, enemyType.Damage,
                        enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier);
                    break;

                case EnemyBahaviourType.EnemyBypassingObstaclesMovementForwardAndSides:
                    newEnemy = new EnemyBypassingObstaclesMovementForwardAndSides(enemyType.Image, enemyType.DescriptionEN, enemyType.DescriptionPL, enemyType.Health, enemyType.Speed, 
                        enemyType.Damage, enemyType.NotImmobilized, enemyType.LootSpawner, enemyType.Defence, enemyType.Tier);
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
        }

        return newEnemy;
    }

    #endregion

    #region GenerationItself

    private bool CheckIfAllEnemiesSpawned()
    {
        bool allSpawned = true;

        foreach(KeyValuePair<EnemyTier, List<GameplayObject>> singlePool in enemyPool)
        {
            if(singlePool.Value.Count > 0)
            {
                allSpawned = false;
            }
        }

        return allSpawned;
    }

    private IEnumerator GenerateLevelPreset(int mapWidth, EnemyData[] avaliableEnemiesTypes, int minHorizontalSpacinBetweenEnemies, int maxHorizontalSpacingBeetwenEnemies, CountOfEnemiesOfTier[] countOfEnemiesOfTier)
    {
        yield return new WaitUntil(() => (enemyPoolInitialised &&  Map.Instance != null) );

        preset = new GameplayObject[mapWidth, 1];
        int currentX = 0;

        while(!CheckIfAllEnemiesSpawned())
        {
            SpawnEnemies(mapWidth, minHorizontalSpacinBetweenEnemies, maxHorizontalSpacingBeetwenEnemies, ref currentX);
        }

        Debug.Log("Preset Y: " + preset.GetLength(1));
        Debug.Log("Enemies spawbned: " + enemiesSpawned);
        initialised = true;
    }

    int enemiesSpawned = 0;

    private void SpawnEnemies(int mapWidth, int minHorizontalSpacinBetweenEnemies, int maxHorizontalSpacingBeetwenEnemies, ref int currentX)
    {
        foreach (KeyValuePair<EnemyTier, int> group in tiersGroupSizes)
        {
            for (int i = 0; i < group.Value && enemyPool[group.Key].Count > 0; i++)
            {
                currentX = GetNextAvaliableXPos(mapWidth, minHorizontalSpacinBetweenEnemies, maxHorizontalSpacingBeetwenEnemies, ref currentX);
                preset[currentX, preset.GetLength(1) - 1] = enemyPool[group.Key][0];
                enemyPool[group.Key].RemoveAt(0);
                enemiesSpawned++;
            }
        }
    }

    private int GetNextAvaliableXPos(int mapWidth, int minHorizontalSpacinBetweenEnemies, int maxHorizontalSpacingBeetwenEnemies, ref int currentX)
    {
        currentX += 1 + ReturnRandomSpacingBeetwenEnemies(minHorizontalSpacinBetweenEnemies, maxHorizontalSpacingBeetwenEnemies);

        if (currentX >= mapWidth)
        {
            //ExtendPresetArray();
            #region ExtendArray
                    GameplayObject[,] temp = new GameplayObject[preset.GetLength(0), preset.GetLength(1) + 1];

        for(int x=0; x < preset.GetLength(0); x++)
        {
            for(int y=0; y < preset.GetLength(1); y++)
            {
                temp[x, y] = preset[x, y];
            }
        }

        preset = new GameplayObject[temp.GetLength(0), temp.GetLength(1)];
        preset = temp;
            #endregion


            currentX = 0;
        }

        return currentX;
    }

    private int ReturnRandomSpacingBeetwenEnemies(int minGap, int maxGap)
    {
        return GetRandomIntFromRange.Get(minGap, maxGap);
    }

    private void ExtendPresetArray()
    {
        GameplayObject[,] temp = new GameplayObject[preset.GetLength(0), preset.GetLength(1) + 1];

        for(int x=0; x < preset.GetLength(0); x++)
        {
            for(int y=0; y < preset.GetLength(1); y++)
            {
                temp[x, y] = preset[x, y];
            }
        }

        preset = new GameplayObject[temp.GetLength(0), temp.GetLength(1)];
        preset = temp;
    }

    #endregion
}
