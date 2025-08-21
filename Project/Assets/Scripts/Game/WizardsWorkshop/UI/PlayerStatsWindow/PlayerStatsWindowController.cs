using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class PlayerStatsWindowController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMainActions;
        [SerializeField] private TextMeshProUGUI textSecondaryActions;
        [SerializeField] private TextMeshProUGUI textPlayerHP;

        private void Awake()
        {
            LoadUpToDateValuesOfStats();
        }

        private void LoadUpToDateValuesOfStats()
        {
            textMainActions.text = "Main Actions: 1";
            textSecondaryActions.text = "Secondary Actions: 1";
            textPlayerHP.text = "Player HP: 1";
        }
    }
}

