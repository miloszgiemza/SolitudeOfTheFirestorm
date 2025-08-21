using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public abstract class ButtonSpellsManagementControllerSpell : MonoBehaviour
    {
        public int PosOnList => posOnList;

        [SerializeField] private Image buttonImage;

        private int posOnList;

        public void Initialize(int posOnList, Sprite spellIcon)
        {
            this.posOnList = posOnList;
            buttonImage.sprite = spellIcon;
        }
    }
}
