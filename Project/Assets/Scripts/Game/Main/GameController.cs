using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public enum ScenesIdentifiers
{
    MainMenu,
    LevelChoiceMenu,
    Gameplay,
    WizardsWorkshop
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

    public bool DebugModeOn => debugModeOn;

    public GameLanguage GameLanguage => gameLanguage;
    public LevelEnemiesRandomisedPreset CurrenLevelPresetToLoad => currenLevelPresetToLoad;
    public int MinPlayerEquipedSpellsNumber => minPlayerEquipedSpellsNumber;

    [SerializeField] private bool debugModeOn = false;

    private GameLanguage gameLanguage => GameLanguage.ENG;

    private LevelEnemiesRandomisedPreset currenLevelPresetToLoad;

    [SerializeField] private int minPlayerEquipedSpellsNumber = 3;

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

    private void Start()
    {
        int[,] array = { { 1, 2, 3 }, { 4, 5, 6 } };
        Debug.Log("Array 1/2: " + array[1, 2]);
    }

    public void LoadCurrenLevelPreset(LevelEnemiesRandomisedPreset newLevelPreset)
    {
        currenLevelPresetToLoad = newLevelPreset;
    }

    #region Application
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

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

    public void LoadWizardsWorkshopScene()
    {
        SceneManager.LoadScene(ScenesIdentifiers.WizardsWorkshop.ToString());
    }
    #endregion
}
