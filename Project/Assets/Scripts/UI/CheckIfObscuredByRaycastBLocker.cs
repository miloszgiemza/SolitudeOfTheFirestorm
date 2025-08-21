using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CheckIfObscuredByRaycastBLocker : MonoBehaviour
{
    public static CheckIfObscuredByRaycastBLocker Instance => instance;
    private static CheckIfObscuredByRaycastBLocker instance;

    [SerializeField] private GraphicRaycaster raycasterGraphic;
    private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    private void Awake()
    {
        if (!ReferenceEquals(CheckIfObscuredByRaycastBLocker.Instance, null))
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

        foreach (RaycastResult raycastResult in raycastResults)
        {
            if (!ReferenceEquals(raycastResult.gameObject.GetComponent<RaycastBlocker>(), null))
            {
                obscured = true;
            }
        }

        return obscured;
    }
}
