using UnityEngine;

using DebugMode;
using GameDatabase;

namespace DebugModeUI
{
    public class ButtonTileEditionPlace : BaseButton
    {
        private DebugModeTileEditionController debugModeTileEditionController;
        private DropdownTileEdtitionChooseEnemy dropdownTileEdtitionChooseEnemy;
        private DebugModeTileEditionUIController debugModeTileEditionUIController;

        protected override void Awake()
        {
            base.Awake();
            debugModeTileEditionController = FindFirstObjectByType<DebugModeTileEditionController>();
            dropdownTileEdtitionChooseEnemy = FindFirstObjectByType<DropdownTileEdtitionChooseEnemy>();
            debugModeTileEditionUIController = GetComponentInParent<DebugModeTileEditionUIController>();
        }

        protected override void DoThisOnClick()
        {
            if (!ReferenceEquals(debugModeTileEditionController.OnCreateNewEnemy, null))
            {
                debugModeTileEditionController.OnCreateNewEnemy.Invoke(
                    (EnemyData)(Database.Instance.DatabaseEnemyTypes.EnemiesTypesDataAll[dropdownTileEdtitionChooseEnemy.CurrentDropdownValue]));

                debugModeTileEditionUIController.HideTileEditionWIndow();
            }
        }
    }
}
