using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class SpellsManagementDragAndDropStateDragging : SpellsManagementDragAndDropBaseState
    {
        public override void Update(SpellsManagementDragAndDrop context)
        {
            if(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.IsPressed())
            {
                context.DraggedSpellIcon.UpdateIconPosition(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>());
            }
            else
            {
                if(context.RaycasterSlotToDrop.CheckForSlotToDrop())
                {
                    if(!CheckIfDraggedSpellIsAlreadyOnList(context.SpellBeingDragged))
                    {
                        List<BaseSpell> newEquipedSpells = PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells;
                        newEquipedSpells[context.RaycasterSlotToDrop.ISpellEquiped.PosOnList] = context.SpellBeingDragged;

                        PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UpdatePlayerPersistentDataLoadedAndUnpacked(
                            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells,
                            newEquipedSpells, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerInventory, 
                            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.SpellsPossibleToDiscardNumber);

                        
                        SavesController.Instance.SaveProgressionState(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells,
                            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerInventory,
                            PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.SpellsPossibleToDiscardNumber);
                        

                        context.SpellsManagementUIController.RefreshSpellListsUIEquiedSpells(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells);
                    }
                }

                context.DraggedSpellIcon.HideIcon();
                context.SwitchState(context.StateNotDragging);
            }
        }

        private bool CheckIfDraggedSpellIsAlreadyOnList(BaseSpell draggedSpell)
        {
            bool isOnList = false;

            for(int i = 0; i < PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells.Count; i++)
            {
                if(Equals(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells[i].IDGameDatabase, draggedSpell.IDGameDatabase))
                {
                    isOnList = true;
                }
            }

            return isOnList;
        }
    }
}
