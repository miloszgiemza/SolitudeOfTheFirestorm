using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/NewEnemyRangedData", fileName = "NewEnemyRangedData")]
[Serializable]
public class EnemyRangedData : EnemyData
{
    public int Range => range;

    [SerializeField] protected int range;
}
