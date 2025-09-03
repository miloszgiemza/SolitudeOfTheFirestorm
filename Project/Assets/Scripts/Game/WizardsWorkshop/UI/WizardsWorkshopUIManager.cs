using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class WizardsWorkshopUIManager : MonoBehaviour
    {
        public SpellsManagementUIController SpellsManagementUIController => spellsManagementUIController;

        private SpellsManagementUIController spellsManagementUIController;

        private void Awake()
        {
            spellsManagementUIController = GetComponentInChildren<SpellsManagementUIController>();
        }
    }
}

