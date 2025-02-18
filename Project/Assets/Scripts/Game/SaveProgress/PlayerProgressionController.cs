using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressionController : MonoBehaviour
{
    public static PlayerProgressionController Instance => instance;
    private static PlayerProgressionController instance;

    public PlayerProgression PlayerProgression => playerProgression;

    [SerializeField] private PlayerProgression playerProgression;

    private void Awake()
    {
        if(!ReferenceEquals(PlayerProgressionController.Instance, null))
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
        if(SaveLoadController.CheckIfSaveFileExists())
        {
            playerProgression = new PlayerProgression(SaveLoadController.LoadGame());
        }
    }

    public void UnlockSpellAndUpdatePlayerProgression(BaseSpell unlockedSpell)
    {
        playerProgression.UnlockSpell(unlockedSpell);

        SaveLoadController.SaveGame(playerProgression);
    }

    public void SaveProgressionState(List<BaseSpell> unlockedSpells, PlayerInventoryController playerInventoryController, int unlockedLevelsNumber)
    {
        List<string> unlockedSpellsIDs = new List<string>();

        for(int i=0; i < unlockedSpells.Count; i++)
        {
            unlockedSpellsIDs.Add(unlockedSpells[i].IDGameDatabase);
        }

        playerProgression = new PlayerProgression(unlockedSpellsIDs, new PlayerInventoryState(playerInventoryController.MaxCapacity, playerInventoryController.InventoryBigScrolls,
            playerInventoryController.InventoryMixtures, playerInventoryController.InventorySmallScrolls), unlockedLevelsNumber);

        SaveLoadController.SaveGame(playerProgression);
    }
}
