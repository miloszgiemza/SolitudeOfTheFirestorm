using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class SpellsManagementDragAndDrop : MonoBehaviour 
    {
        public DraggedSpellIcon DraggedSpellIcon => draggedSpellIcon;

        public StartDragDetectorByRaycast RaycasterSpellToDrag => raycasterSpellToDrag;
        public EndDragDetectionByRaycast RaycasterSlotToDrop => raycasterSlotToDrop;
        public SpellsManagementUIController SpellsManagementUIController => spellsManagementUIController;

        public SpellsManagementDragAndDropStateNotDragging StateNotDragging => stateNotDragging;
        public SpellsManagementDragAndDropStateDragging StateDragging => stateDragging;

        public BaseSpell SpellBeingDragged => spellBeingDragged;

        [SerializeField] private DraggedSpellIcon draggedSpellIcon;
        [SerializeField] private StartDragDetectorByRaycast raycasterSpellToDrag;
        [SerializeField] private EndDragDetectionByRaycast raycasterSlotToDrop;
        [SerializeField] private SpellsManagementUIController spellsManagementUIController;

        private SpellsManagementDragAndDropBaseState currentState;

        private SpellsManagementDragAndDropStateNotDragging stateNotDragging = new SpellsManagementDragAndDropStateNotDragging();
        private SpellsManagementDragAndDropStateDragging stateDragging = new SpellsManagementDragAndDropStateDragging();

        private BaseSpell spellBeingDragged;

        private void Awake()
        {
            currentState = stateNotDragging;
        }

        private void Update()
        {
            currentState.Update(this);
        }

        public void SwitchState(SpellsManagementDragAndDropBaseState newState)
        {
            currentState = newState;
        }

        public void UpdateSpellBeingDragged(BaseSpell spellToDrag)
        {
            spellBeingDragged = spellToDrag;
        }
    }
}
