using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerPersistentDataLoadedAndUnpacked
{
    public PlayerPersistentDataLoadedAndUnpacked(int unlockedLevels, List<BaseSpell> playerUnlockedSpells, List<BaseSpell> playerEquipedSpells, PlayerInventory playerInventory)
    {
        this.playerUnlockedSpells = playerUnlockedSpells;
        this.playerEquipedSpells = playerEquipedSpells;
        this.playerInventory = playerInventory;
    }

    public int UnlockedLevels => unlockedLevels;
    public List<BaseSpell> PlayerUnlockedSpells => playerUnlockedSpells;
    public List<BaseSpell> PlayerEquipedSpells => playerEquipedSpells;
    public PlayerInventory PlayerInventory => playerInventory;

    [SerializeField] private int unlockedLevels = 1;

    [SerializeField] private List<BaseSpell> playerUnlockedSpells;
    [SerializeField] private List<BaseSpell> playerEquipedSpells;
    [SerializeField] private PlayerInventory playerInventory;

    public void UpdatePlayerPersistentDataLoadedAndUnpacked(int unlockedLevels, List<BaseSpell> playerUnlockedSpells, List<BaseSpell> playerEquipedSpells, PlayerInventory playerInventory)
    {
        this.unlockedLevels = unlockedLevels;
        this.playerUnlockedSpells = playerUnlockedSpells;
        this.playerEquipedSpells = playerEquipedSpells;
        this.playerInventory = playerInventory;
    }
}
