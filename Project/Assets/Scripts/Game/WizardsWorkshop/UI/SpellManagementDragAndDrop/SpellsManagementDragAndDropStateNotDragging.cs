using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class SpellsManagementDragAndDropStateNotDragging : SpellsManagementDragAndDropBaseState
    {

        public override void Update(SpellsManagementDragAndDrop context)
        {
            if(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.IsPressed())
            {
                if(context.RaycasterSpellToDrag.CheckForSpellToDrag())
                {
                    context.UpdateSpellBeingDragged(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells[context.RaycasterSpellToDrag.ISpellUnlocked.PosOnList]);
                    context.DraggedSpellIcon.LoadSpellIcon(context.SpellBeingDragged.SpellIcon);
                    context.SwitchState(context.StateDragging);
                }
            }
        }
    }
}

