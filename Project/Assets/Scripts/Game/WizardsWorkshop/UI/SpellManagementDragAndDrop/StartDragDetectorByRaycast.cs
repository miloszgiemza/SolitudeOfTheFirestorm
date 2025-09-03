using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class StartDragDetectorByRaycast : MonoBehaviour
    {
        public ISpellUnlocked ISpellUnlocked => iSpellUnlocked;

        private ISpellUnlocked iSpellUnlocked;

        [SerializeField] private GraphicRaycaster raycasterGraphic;
        private PointerEventData pointerEventData;
        [SerializeField] private EventSystem eventSystem;

        public bool CheckForSpellToDrag()
        {
            bool startDragging = false;

            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>();
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            raycasterGraphic.Raycast(pointerEventData, raycastResults);

            foreach (RaycastResult raycastResult in raycastResults)
            {
                if (!ReferenceEquals(raycastResult.gameObject.GetComponent<ISpellUnlocked>(), null))
                {
                    startDragging = true;
                    iSpellUnlocked = raycastResult.gameObject.GetComponent<ISpellUnlocked>();
                }
            }

            return startDragging;
        }
    }
}

