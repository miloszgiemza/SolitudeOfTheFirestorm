using UnityEngine;

namespace DebugMode
{
    public class StateSelectedTileEditor : BaseStateTileEditor
    {
        public override DebugModeTileEditionController.TileEditorStateIdentifier State => DebugModeTileEditionController.TileEditorStateIdentifier.Selected;

        private DebugModeTileEditionController tileEditor;

        public override void InitializeState(DebugModeTileEditionController tileEditor)
        {
            this.tileEditor = tileEditor;
            tileEditor.OnCreateNewEnemy += CreateNewEnemy;
            tileEditor.OnDeselectTile += DeselectTheTile;
            if (!ReferenceEquals(tileEditor.OnTileSelected, null)) tileEditor.OnTileSelected.Invoke();
        }

        public override void DisableState()
        {
            tileEditor.OnCreateNewEnemy -= CreateNewEnemy;
            tileEditor.OnDeselectTile -= DeselectTheTile;
        }

        public override void RunUpdate(DebugModeTileEditionController tileEditor)
        {
           
        }

        private void CreateNewEnemy(EnemyData newEnemyData)
        {
            //usun¹æ starego
            if(!ReferenceEquals(tileEditor.SelectedTile.EnemySocket, null))tileEditor.SelectedTile.EnemySocket.MarkToRemoveFromGame();
            EnemiesController.Instance.ClearDeadEnemies();
            tileEditor.Spawner.SpawnSingleEnemy(tileEditor.SelectedTile, tileEditor.LevelLoader.GenerateRightEnemy(newEnemyData));

            EnemiesDisplayControler.Instance.Display(EnemiesController.Instance.EnemiesOnMap);
            EnemiesNormalAtributesUIDisplayController.Instance.UpdateUI(Map.Instance.MapData);

            tileEditor.SwitchState(DebugModeTileEditionController.TileEditorStateIdentifier.NotSelected);
        }

        private void DeselectTheTile()
        {
            tileEditor.SwitchState(DebugModeTileEditionController.TileEditorStateIdentifier.NotSelected);
        }
    }
}
