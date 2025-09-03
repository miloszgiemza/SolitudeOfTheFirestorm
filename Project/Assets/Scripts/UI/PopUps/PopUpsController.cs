using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpID
{
    PlayerWon,
    PlayerLost
}

public class PopUpsController : MonoBehaviour
{
    private GameplayController gameplayController;

    [SerializeField] private GameObject popUpPlayerWon;
    [SerializeField] private GameObject popUpPlayerLost;

    private void Awake()
    {
        gameplayController = GetComponentInParent<GameplayController>();
    }

    private void OnEnable()
    {
        gameplayController.OnDefeat += ShowPopUpDefeat;
    }

    private void OnDisable()
    {
        gameplayController.OnDefeat -= ShowPopUpDefeat;
    }

    private void ShowOrHidePopUp(PopUpID popUpID, bool show)
    {
        switch(popUpID)
        {
            case PopUpID.PlayerWon:
                popUpPlayerWon.SetActive(show);
                break;

            case PopUpID.PlayerLost:
                popUpPlayerLost.SetActive(show);
                    break;
        }
    }

    private void ShowPopUpDefeat()
    {
        popUpPlayerLost.SetActive(true);
    }
}
