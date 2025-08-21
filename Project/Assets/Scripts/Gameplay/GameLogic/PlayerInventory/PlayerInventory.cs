using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInventory
{
    public int MaxCapacity => maxCapacity;

    public BaseItem[] InventoryBigScrolls => inventoryBigScrolls;
    public BaseItem[] InventoryMixtures => inventoryMixtures;
    public BaseItem[] InventorySmallScrolls => inventorySmallScrolls;

    private int maxCapacity = 3;

    [SerializeField] private BaseItem[] inventoryBigScrolls;
    [SerializeField] private BaseItem[] inventoryMixtures;
    [SerializeField] private BaseItem[] inventorySmallScrolls;

    private BaseItem currentItem;
    private int currentItemindex;

    public void Initialise(int maxCompartementSize, ItemBigScroll[] bigScrolls, ItemMixture[] mixtures, ItemSmallScroll[] smallScrolls)
    {
        maxCapacity = maxCompartementSize;
        
        inventoryBigScrolls = new ItemBigScroll[maxCompartementSize];
        for(int i=0; i < bigScrolls.Length; i++)
        {
            inventoryBigScrolls[i] = bigScrolls[i];
        }

        inventoryMixtures = new ItemMixture[maxCompartementSize];
        for (int i = 0; i < mixtures.Length; i++)
        {
            inventoryMixtures[i] = mixtures[i];
        }

        inventorySmallScrolls = new ItemSmallScroll[maxCompartementSize];
        for(int i=0; i < smallScrolls.Length; i++)
        {
            inventorySmallScrolls[i] = smallScrolls[i];
        }
    }

    public void LoadPersistentProgressionAtStartOfLevel(PlayerInventory playerInventory)
    {
        maxCapacity = playerInventory.MaxCapacity;

        for (int i = 0; i < maxCapacity; i++)
        {
            inventoryBigScrolls = new ItemBigScroll[maxCapacity];
            inventoryMixtures = new ItemMixture[maxCapacity];
            inventorySmallScrolls = new ItemSmallScroll[maxCapacity];
        }

            for (int i = 0; i < maxCapacity; i++)
        {
            if(!ReferenceEquals(playerInventory.InventoryBigScrolls[i], null))inventoryBigScrolls[i] = playerInventory.InventoryBigScrolls[i];
            if (!ReferenceEquals(playerInventory.InventoryMixtures[i], null)) inventoryMixtures[i] = playerInventory.InventoryMixtures[i];
            if (!ReferenceEquals(playerInventory.InventorySmallScrolls[i], null)) inventorySmallScrolls[i] = playerInventory.InventorySmallScrolls[i];
        }
    }

    /*
#region Initialise
public void LoadCompartmentState(BaseItem[] compartment, string[] itemsToLoad)
{
    for (int i = 0; i < itemsToLoad.Length && i < compartment.Length; i++)
    {
        compartment[i] = Database.Instance.DatabaseInventory.ReturnItem(itemsToLoad[i]);
    }
}

public void InitializeAndLoadProgression(PlayerInventoryState playerInventoryState)
{
    if (SaveLoadFileController.CheckIfSaveFileExists())
    {
        inventoryBigScrolls = new BaseItem[playerInventoryState.CompartmentsSize];
        inventoryMixtures = new BaseItem[playerInventoryState.CompartmentsSize];
        inventorySmallScrolls = new BaseItem[playerInventoryState.CompartmentsSize];

        LoadCompartmentState(inventoryBigScrolls, playerInventoryState.CompartmentBigScrolls);
        LoadCompartmentState(inventoryMixtures, playerInventoryState.CompartmentMixtures);
        LoadCompartmentState(inventorySmallScrolls, playerInventoryState.CompartmentSmallScrolls);
    }
}

#endregion

public void CopyInventoryCompartment(BaseItem[] loadTo, BaseItem[] loadFrom)
{
    for (int i = 0; i < loadFrom.Length && i < loadTo.Length; i++)
    {
        loadTo[i] = loadFrom[i];
    }
}

*/

    public void ReceiveItemAndTryToAcquireIt(BaseItem newItem)
    {
        switch (newItem.ItemType)
        {
            case ItemType.BigScroll:
                TryeAdd(inventoryBigScrolls, newItem);
                break;

            case ItemType.Mixture:
                TryeAdd(inventoryMixtures, newItem);
                break;

            case ItemType.SmallScrolls:
                TryeAdd(inventorySmallScrolls, newItem);
                break;
        }
    }

    public void TryeAdd(BaseItem[] inventoryCompartment, BaseItem newItem)
    {
        bool spotFound = false;

        for (int i = 0; i < inventoryCompartment.Length && !spotFound; i++)
        {
            if (ReferenceEquals(inventoryCompartment[i], null))
            {
                inventoryCompartment[i] = newItem;
                spotFound = true;
            }
        }

        if (spotFound == false)
        {
            inventoryCompartment[0] = newItem;
        }

        PlayerInventoryUIController.Instance.UpdateInventory();
    }

    public void TryUseItem(ItemType itemType, int itemIndex)
    {
        switch (itemType)
        {
            case ItemType.BigScroll:
                if (!ReferenceEquals(inventoryBigScrolls[itemIndex], null))
                {
                    currentItem = inventoryBigScrolls[itemIndex];
                    currentItemindex = itemIndex;
                    currentItem.TryUseItem();
                }
                break;

            case ItemType.Mixture:
                if (!ReferenceEquals(inventoryMixtures[itemIndex], null))
                {
                    currentItem = inventoryMixtures[itemIndex];
                    currentItemindex = itemIndex;
                    currentItem.TryUseItem();
                }
                break;

            case ItemType.SmallScrolls:
                if (!ReferenceEquals(inventorySmallScrolls[itemIndex], null))
                {
                    currentItem = inventorySmallScrolls[itemIndex];
                    currentItemindex = itemIndex;
                    currentItem.TryUseItem();
                }
                break;
        }
    }

    public void DestroyItemAndNullCurrentItemOnSuccesfullUse()
    {
        switch (currentItem.ItemType)
        {
            case ItemType.BigScroll:
                currentItem.ClearAfterItemUse();
                inventoryBigScrolls[currentItemindex] = null;
                currentItem = null;
                break;

            case ItemType.Mixture:
                currentItem.ClearAfterItemUse();
                inventoryMixtures[currentItemindex] = null;
                currentItem = null;
                break;

            case ItemType.SmallScrolls:
                currentItem.ClearAfterItemUse();
                inventorySmallScrolls[currentItemindex] = null;
                currentItem = null;
                break;
        }

        PlayerInventoryUIController.Instance.UpdateInventory();
    }

    public void DeselectItemAndNullCurrentItemOnUnsuccesfullUse()
    {
        currentItem.ClearAfterItemUse();
        currentItem = null;
        PlayerInventoryUIController.Instance.UpdateInventory();
    }

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage, ItemType itemType, int numberInInventoryCompartment)
    {
        TooltipParagraph[] description = new TooltipParagraph[0];

        switch (itemType)
        {
            case ItemType.BigScroll:
                if (!ReferenceEquals(inventoryBigScrolls[numberInInventoryCompartment], null)) description = inventoryBigScrolls[numberInInventoryCompartment].ReturnTooltipText(gameLanguage);
                break;

            case ItemType.Mixture:
                if (!ReferenceEquals(inventoryMixtures[numberInInventoryCompartment], null)) description = inventoryMixtures[numberInInventoryCompartment].ReturnTooltipText(gameLanguage);
                break;

            case ItemType.SmallScrolls:
                if (!ReferenceEquals(inventorySmallScrolls[numberInInventoryCompartment], null)) description = inventorySmallScrolls[numberInInventoryCompartment].ReturnTooltipText(gameLanguage);
                break;
        }

        return description;
    }

    public void TryUseItemInDebugMode(BaseItem debugItem)
    {
        currentItem = debugItem;
        currentItem.TryUseItem();
    }
}
