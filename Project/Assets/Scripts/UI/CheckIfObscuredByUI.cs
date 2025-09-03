using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckIfObscuredByUI : MonoBehaviour
{
    public static CheckIfObscuredByUI Instance => instance;
    private static CheckIfObscuredByUI instance;

    [SerializeField] private GraphicRaycaster raycasterGraphic;
    private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    private void Awake()
    {
        if(!ReferenceEquals(CheckIfObscuredByUI.Instance, null))
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

    public bool CheckIfObscured()
    {
        bool obscured = false;

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>();
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        raycasterGraphic.Raycast(pointerEventData, raycastResults);

        if(!ReferenceEquals(raycastResults, null) && raycastResults.Count > 0)
        {
            obscured = true;
        }

        return obscured;
    }
}
