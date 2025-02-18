using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using GameDatabase;

public class Database : MonoBehaviour
{
    public static Database Instance => instance;
    private static Database instance;

    public DatabaseSpells DatabaseSpells => databaseSpells;
    public DatabaseInventory DatabaseInventory => databaseInventory;

    [SerializeField] private DatabaseSpells databaseSpells;
    [SerializeField] private DatabaseInventory databaseInventory;

    private void Awake()
    {
        if(!ReferenceEquals(Database.Instance, null))
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
