using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelEnemiesPreset", fileName = "LevelEnemiesPreset")]
public class LevelEnemiesPreset : ScriptableObject
{
    public int WavesNumber => wavesNumber;
    public Serializable2DArray<EnemyDataAndStats> Waves => waves;

    [SerializeField] private int wavesNumber = 100;
    [SerializeField] private Serializable2DArray<EnemyDataAndStats> waves = new Serializable2DArray<EnemyDataAndStats>(8, 100);
}
