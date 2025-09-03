using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelEnemiesPreset", fileName = "LevelEnemiesPreset")]
public class LevelEnemiesPreset : BaseLevelPreset
{
    public int WavesNumber => wavesNumber;
    public Serializable2DArray<EnemyData> Waves => waves;

    [SerializeField] private int wavesNumber = 100;
    [SerializeField] private Serializable2DArray<EnemyData> waves = new Serializable2DArray<EnemyData>(8, 100);
}
