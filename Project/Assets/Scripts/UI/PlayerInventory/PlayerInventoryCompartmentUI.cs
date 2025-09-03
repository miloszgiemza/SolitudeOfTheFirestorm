using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryCompartmentUI : MonoBehaviour
{
    public List<PlayerInventoryItemUIIcon> Items => items;

    private ItemType itemsType;

    private List<PlayerInventoryItemUIIcon> items = new List<PlayerInventoryItemUIIcon>();

    public void Initialize(ItemType itemsType, BaseItem[] itemsArray, GameObject itemUIIconPrefab)
    {
        this.itemsType = itemsType;

        for(int i = 0; i < itemsArray.Length; i++)
        {
            items.Add(Instantiate(itemUIIconPrefab, this.transform).GetComponent<PlayerInventoryItemUIIcon>());
            items[items.Count - 1].Initialize(itemsType, i);
            
            if (!ReferenceEquals(itemsArray[i], null))
            {
                items[items.Count - 1].UpdateItem(itemsArray[i].Icon);
            }
            else
            {
                items[items.Count - 1].UpdateItem(null);
            }
        }
    }
}
