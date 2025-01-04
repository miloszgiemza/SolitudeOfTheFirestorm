using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevelPreset : ScriptableObject 
{
    public int Tier1SpellChance => tier1SpellChance;
    public int Tier2SpellChance => tier2SpellChance;
    public int Tier3SpellChance => tier3SpellChance;

    [SerializeField] [Range(0, 100)] private int tier1SpellChance = 100;
    [SerializeField] [Range(0, 100)] private int tier2SpellChance;
    [SerializeField] [Range(0, 100)]  private int tier3SpellChance;
}
