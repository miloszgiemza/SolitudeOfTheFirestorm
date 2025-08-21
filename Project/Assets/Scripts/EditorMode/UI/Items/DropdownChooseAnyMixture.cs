using GameDatabase;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;

using DebugMode;

namespace DebugModeUI
{
    public class DropdownChooseAnyMixture : BaseDropdown
    {
        protected override List<string> DropdownChoiceOptions => dropdownChoiceOptions;

        private List<string> dropdownChoiceOptions = new List<string>();

        private DebugModeItemsChoiceController debugModeItemsChoiceController;

        protected override void Awake()
        {
            debugModeItemsChoiceController = FindFirstObjectByType<DebugModeItemsChoiceController>();
            base.Awake();
        }

        protected override void InitializeDropdownChoiceOptions(List<string> dropdownChoiceOptions)
        {
            for (int i = 0; i < Database.Instance.DatabaseInventory.MixturesAll.Length; i++)
            {
                this.dropdownChoiceOptions.Add(Database.Instance.DatabaseInventory.MixturesAll[i].Name);
            }

            base.InitializeDropdownChoiceOptions(dropdownChoiceOptions);
        }

        protected override void HandleDropdownValueChange(TMP_Dropdown dropdown)
        {
            StartCoroutine(WaitWithOnClickActionTillButtonReleased());
        }

        private IEnumerator WaitWithOnClickActionTillButtonReleased()
        {
            yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
            debugModeItemsChoiceController.TryUseMixture(dropdown.value);
        }
    }
}
