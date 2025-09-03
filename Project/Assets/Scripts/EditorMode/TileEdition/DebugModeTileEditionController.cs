using UnityEngine;
using System;

namespace DebugMode
{
    public class DebugModeTileEditionController : MonoBehaviour
    {
        public LevelLoader LevelLoader => levelLoader;
        public Spawner Spawner => spawner;
        public GameplayController GameplayController => gameplayController;
        public Player Player => player;

        public TileSelector TileSelector => tileSelector;

        public Tile SelectedTile => selectedTile;

        public Action<EnemyData> OnCreateNewEnemy;
        public Action OnTileSelected;
        public Action OnDeselectTile;

        private LevelLoader levelLoader;
        private Spawner spawner;
        private GameplayController gameplayController;
        private Player player;

        private TileSelector tileSelector;

        public enum TileEditorStateIdentifier
        {
            NotSelected,
            Selected
        }

        private BaseStateTileEditor currentState;

        private StateNotSelectedTileEditor stateNotSelectedTileEditor = new StateNotSelectedTileEditor();
        private StateSelectedTileEditor stateSelectedTileEditor = new StateSelectedTileEditor();

        private Tile selectedTile = null;

        private void Awake()
        {
            levelLoader = FindFirstObjectByType<LevelLoader>();
            spawner = FindFirstObjectByType<Spawner>();
            gameplayController = GetComponentInParent<GameplayController>();
            player = FindFirstObjectByType<Player>();
            tileSelector = GetComponentInChildren<TileSelector>();

            currentState = stateNotSelectedTileEditor;
        }

        private void Update()
        {
            if(GameController.Instance.DebugModeOn)
            {
                currentState.RunUpdate(this);
            }
        }

        public void SwitchState(TileEditorStateIdentifier newState)
        {
            switch(newState)
            {
                case TileEditorStateIdentifier.NotSelected:
                    currentState.DisableState();
                    selectedTile = null;
                    currentState = stateNotSelectedTileEditor;
                    currentState.InitializeState(this);
                    break;

                case TileEditorStateIdentifier.Selected:
                    currentState.DisableState();
                    currentState = stateSelectedTileEditor;
                    currentState.InitializeState(this);
                    break;
            }
        }

        public void SelectTile(Tile selectedTile)
        {
            this.selectedTile = selectedTile;
        }
    }
}
