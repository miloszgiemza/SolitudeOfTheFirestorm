using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelChoiceScene
{
    public class SelectedLevelWindowController : MonoBehaviour
    {
        public static SelectedLevelWindowController Instance => instance;
        private static SelectedLevelWindowController instance;

        [SerializeField] private SelectedLevelWindow selectedLevelWindow;

        private void Awake()
        {

            if(!ReferenceEquals(SelectedLevelWindowController.Instance, null))
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private void OnDisable()
        {
            instance = null;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void ShowSelectedLevelWindow(EnemyData[] levelEnemiesPreset, CountOfEnemiesOfTier[] countOfEnemiesOfTierPreset)
        {
            selectedLevelWindow.gameObject.SetActive(true);
            selectedLevelWindow.InitialzieWindow(levelEnemiesPreset, countOfEnemiesOfTierPreset);
        }
    }
}