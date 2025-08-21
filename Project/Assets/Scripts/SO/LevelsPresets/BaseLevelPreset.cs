using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevelPreset : ScriptableObject 
{
    public int LevelNumber => levelNumber;

    public List<BaseSpell> SpellsUnlocksAvaliableAsRewards => spellsUnlocksAvaliableAsRewards;

    [SerializeField] private List<BaseSpell> spellsUnlocksAvaliableAsRewards;

    [SerializeField] private int levelNumber;

    [SerializeField] [Range(0, 100)] private int tier1SpellChance = 100;
    [SerializeField] [Range(0, 100)] private int tier2SpellChance;
    [SerializeField] [Range(0, 100)]  private int tier3SpellChance;
}
