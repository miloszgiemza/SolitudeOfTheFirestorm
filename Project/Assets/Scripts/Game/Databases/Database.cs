using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameDatabase
{
    public class Database : MonoBehaviour
{
    public static Database Instance => instance;
    private static Database instance;

    public DatabaseSpells DatabaseSpells => databaseSpells;
    public DatabaseInventory DatabaseInventory => databaseInventory;
    public    DatabaseEnemyTypes DatabaseEnemyTypes => databaseEnemyTypes;

    [SerializeField] private DatabaseSpells databaseSpells;
    [SerializeField] private DatabaseInventory databaseInventory;
    [SerializeField] private DatabaseEnemyTypes databaseEnemyTypes;

    private void Awake()
    {
        if (!ReferenceEquals(Database.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
    }
}

}

