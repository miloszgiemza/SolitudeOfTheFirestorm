using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LootSpawner", fileName = "LootSpawner")]
public class LootSpawner : ScriptableObject
{
    [SerializeField] protected List<Loot> spawnableItems = new List<Loot>();

    public void TrySpawnLootUI()
    {
        for (int i = 0; i < spawnableItems.Count; i++)
        {
            if (GetRandomIntFromRange.Get(0, 100) <= spawnableItems[i].ProbabilityOfDrop)
            {
                DroppedItemsController.Instance.ReceiveDroppedItem(spawnableItems[i].Item);
            }
        }
    }

    #region NotUsedForNow

    public void TrySpawnLootTile(Tile tile)
    {
        for (int i = 0; i < spawnableItems.Count; i++)
        {
            if (GetRandomIntFromRange.Get(0, 100) <= spawnableItems[i].ProbabilityOfDrop)
            {
                new CollectableItem(spawnableItems[i].Item.IconCollectable, spawnableItems[i].Item.DescriptionEN, spawnableItems[i].Item.DescriptionPL, tile, spawnableItems[i].Item);
            }
        }
    }

    #endregion
}
