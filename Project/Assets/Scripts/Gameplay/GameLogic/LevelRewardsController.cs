using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRewardsController : MonoBehaviour
{
    public static LevelRewardsController Instance => instance;
    private static LevelRewardsController instance;

    private void Awake()
    {
        if(!ReferenceEquals(LevelRewardsController.Instance, null))
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public List<BaseSpell> DrawSpellsAvaliableToChooseForUnlock(int numberOfSpellsToReturn, BaseSpell[] spellsOfTier1, BaseSpell[] spellsOfTier2, BaseSpell[] spellsOfTier3, int tier1Probability, int tier2Probability, int tier3Probability)
    {
        List<BaseSpell> spellsAvaliableToChooseForUnlock = new List<BaseSpell>();

        List<BaseSpell> poolTier1 = CreateCurrentPoolToDraw(spellsOfTier1);
        List<BaseSpell> poolTier2 = CreateCurrentPoolToDraw(spellsOfTier2);
        List<BaseSpell> poolTier3 = CreateCurrentPoolToDraw(spellsOfTier3);

        for(int i = 0; i < numberOfSpellsToReturn; i++)
        {
            BaseSpell drawnSpell = TryDrawSpell(poolTier1, tier1Probability);
            if(!ReferenceEquals(drawnSpell, null)) spellsAvaliableToChooseForUnlock.Add(drawnSpell);

            drawnSpell = TryDrawSpell(poolTier2, tier2Probability);
            if (!ReferenceEquals(drawnSpell, null)) spellsAvaliableToChooseForUnlock[i] = drawnSpell;


            drawnSpell = TryDrawSpell(poolTier3, tier3Probability);
            if (!ReferenceEquals(drawnSpell, null)) spellsAvaliableToChooseForUnlock[i] = drawnSpell;
        }

        return spellsAvaliableToChooseForUnlock;
    }

    private List<BaseSpell> CreateCurrentPoolToDraw(BaseSpell[] tierSpells)
    {
        List<BaseSpell> pool = new List<BaseSpell>();

        for(int i=0; i < tierSpells.Length; i++)
        {
            pool.Add(tierSpells[i]);
        }

        return pool;
    }

    private BaseSpell TryDrawSpell(List<BaseSpell> pool, int probability)
    {
        BaseSpell drawnSpell = null;

        if(GetRandomIntFromRange.Get(1, 100) <= probability)
        {
            drawnSpell = ListRandomElement<BaseSpell>.ReturnRandomElement(pool);
        }

        return drawnSpell;
    }
}
