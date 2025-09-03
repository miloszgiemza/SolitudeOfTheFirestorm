using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsWorkshop_PlayerProgressionMode
{
    public class SpellsManagementController : MonoBehaviour
    {
        private bool CheckIfSpellIsAlreadyOnList(BaseSpell spell, List<BaseSpell> listOfSpells)
        {
            bool spellIsAlreadyOnList = false;

            for (int i = 0; i < listOfSpells.Count; i++)
            {
                if (spell.IDGameDatabase == listOfSpells[i].IDGameDatabase) spellIsAlreadyOnList = true;
            }

            return spellIsAlreadyOnList;
        }

        public void DiscardSpellSlotFromEquipedSpellsList(List<BaseSpell> playerUnlockedSpells, List<BaseSpell> playerEquipedSpells, int spellsPossibleToDiscardNumber)
        {
            if( (playerEquipedSpells.Count > (playerUnlockedSpells.Count - spellsPossibleToDiscardNumber)) && (playerEquipedSpells.Count > GameController.Instance.MinPlayerEquipedSpellsNumber) )
            {
                playerEquipedSpells.RemoveAt(playerEquipedSpells.Count-1);
            }

            SavesController.Instance.SaveProgressionState(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels,
            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells,
            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerInventory,
            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.SpellsPossibleToDiscardNumber);
        }

        public void AddSpellSlotToEquipedSpellsList(List<BaseSpell> playerUnlockedSpells, List<BaseSpell> playerEquipedSpells)
        {
            if(playerEquipedSpells.Count < playerUnlockedSpells.Count && playerUnlockedSpells.Count > playerEquipedSpells.Count)
            {
                bool notEquipedSpellFound = false;

                for(int i = 0; i < playerUnlockedSpells.Count && !notEquipedSpellFound; i++)
                {
                    notEquipedSpellFound = true;

                    for(int j=0; j < playerEquipedSpells.Count; j++)
                    {
                        if(Equals(playerUnlockedSpells[i].IDGameDatabase, playerEquipedSpells[j].IDGameDatabase))
                        {
                            notEquipedSpellFound = false;
                        }
                    }

                    if(notEquipedSpellFound)
                    {
                        playerEquipedSpells.Add(playerUnlockedSpells[i]);
                    }
                }

                SavesController.Instance.SaveProgressionState(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels, 
                    PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells,
                    PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerInventory, 
                    PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.SpellsPossibleToDiscardNumber);
            }
        }

        public void AddUnlockedSpellToEquipedSpells(BaseSpell spellToAdd, int positionOnListToInsertSpell, List<BaseSpell> equipedSpells)
        {
            if(!CheckIfSpellIsAlreadyOnList(spellToAdd, equipedSpells))
            {
                equipedSpells[positionOnListToInsertSpell] = spellToAdd;
            }
        }
    }
}

