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

    public List<BaseSpell> DrawSpellsAvaliableToChooseForUnlock(int numberOfSpellsToReturn, List<BaseSpell> spellsAvaliableAsRewards)
    {
        List<BaseSpell> spellsNotYetUnlocked = new List<BaseSpell>();

        for(int i=0; i < spellsAvaliableAsRewards.Count; i++)
        {
            bool spellAlreadyUnlocked = false;

            for (int j=0; j < PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells.Count; j++)
            {
                if(Equals(spellsAvaliableAsRewards[i].IDGameDatabase, 
                    PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells[j].IDGameDatabase))
                {
                    spellAlreadyUnlocked = true;
                }
            }
            if (!spellAlreadyUnlocked) spellsNotYetUnlocked.Add(spellsAvaliableAsRewards[i]);
        }

        List<BaseSpell> spellsDrawnedAsRewards = new List<BaseSpell>();
            
        for (int i = 0; i < numberOfSpellsToReturn; i++)
            {
            spellsDrawnedAsRewards.Add(spellsNotYetUnlocked[GetRandomIntFromRange.Get(0, spellsNotYetUnlocked.Count-1)]);
            }

        return spellsDrawnedAsRewards;
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
