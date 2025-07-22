using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WizardsWorkshop_PlayerProgressionMode;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class ButtonEquipedSpellsAddSlot : BaseButton
    {
        private WizardsWorkshopManager wizardsWorkshopManager;
        private WizardsWorkshopUIManager wizardsWorkshopUIManager;

        protected override void Awake()
        {
            base.Awake();
            wizardsWorkshopManager = GetComponentInParent<WizardsWorkshopManager>();
            wizardsWorkshopUIManager = GetComponentInParent<WizardsWorkshopUIManager>();
        }

        protected override void DoThisOnClick()
        {
            wizardsWorkshopManager.SpellsManagementController.AddSpellSlotToEquipedSpellsList(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells, 
                PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells);

            wizardsWorkshopUIManager.SpellsManagementUIController.RefreshSpellListsUIEquiedSpells(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells);
        }
    }
}
