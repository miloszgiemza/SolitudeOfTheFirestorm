using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUIController : MonoBehaviour
{
    public static PlayerInventoryUIController Instance => instance;
    private static PlayerInventoryUIController instance;

    [SerializeField] private GameObject inventoryCompartmentPrefab;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject windowParent;

    private Dictionary<ItemType, PlayerInventoryCompartmentUI> inventoryCompartements = new Dictionary<ItemType, PlayerInventoryCompartmentUI>();

    private void Awake()
    {
        if(!ReferenceEquals(PlayerInventoryUIController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        CreateInventoryUI();

        windowParent.SetActive(false);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void CreateInventoryUI()
    {
        CreateCompartement(ItemType.BigScroll, PlayerInventoryController.Instance.PlayerInventory.InventoryBigScrolls);
        CreateCompartement(ItemType.Mixture, PlayerInventoryController.Instance.PlayerInventory.InventoryMixtures);
        CreateCompartement(ItemType.SmallScrolls, PlayerInventoryController.Instance.PlayerInventory.InventorySmallScrolls);
    }

    private void CreateCompartement(ItemType itemType, BaseItem[] itemsCompartement)
    {
        inventoryCompartements.Add(itemType, Instantiate(inventoryCompartmentPrefab, windowParent.transform).GetComponent<PlayerInventoryCompartmentUI>());
        inventoryCompartements[itemType].Initialize(itemType, itemsCompartement, inventoryItemPrefab);
    }

    public void UpdateInventory()
    {
        UpdateCompartement(ItemType.BigScroll, PlayerInventoryController.Instance.PlayerInventory.InventoryBigScrolls);
        UpdateCompartement(ItemType.Mixture, PlayerInventoryController.Instance.PlayerInventory.InventoryMixtures);
        UpdateCompartement(ItemType.SmallScrolls, PlayerInventoryController.Instance.PlayerInventory.InventorySmallScrolls);
    }

    private void UpdateCompartement(ItemType itemType, BaseItem[] items)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(!ReferenceEquals(items[i], null))
            {
                inventoryCompartements[itemType].Items[i].UpdateItem(items[i].Icon);
            }
            else
            {
                inventoryCompartements[itemType].Items[i].UpdateItem(null);
            }
        }
    }

    public void ShowHideWindow()
    {
        if(windowParent.activeSelf==true)windowParent.SetActive(false);
        else windowParent.SetActive(true);
    }

    public void HideWindow()
    {
        windowParent.SetActive(false);
    }
}
