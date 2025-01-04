using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUIController : BaseDynamicallyGeneratedButtonsController
{
    public static VictoryUIController Instance => instance;
    private static VictoryUIController instance;

    [SerializeField] private GameObject window;

    private void Awake()
    {
        if(!ReferenceEquals(VictoryUIController.Instance, null))
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void HideWindow()
    {
        window.SetActive(false);
    }

    public void ShowWindow(int currentButtonsCount, List<Sprite> buttonsSprites, Action<int> buttonsMethod)
    {
        window.SetActive(true);

        UpdateButtons(currentButtonsCount, buttonsSprites, buttonsMethod);
    }
}
