using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/NewLootSpawner", fileName = "LootSpawner")]
public class LootSpawner : ScriptableObject
{
    [SerializeField] protected List<Loot> spawnableItems = new List<Loot>();

    public void TrySpawnLoot(Tile tile)
    {
        for(int i = 0; i < spawnableItems.Count; i++)
        {
            if(GetRandomIntFromRange.Get(0, 100) <= spawnableItems[i].ProbabilityOfDrop)
            {
                new CollectableItem(spawnableItems[i].Item.IconCollectable, tile, spawnableItems[i].Item);
            }
        }
    }
}
