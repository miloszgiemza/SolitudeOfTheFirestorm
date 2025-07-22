using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsWorkshop_PlayerProgressionMode
{
    public class WizardsWorkshopManager : MonoBehaviour
    {
        public SpellsManagementController SpellsManagementController => spellsManagementController;

        private SpellsManagementController spellsManagementController;

        private void Awake()
        {
            spellsManagementController = GetComponentInChildren<SpellsManagementController>();
        }
    }
}
