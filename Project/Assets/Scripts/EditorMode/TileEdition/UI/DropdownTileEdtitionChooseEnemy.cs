using System.Collections.Generic;
using TMPro;
using UnityEngine;

using GameDatabase;

namespace DebugModeUI
{
    public class DropdownTileEdtitionChooseEnemy : BaseDropdown
    {
        public int CurrentDropdownValue => currentDropdownValue;

        protected override List<string> DropdownChoiceOptions => dropdownChoiceOptions;

        private List<string> dropdownChoiceOptions = new List<string>();

        private int currentDropdownValue = 0;

        private void OnDisable()
        {
            currentDropdownValue = 0;
        }

        protected override void InitializeDropdownChoiceOptions(List<string> dropdownChoiceOptions)
        {
            for (int i = 0; i < Database.Instance.DatabaseEnemyTypes.EnemiesTypesDataAll.Length; i++)
            {
                if(!ReferenceEquals(Database.Instance.DatabaseEnemyTypes.EnemiesTypesDataAll[i].ReturnTooltipText(GameController.Instance.GameLanguage), null))
                {
                    this.dropdownChoiceOptions.Add(Database.Instance.DatabaseEnemyTypes.EnemiesTypesDataAll[i].ReturnTooltipText(GameController.Instance.GameLanguage)[0].Title);
                }
            }

            base.InitializeDropdownChoiceOptions(dropdownChoiceOptions);
        }

        protected override void HandleDropdownValueChange(TMP_Dropdown dropdown)
        {
            currentDropdownValue = dropdown.value;
        }
    }
}
