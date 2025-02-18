using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelEnemiesRandomisedPreset", fileName = "LevelEnemiesRandomisedPreset")]
public class LevelEnemiesRandomisedPreset : BaseLevelPreset
{
    public EnemyData[] AvaliableEnemiesTypes => avaliableEnemiesTypes;
    public int MinHorizotalSpacingBeetwenEnemies => minHorizotalSpacingBeetwenEnemies;
    public int MaxHorizontalSpacingBeetwenEnemies => maxHorizontalSpacingBeetwenEnemies;
    public CountOfEnemiesOfTier[] CountOfEnemiesOfTier => countOfEnemiesOfTier;

    [SerializeField] private EnemyData[] avaliableEnemiesTypes;
    [SerializeField] private int minHorizotalSpacingBeetwenEnemies;
    [SerializeField] private int maxHorizontalSpacingBeetwenEnemies;
    [SerializeField] private CountOfEnemiesOfTier[] countOfEnemiesOfTier;
}
