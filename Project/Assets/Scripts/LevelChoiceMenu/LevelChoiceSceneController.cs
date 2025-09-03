using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelChoiceScene
{
    public class LevelChoiceSceneController : MonoBehaviour
    {
        public void LoadLevelAndStartGameplay(int levelPresetNumber)
        {
            GameController.Instance.LoadCurrenLevelPreset(LevelsPresetsController.Instance.ReturnLevelPreset(levelPresetNumber));
            GameController.Instance.LoadGameplayScene();
        }
    }
}

