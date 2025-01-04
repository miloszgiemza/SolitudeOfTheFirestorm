using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelChoiceScene
{
    public class LevelsPresetsController : MonoBehaviour
    {
        public static LevelsPresetsController Instance => instance;
        private static LevelsPresetsController instance;

        public int UnlockedLevelsNumber => unlockedLevelsNumber;
        public LevelEnemiesRandomisedPreset[] UnlockedLevels => unlockedLevels;

        [SerializeField] private int unlockedLevelsNumber = 1;

        [SerializeField] private LevelEnemiesRandomisedPreset[] levelsPresets;

        private LevelEnemiesRandomisedPreset[] unlockedLevels;

        private void Awake()
        {
            if(!ReferenceEquals(LevelsPresetsController.Instance, null))
            {
                Destroy(this);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);

                TryLoadProgressionAndInitialise();
            }
        }

        private void TryLoadProgressionAndInitialise()
        {
            if(SaveLoadController.CheckIfSaveFileExists())unlockedLevelsNumber = PlayerProgressionController.Instance.PlayerProgression.UnlockedLevelsNumber;

            unlockedLevelsNumber = Mathf.Clamp(unlockedLevelsNumber, 1, levelsPresets.Length);
            unlockedLevels = new LevelEnemiesRandomisedPreset[unlockedLevelsNumber];

            for(int i=0; i < unlockedLevelsNumber && i < levelsPresets.Length; i++)
            {
                unlockedLevels[i] = levelsPresets[i];
            }
        }

        public void UpdateUnlockedLevels(int currentUnlockedLevelsNumber)
        {
            unlockedLevelsNumber = Mathf.Clamp(currentUnlockedLevelsNumber, 1, levelsPresets.Length);
            unlockedLevels = new LevelEnemiesRandomisedPreset[unlockedLevelsNumber];

            for (int i = 0; i < unlockedLevelsNumber && i < levelsPresets.Length; i++)
            {
                unlockedLevels[i] = levelsPresets[i];
            }
        }

        public LevelEnemiesRandomisedPreset ReturnUnlockedLevelPreset(int presetNumber)
        {
            return unlockedLevels[presetNumber];
        }
    }
}
