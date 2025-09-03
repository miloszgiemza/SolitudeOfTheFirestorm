using UnityEngine;

namespace DebugMode
{
    public abstract class BaseStateTileEditor
    {
        public abstract DebugModeTileEditionController.TileEditorStateIdentifier State { get; }

        public abstract void RunUpdate(DebugModeTileEditionController tileEditor);
        public abstract void InitializeState(DebugModeTileEditionController tileEditor);
        public abstract void DisableState();
    }
}
