using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsAvaliableForFinishingLevels : MonoBehaviour
{
    public static RewardsAvaliableForFinishingLevels Instance => instance;
    private static RewardsAvaliableForFinishingLevels instance;

    private void Awake()
    {
        if(!ReferenceEquals(RewardsAvaliableForFinishingLevels.Instance, null))
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public BaseSpell[] SpellsTier1 => spellsTier1;
    public BaseSpell[] SpellsTier2 => spellsTier2;
    public BaseSpell[] SpellsTier3 => spellsTier3;

    [SerializeField] private BaseSpell[] spellsTier1;
    [SerializeField] private BaseSpell[] spellsTier2;
    [SerializeField] private BaseSpell[] spellsTier3;
}
