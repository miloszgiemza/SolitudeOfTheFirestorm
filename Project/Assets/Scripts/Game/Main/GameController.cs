using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public enum ScenesIdentifiers
{
    MainMenu,
    LevelChoiceMenu,
    Gameplay
}

public enum GameLanguage
{
    ENG,
    PL
}

public class GameController : MonoBehaviour
{
    public static GameController Instance => instance;

    private static GameController instance;

    public GameLanguage GameLanguage => gameLanguage;
    public LevelEnemiesRandomisedPreset CurrenLevelPresetToLoad => currenLevelPresetToLoad;

    private GameLanguage gameLanguage => GameLanguage.ENG;

    private LevelEnemiesRandomisedPreset currenLevelPresetToLoad;

    private void Awake()
    {
        if(!ReferenceEquals(GameController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadCurrenLevelPreset(LevelEnemiesRandomisedPreset newLevelPreset)
    {
        currenLevelPresetToLoad = newLevelPreset;
    }

    #region Scenes
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(ScenesIdentifiers.MainMenu.ToString());
    }
    public void LoadLevelChoiceScene()
    {
        SceneManager.LoadScene(ScenesIdentifiers.LevelChoiceMenu.ToString());
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(ScenesIdentifiers.Gameplay.ToString());
    }
    #endregion
}
