using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavesController : MonoBehaviour
{
    public static SavesController Instance => instance;
    private static SavesController instance;

    public PlayerProgression PlayerProgression => playerProgression;

    [SerializeField] private PlayerProgression playerProgression;

    private void Awake()
    {
        if(!ReferenceEquals(SavesController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);

            TryLoadProgression();
        }
    }

    private void TryLoadProgression()
    {
        if (SaveLoadFileController.CheckIfSaveFileExists())
        {
            playerProgression = new PlayerProgression(SaveLoadFileController.LoadGame());
        }
        else
        {
            /*
            SaveProgressionState(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels,
                PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells,
                PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerInventory);
            */
        }
    }

    public void SaveProgressionState(int unlockedLevels, List<BaseSpell> unlockedSpells, List<BaseSpell> equipedSpells, PlayerInventory playerInventory, int spellsPossibleToDiscardNumber)
    {
        List<string> unlockedSpellsIDs = new List<string>();
        for(int i=0; i < unlockedSpells.Count; i++)
        {
            unlockedSpellsIDs.Add(unlockedSpells[i].IDGameDatabase);
        }

        List<string> equipedSpellsIDs = new List<string>();
        for (int i = 0; i < equipedSpells.Count; i++)
        {
            equipedSpellsIDs.Add(equipedSpells[i].IDGameDatabase);
        }

        string[] bigScrolls = new string[playerInventory.InventoryBigScrolls.Length];
        for(int i =0; i < playerInventory.InventoryBigScrolls.Length; i++)
        {
            if(!ReferenceEquals(playerInventory.InventoryBigScrolls[i], null)) bigScrolls[i] = playerInventory.InventoryBigScrolls[i].IDGameDatabase;
        }

        string[] mixtures = new string[playerInventory.InventoryMixtures.Length];
        for (int i = 0; i < playerInventory.InventoryMixtures.Length; i++)
        {
            if(!ReferenceEquals(playerInventory.InventoryMixtures[i], null))mixtures[i] = playerInventory.InventoryMixtures[i].IDGameDatabase;
        }

        string[] smallScrolls = new string[playerInventory.InventorySmallScrolls.Length];
        for (int i = 0; i < playerInventory.InventorySmallScrolls.Length; i++)
        {
            if(!ReferenceEquals(playerInventory.InventorySmallScrolls[i], null))smallScrolls[i] = playerInventory.InventorySmallScrolls[i].IDGameDatabase;
        }

        PlayerInventoryState playerInventoryState = new PlayerInventoryState(playerInventory.MaxCapacity, bigScrolls, mixtures, smallScrolls);

        playerProgression = new PlayerProgression(unlockedLevels, unlockedSpellsIDs, equipedSpellsIDs, playerInventoryState, spellsPossibleToDiscardNumber);

        SaveLoadFileController.SaveGame(playerProgression);
    }
}
