using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Mixture", fileName = "NewMixture")]
public class ItemMixture : BaseItem
{
    public override ItemType ItemType => ItemType.Mixture;

    public List<Status> Statuses => statuses;

    [SerializeField] protected List<Status> statuses = new List<Status>();

    public override void TryUseItem()
    {
        if(!ReferenceEquals(PlayerStateIdleEvents.OnTryDrinkMixture, null))
        {
            PlayerStateIdleEvents.OnTryDrinkMixture.Invoke();
            PlayerStateDrinkMixtureEvents.OnDrinMixture.Invoke(this);
        }
    }

    public override void ClearAfterItemUse()
    {
    }
}
