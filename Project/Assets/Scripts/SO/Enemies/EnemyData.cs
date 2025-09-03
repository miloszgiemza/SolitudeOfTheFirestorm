using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public enum EnemyBahaviourType
{
    EnemyJustGoingForward,
    EnemyBypassingObstaclesMovementForwardAndSides,
    EnemyBypassingObstaclesAndAvoidingDangersMovementAllDirections,
    EnemyAreaAura,
    EnemyRanged
}

public enum EnemyTier
{
    Tier1,
    Tier2,
    Tier3
}

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/NewEnemyData", fileName = "NewEnemyData")]
[Serializable]
public class EnemyData : BaseGampelayObjectDataAndStats
{
    public override DatabaseItemType DatabaseItemType => DatabaseItemType.EnemyTypeData;

    public EnemyBahaviourType EnemyBehaviourType => enemyBehaviourType;
    public int Speed => speed;
    public int Health => health;
    public int NotImmobilized => notImmobilized;
    public LootSpawner LootSpawner => lootSpawner;
    public int Defence => defence;
    public EnemyTier Tier => tier;

    //enemy with area aura
    public List<Status> Statuses => statuses;
    public MapPosition[] AreaOfEffect => areaOfEffect;

    //enemy ranged
    //public int Range => range;



    [SerializeField] protected EnemyBahaviourType enemyBehaviourType = EnemyBahaviourType.EnemyJustGoingForward;

    [SerializeField] protected int speed = 1;
    [SerializeField] protected int health = 1;
    [SerializeField] protected int notImmobilized = 1;
    [SerializeField] protected LootSpawner lootSpawner;
    [SerializeField] protected int defence = 0;
    [SerializeField] protected EnemyTier tier;

    //enemy with area aura
    [SerializeField] protected List<Status> statuses = new List<Status>();
    [SerializeField] protected MapPosition[] areaOfEffect;

    //enemy ranged
    //[SerializeField] protected int range;
}
