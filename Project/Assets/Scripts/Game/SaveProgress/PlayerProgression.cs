using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

[Serializable]
public class PlayerProgression
{
    public int UnlockedLevelsNumber => unlockedLevelsNumber;
    public List<string> UnlockedSpells => unlockedSpells;
    public PlayerInventoryState PlayerInventoryState => playerInventoryState;

    private int unlockedLevelsNumber = 1;
    [SerializeField] private List<string> unlockedSpells = new List<string>();
    private PlayerInventoryState playerInventoryState;

    public PlayerProgression(List<string> unlockedSpells, PlayerInventoryState playerInventoryState, int unlockedLevelsNumber)
    {
        this.unlockedSpells = unlockedSpells;
        this.playerInventoryState = playerInventoryState;
        this.unlockedLevelsNumber = unlockedLevelsNumber;
    }

    public PlayerProgression(PlayerProgression newProgression)
    {
        unlockedSpells = newProgression.UnlockedSpells;
        this.playerInventoryState = newProgression.playerInventoryState;
        this.unlockedLevelsNumber = newProgression.UnlockedLevelsNumber;
    }

    public void UnlockSpell(BaseSpell unlockedSpell)
    {
        unlockedSpells.Add(unlockedSpell.IDGameDatabase);
    }
}
