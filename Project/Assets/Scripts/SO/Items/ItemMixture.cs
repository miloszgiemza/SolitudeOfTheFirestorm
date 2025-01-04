using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Mixture", fileName = "NewMixture")]
public class ItemMixture : BaseItem
{
    public override ItemType ItemType => ItemType.Mixture;

    public override void TryUseItem()
    {
    }

    public override void ClearAfterItemUse()
    {
    }
}
