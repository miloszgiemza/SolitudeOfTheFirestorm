using UnityEngine;

using DebugMode;

namespace DebugModeUI
{
    public class ButtonTileEditionCancel : BaseButton
    {
        private DebugModeTileEditionUIController debugModeTileEditionUIController;
        private DebugModeTileEditionController debugModeTileEditionController;

        protected override void Awake()
        {
            base.Awake();
            debugModeTileEditionUIController = GetComponentInParent<DebugModeTileEditionUIController>();
            debugModeTileEditionController = FindFirstObjectByType<DebugModeTileEditionController>();
        }

        protected override void DoThisOnClick()
        {
            debugModeTileEditionUIController.HideTileEditionWIndow();
            if(!ReferenceEquals(debugModeTileEditionController.OnDeselectTile, null))debugModeTileEditionController.OnDeselectTile.Invoke();
        }
    }
}
