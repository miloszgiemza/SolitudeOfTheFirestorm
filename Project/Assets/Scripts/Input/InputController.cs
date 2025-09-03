using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance => instance;

    private static InputController instance;

    public MainInputAssetWrapper MainInputAssetsWrapper => mainInputAssetsWrapper;

    private MainInputAssetWrapper mainInputAssetsWrapper;

    public bool ButtonChooseSpellAtStartOfTurnPressed => buttonChooseSpellAtStartOfTurnPressed;
    private bool buttonChooseSpellAtStartOfTurnPressed = false;

    private void Awake()
    {
        if(!ReferenceEquals(InputController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);

            mainInputAssetsWrapper = new MainInputAssetWrapper();
        }
    }

    private void OnEnable()
    {
        mainInputAssetsWrapper.MobileDevicesMap.Enable();
    }

    private void OnDisable()
    {
        if(!ReferenceEquals(mainInputAssetsWrapper, null))mainInputAssetsWrapper.MobileDevicesMap.Disable();
    }
}
