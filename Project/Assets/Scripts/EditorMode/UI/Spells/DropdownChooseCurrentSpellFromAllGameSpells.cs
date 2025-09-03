using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GameDatabase;

using DebugMode;

namespace DebugModeUI
{
    public class DropdownChooseCurrentSpellFromAllGameSpells : BaseDropdown
    {
        protected override List<string> DropdownChoiceOptions => dropdownChoiceOptions;

        private DebugModeSpellChoiceController debugModeSpellChoiceController;

        private List<string> dropdownChoiceOptions = new List<string>();

        protected override void Awake()
        {
            debugModeSpellChoiceController = FindFirstObjectByType<DebugModeSpellChoiceController>();
            base.Awake();
        }

        protected override void InitializeDropdownChoiceOptions(List<string> dropdownChoiceOptions)
        {
            for(int i = 0; i < Database.Instance.DatabaseSpells.SpellsAll.Length; i++)
            {
                this.dropdownChoiceOptions.Add(Database.Instance.DatabaseSpells.SpellsAll[i].SpellName);
            }

            base.InitializeDropdownChoiceOptions(dropdownChoiceOptions);
        }

        protected override void HandleDropdownValueChange(TMP_Dropdown dropdown)
        {
            debugModeSpellChoiceController.ChooseCurrentSpellFromAllGameSpells(dropdown.value);
        }
    }
}

