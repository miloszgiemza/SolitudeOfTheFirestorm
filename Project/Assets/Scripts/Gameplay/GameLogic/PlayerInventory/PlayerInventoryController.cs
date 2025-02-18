using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    public static PlayerInventoryController Instance => instance;
    private static PlayerInventoryController instance;

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

    private void Awake()
    {
        if(!ReferenceEquals(PlayerInventoryController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            InitializeAndLoadProgression(PlayerProgressionController.Instance.PlayerProgression.PlayerInventoryState);
        }
    }

    #region Initialise
    private void LoadCompartmentState(BaseItem[] compartment, string[] itemsToLoad)
    {
        for(int i=0; i < itemsToLoad.Length && i < compartment.Length; i++)
        {
            compartment[i] = Database.Instance.DatabaseInventory.ReturnItem(itemsToLoad[i]);
        }
    }

    private void InitializeAndLoadProgression(PlayerInventoryState playerInventoryState)
    {
        if (SaveLoadController.CheckIfSaveFileExists())
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

    private void CopyInventoryCompartment(BaseItem[] loadTo, BaseItem[] loadFrom)
    {
        for(int i =0; i < loadFrom.Length && i < loadTo.Length; i++)
        {
            loadTo[i] = loadFrom[i];
        }
    }

    public void ReceiveItemAndTryToAcquireIt(BaseItem newItem)
    {
        switch(newItem.ItemType)
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

    private void TryeAdd(BaseItem[] inventoryCompartment, BaseItem newItem)
    {
        bool spotFound = false;

        for(int i = 0; i < inventoryCompartment.Length && !spotFound; i++)
        {
            if(ReferenceEquals(inventoryCompartment[i], null))
            {
                inventoryCompartment[i] = newItem;
                spotFound = true;
            }
        }

        if(spotFound==false)
        {
            inventoryCompartment[0] = newItem;
        }

        PlayerInventoryUIController.Instance.UpdateInventory();
    }

    public void TryUseItem(ItemType itemType, int itemIndex)
    {
        switch(itemType)
        {
            case ItemType.BigScroll:
                if(!ReferenceEquals(inventoryBigScrolls[itemIndex], null))
                {
                    currentItem = inventoryBigScrolls[itemIndex];
                    currentItemindex = itemIndex;
                    currentItem.TryUseItem();
                }
                break;

            case ItemType.Mixture:
                if(!ReferenceEquals(inventoryMixtures[itemIndex], null))
                {
                    currentItem = inventoryMixtures[itemIndex];
                    currentItemindex = itemIndex;
                    currentItem.TryUseItem();
                }
                break;

            case ItemType.SmallScrolls:
                if(!ReferenceEquals(inventorySmallScrolls[itemIndex], null))
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
        switch(currentItem.ItemType)
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
}
