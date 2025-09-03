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

        [SerializeField] private int unlockedLevelsNumber = 1;

        [SerializeField] private LevelEnemiesRandomisedPreset[] levelsPresets;

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
            }
        }

        public LevelEnemiesRandomisedPreset ReturnLevelPreset(int presetNumber)
        {
            return levelsPresets[presetNumber];
        }
    }
}
