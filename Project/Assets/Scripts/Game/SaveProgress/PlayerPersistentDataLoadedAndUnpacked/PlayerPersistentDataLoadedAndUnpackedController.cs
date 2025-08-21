using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDatabase;

//stores loaded persistent data
//is updated with player progression
//calls dedicated function for saving to file and forwards to it it's state
public class PlayerPersistentDataLoadedAndUnpackedController : MonoBehaviour
{
    public static PlayerPersistentDataLoadedAndUnpackedController Instance => instance;
    private static PlayerPersistentDataLoadedAndUnpackedController instance;

    public PlayerPersistentDataLoadedAndUnpacked PlayerPersistentData => playerPersistentDataLoadedAndUnpacked;

    [SerializeField] private PlayerPersistentDataLoadedAndUnpacked playerPersistentDataLoadedAndUnpacked;

    private void Awake()
    {
        if(ReferenceEquals(PlayerPersistentDataLoadedAndUnpackedController.Instance, null))
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitTillTheSaveIsLoaded());
    }

    private IEnumerator WaitTillTheSaveIsLoaded()
    {
        yield return new WaitUntil(() => (SavesController.Instance != null));
        LoadPlayerPersistentData(SavesController.Instance.PlayerProgression.UnlockedLevelsNumber, SavesController.Instance.PlayerProgression.UnlockedSpells, SavesController.Instance.PlayerProgression.EquipedSpells,
    SavesController.Instance.PlayerProgression.PlayerInventoryState, SavesController.Instance.PlayerProgression.SpellsPossibleToDiscardNumber);
    }

    public void LoadPlayerPersistentData(int unlockedLevels, List<string> unlockedSpellsValue, List<string> equipedSpellsValue, PlayerInventoryState inventoryState, int spellsPossibleToDiscardNumber)
    {
        List<BaseSpell> unlockedSpells = new List<BaseSpell>();

        for(int i=0; i < unlockedSpellsValue.Count; i++)
        {
            if(!ReferenceEquals(Database.Instance.DatabaseSpells.ReturnItem(unlockedSpellsValue[i]), null)) unlockedSpells.Add(Database.Instance.DatabaseSpells.ReturnItem(unlockedSpellsValue[i]));
        }

        List<BaseSpell> equipedSpells = new List<BaseSpell>();

        for (int i = 0; i < equipedSpellsValue.Count; i++)
        {
            if (!ReferenceEquals(Database.Instance.DatabaseSpells.ReturnItem(equipedSpellsValue[i]), null)) equipedSpells.Add(Database.Instance.DatabaseSpells.ReturnItem(equipedSpellsValue[i]));
        }

        ItemBigScroll[] bigScrolls = new ItemBigScroll[inventoryState.CompartmentsSize];
        for(int i = 0; i < inventoryState.CompartmentBigScrolls.Length; i++)
        {
            bigScrolls[i] = (ItemBigScroll) Database.Instance.DatabaseInventory.ReturnItem(inventoryState.CompartmentBigScrolls[i]);
        }

        ItemMixture[] mixtures = new ItemMixture[inventoryState.CompartmentsSize];
        for (int i = 0; i < inventoryState.CompartmentMixtures.Length; i++)
        {
            mixtures[i] = (ItemMixture) Database.Instance.DatabaseInventory.ReturnItem(inventoryState.CompartmentMixtures[i]);
        }

        ItemSmallScroll[] smallScrolls = new ItemSmallScroll[inventoryState.CompartmentsSize];
        for (int i = 0; i < inventoryState.CompartmentSmallScrolls.Length; i++)
        {
            smallScrolls[i] = (ItemSmallScroll) Database.Instance.DatabaseInventory.ReturnItem(inventoryState.CompartmentSmallScrolls[i]);
        }

        PlayerInventory playerInventory = new PlayerInventory();
        playerInventory.Initialise(inventoryState.CompartmentsSize, bigScrolls, mixtures, smallScrolls);

        playerPersistentDataLoadedAndUnpacked = new PlayerPersistentDataLoadedAndUnpacked(unlockedLevels, unlockedSpells, equipedSpells, playerInventory, spellsPossibleToDiscardNumber);
    }

    public void UnlockSpell(BaseSpell unlockedSpell)
    {
        List<BaseSpell> newUnlockedSpellsList = playerPersistentDataLoadedAndUnpacked.PlayerUnlockedSpells;
        newUnlockedSpellsList.Add(unlockedSpell);

        List<BaseSpell> newEquipedSpells = playerPersistentDataLoadedAndUnpacked.PlayerEquipedSpells;
        newEquipedSpells.Add(unlockedSpell);

        playerPersistentDataLoadedAndUnpacked.UpdatePlayerPersistentDataLoadedAndUnpacked(playerPersistentDataLoadedAndUnpacked.UnlockedLevels, 
            newUnlockedSpellsList, newEquipedSpells, playerPersistentDataLoadedAndUnpacked.PlayerInventory, playerPersistentDataLoadedAndUnpacked.SpellsPossibleToDiscardNumber);

        SavesController.Instance.SaveProgressionState(playerPersistentDataLoadedAndUnpacked.UnlockedLevels, playerPersistentDataLoadedAndUnpacked.PlayerUnlockedSpells,
          playerPersistentDataLoadedAndUnpacked.PlayerEquipedSpells, playerPersistentDataLoadedAndUnpacked.PlayerInventory, playerPersistentDataLoadedAndUnpacked.SpellsPossibleToDiscardNumber);
    }

    public void UpdateUnlockedLevels(int unlockedLevels)
    {
        playerPersistentDataLoadedAndUnpacked.UpdatePlayerPersistentDataLoadedAndUnpacked(unlockedLevels, playerPersistentDataLoadedAndUnpacked.PlayerUnlockedSpells, playerPersistentDataLoadedAndUnpacked.PlayerEquipedSpells,
            playerPersistentDataLoadedAndUnpacked.PlayerInventory, playerPersistentDataLoadedAndUnpacked.SpellsPossibleToDiscardNumber);

        SavesController.Instance.SaveProgressionState(playerPersistentDataLoadedAndUnpacked.UnlockedLevels, playerPersistentDataLoadedAndUnpacked.PlayerUnlockedSpells,
            playerPersistentDataLoadedAndUnpacked.PlayerEquipedSpells, playerPersistentDataLoadedAndUnpacked.PlayerInventory, playerPersistentDataLoadedAndUnpacked.SpellsPossibleToDiscardNumber);
    }

    public void UpdatePlayerPersistentData(int unlockedLevels, List<BaseSpell> unlockedSpellsValue, List<BaseSpell> equipedSpellsValue, PlayerInventory playerInventory, int spellsPossibleToDiscardNumber)
    {
        playerPersistentDataLoadedAndUnpacked = new PlayerPersistentDataLoadedAndUnpacked(unlockedLevels, unlockedSpellsValue, equipedSpellsValue, playerInventory, spellsPossibleToDiscardNumber);

        SavesController.Instance.SaveProgressionState(playerPersistentDataLoadedAndUnpacked.UnlockedLevels, playerPersistentDataLoadedAndUnpacked.PlayerUnlockedSpells,
            playerPersistentDataLoadedAndUnpacked.PlayerEquipedSpells, playerPersistentDataLoadedAndUnpacked.PlayerInventory, playerPersistentDataLoadedAndUnpacked.SpellsPossibleToDiscardNumber);
    }
}
