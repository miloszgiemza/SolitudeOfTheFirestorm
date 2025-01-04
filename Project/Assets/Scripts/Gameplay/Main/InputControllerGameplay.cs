using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerGameplay : MonoBehaviour
{
    public static InputControllerGameplay Instance => instance;

    private static InputControllerGameplay instance;

    public MainInputAssetWrapper MainInputAssetsWrapper => mainInputAssetsWrapper;

    private MainInputAssetWrapper mainInputAssetsWrapper;

    public bool ButtonChooseSpellAtStartOfTurnPressed => buttonChooseSpellAtStartOfTurnPressed;
    private bool buttonChooseSpellAtStartOfTurnPressed = false;

    private void Awake()
    {
        if(!ReferenceEquals(InputControllerGameplay.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        mainInputAssetsWrapper = new MainInputAssetWrapper();
    }

    private void OnEnable()
    {
        mainInputAssetsWrapper.MobileDevicesMap.Enable();
    }

    private void OnDisable()
    {
        mainInputAssetsWrapper.MobileDevicesMap.Disable();
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
