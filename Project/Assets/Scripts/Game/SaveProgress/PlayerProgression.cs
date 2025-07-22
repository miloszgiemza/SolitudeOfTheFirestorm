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
    public List<string> EquipedSpells => equipedSpells;
    public PlayerInventoryState PlayerInventoryState => playerInventoryState;
    public int SpellsPossibleToDiscardNumber => spellsPossibleToDiscardNumber;

    #region GameProgressionState
    private int unlockedLevelsNumber = 1;
    #endregion

    #region PlayerProgressionState
    [SerializeField] private List<string> unlockedSpells = new List<string>();
    [SerializeField] private List<string> equipedSpells = new List<string>();
    [SerializeField] private PlayerInventoryState playerInventoryState;
    [SerializeField] private int spellsPossibleToDiscardNumber;
    #endregion

    public PlayerProgression(PlayerProgression playerProgression)
    {
        this.unlockedLevelsNumber = playerProgression.unlockedLevelsNumber;
        this.unlockedSpells = playerProgression.unlockedSpells;
        this.equipedSpells = playerProgression.equipedSpells;
        this.playerInventoryState = playerProgression.playerInventoryState;
        this.spellsPossibleToDiscardNumber = playerProgression.SpellsPossibleToDiscardNumber;
    }

    public PlayerProgression(int unlockedLevelsNumber, List<string> unlockedSpells, List<string> equipedSpells, PlayerInventoryState playerInventoryState, int spellsPossibleToDiscardNumber)
    {
        this.unlockedLevelsNumber = unlockedLevelsNumber;
        this.unlockedSpells = unlockedSpells;
        this.equipedSpells = equipedSpells;
        this.playerInventoryState = playerInventoryState;
        this.spellsPossibleToDiscardNumber = spellsPossibleToDiscardNumber;
    }
}
