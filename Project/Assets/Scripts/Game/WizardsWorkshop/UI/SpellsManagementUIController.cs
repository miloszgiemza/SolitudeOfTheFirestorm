using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WizardsWorkshop_PlayerProgressionMode;

//zrobiæ na maszynie stanów 
//wybieranie -> dragging

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class SpellsManagementUIController : MonoBehaviour
    {
        private WizardsWorkshopManager wizardsWorkshopManager;

        [SerializeField] Transform unlockedSpellsParent;
        [SerializeField] Transform equipedSpellsParent;

        [SerializeField] GameObject spellEquipedButtonPrefab;
        [SerializeField] GameObject spellUnlockedButtonPrefab;


        private List<ButtonSpellsManagementControllerSpell> unlockedSpellsButtonsPool = new List<ButtonSpellsManagementControllerSpell>();
        private List<ButtonSpellsManagementControllerSpell> equipedSpellsButtonsPool = new List<ButtonSpellsManagementControllerSpell>();

        private void Awake()
        {
            wizardsWorkshopManager = GetComponentInParent<WizardsWorkshopManager>();

            RefreshSpellListsUI(spellUnlockedButtonPrefab, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells, unlockedSpellsButtonsPool, unlockedSpellsParent);
            RefreshSpellListsUI(spellEquipedButtonPrefab, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells, equipedSpellsButtonsPool, equipedSpellsParent);
        }

        #region GeneratingAndPooler
        private void RefreshSpellListsUI(GameObject buttonPrefab, List<BaseSpell> spellsList, List<ButtonSpellsManagementControllerSpell> spellsButtonsPool, Transform parentTransform)
        {
            for(int i = 0; i < spellsButtonsPool.Count; i++)
            {
                spellsButtonsPool[i].gameObject.SetActive(false);
            }

            for(int i = 0; i < spellsList.Count; i++)
            {
                //use pool
                if (i < spellsButtonsPool.Count)
                {
                    spellsButtonsPool[i].gameObject.SetActive(true);

                    spellsButtonsPool[i].Initialize(i, spellsList[i].SpellIcon);
                }
                //expand pool
                else
                {
                    spellsButtonsPool.Add( Instantiate(buttonPrefab, parentTransform).GetComponent<ButtonSpellsManagementControllerSpell>() );
                    spellsButtonsPool[i].Initialize(i, spellsList[i].SpellIcon);
                }
            }
        }

        public void RefreshSpellListsUIEquiedSpells(List<BaseSpell> spellsList)
        {
            RefreshSpellListsUI(spellEquipedButtonPrefab, PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells, equipedSpellsButtonsPool, equipedSpellsParent);
        }
        #endregion
    }
}
