using UnityEngine;

namespace DebugMode
{
    public class StateNotSelectedTileEditor : BaseStateTileEditor
    {
        public override DebugModeTileEditionController.TileEditorStateIdentifier State => DebugModeTileEditionController.TileEditorStateIdentifier.NotSelected;

        public override void InitializeState(DebugModeTileEditionController tileEditor)
        {
        }

        public override void DisableState()
        {
        }

        public override void RunUpdate(DebugModeTileEditionController tileEditor)
        {
            if(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.IsPressed())
            {
                if(tileEditor.GameplayController.CurrentState.StateID == GameplayController.States.Player && tileEditor.Player.CurrentState.State == PlayerState.IdleDebugMode)
                {
                    if (!ReferenceEquals(tileEditor.TileSelector.ReturnTile(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
                    Map.Instance.MapData), null))
                    {
                        tileEditor.SelectTile(tileEditor.TileSelector.ReturnTile(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
                        Map.Instance.MapData));

                        tileEditor.SwitchState(DebugModeTileEditionController.TileEditorStateIdentifier.Selected);
                    }
                }
            }
        }
    }
}
