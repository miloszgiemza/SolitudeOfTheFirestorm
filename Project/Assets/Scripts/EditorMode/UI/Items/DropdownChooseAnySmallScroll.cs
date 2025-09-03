using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;


using GameDatabase;
using DebugMode;

namespace DebugModeUI
{
    public class DropdownChooseAnySmallScroll : BaseDropdown
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
            for (int i = 0; i < Database.Instance.DatabaseInventory.SmallScrollsAll.Length; i++)
            {
                this.dropdownChoiceOptions.Add(Database.Instance.DatabaseInventory.SmallScrollsAll[i].Name);
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
            debugModeItemsChoiceController.TryUseSmallScroll(dropdown.value);
        }
    }
}
