using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipValidObjectsDetector : MonoBehaviour
{
    public IReturnObjectDataForTooltip ObjectDataForTooltip => objectDataForTooltip;

    [SerializeField] private GraphicRaycaster raycasterGraphic;
    private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    private IReturnObjectDataForTooltip objectDataForTooltip;

    public bool CheckForObjects()
    {
        bool objectHasTooltipInterface = false;

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>();
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        raycasterGraphic.Raycast(pointerEventData, raycastResults);

        foreach(RaycastResult raycastResult in raycastResults)
        {
            if(!ReferenceEquals(raycastResult.gameObject.GetComponent<IReturnObjectDataForTooltip>(), null) )
            {
                objectHasTooltipInterface = true;
                objectDataForTooltip = raycastResult.gameObject.GetComponent<IReturnObjectDataForTooltip>();
            }
        }

        return objectHasTooltipInterface;
    }
}
