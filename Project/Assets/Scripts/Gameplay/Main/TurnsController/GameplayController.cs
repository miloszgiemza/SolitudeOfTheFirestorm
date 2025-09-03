using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameplayController : MonoBehaviour
{
    public bool DebugModeOn => GameController.Instance.DebugModeOn;

    public Action OnDefeat;

    public enum States
    {
        Idle,
        LevelIntro,
        Player,
        Enemy,
        Defeat,
        Victory,
    }

    public BaseGameState CurrentState => currentState;

    public Spawner Spawner => spawner;
    public Player Player => player;

    [SerializeField] private GameObject raycastBlockerScreenSpace;
    [SerializeField] private GameObject raycastBlockerWorldSpace;
    [SerializeField] private GameObject raycastBlockerDebugModeCanvas;

    private Spawner spawner;
    private Player player;

    private BaseGameState currentState;

    private StateGameIdle stateGameIdle = new StateGameIdle();
    private StateGameLevelIntro stateGameLevelIntro = new StateGameLevelIntro();
    private StatePlayerTurn statePlayerTurn= new StatePlayerTurn();
    private StateEnemiesTurn stateEnemiesTurn = new StateEnemiesTurn();
    private StateDefeat stateDefeat = new StateDefeat();
    private StateVictory stateVictory = new StateVictory();

    private void Awake()
    {
        spawner = GetComponentInChildren<Spawner>();
        player = GetComponentInChildren<Player>();
    }

    private void Start()
    {
        currentState = stateGameIdle;
        StartCoroutine(StartGameWhenEverythingInitialised());
    }

    private void Update()
    {
        currentState.RunUpdate(this);
    }

    public void SwitchState(States newState)
    {
        switch(newState)
        {
            case States.Idle:
                currentState = stateGameIdle;
                currentState.StartTurn(this);
                break;

            case States.LevelIntro:
                currentState = stateGameLevelIntro;
                currentState.StartTurn(this);
                break;

            case States.Player:
                currentState = statePlayerTurn;
                currentState.StartTurn(this);
                break;

            case States.Enemy:
                currentState = stateEnemiesTurn;
                    currentState.StartTurn(this);
                break;

            case States.Defeat:
                currentState = stateDefeat;
                currentState.StartTurn(this);
                break;

            case States.Victory:
                currentState = stateVictory;
                currentState.StartTurn(this);
                break;
        }
    }

    public bool CheckIfPlayerWon(bool allWavesSpawned, bool noEnemiesOnMap, int playerHealth)
    {
        bool playerWon = false;

        if(allWavesSpawned && noEnemiesOnMap && playerHealth > 0)
        {
            playerWon = true;
        }

        return playerWon;
    }

    public IEnumerator StartGameWhenEverythingInitialised()
    {
        yield return new WaitUntil(() => LevelLoader.Instance.Initialised);
        SwitchState(States.LevelIntro);
    }

    public void DisableRaycastBlockers()
    {
        raycastBlockerScreenSpace.SetActive(false);
        raycastBlockerWorldSpace.SetActive(false);
        raycastBlockerDebugModeCanvas.SetActive(false);
    }
}
