using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    public static PlayerInventoryController Instance => instance;
    private static PlayerInventoryController instance;

    public PlayerInventory PlayerInventory => playerInventory;

    private PlayerInventory playerInventory = new PlayerInventory();

    private void Awake()
    {
        if(!ReferenceEquals(PlayerInventoryController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            playerInventory.LoadPersistentProgressionAtStartOfLevel(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerInventory);
        }
    }
}
