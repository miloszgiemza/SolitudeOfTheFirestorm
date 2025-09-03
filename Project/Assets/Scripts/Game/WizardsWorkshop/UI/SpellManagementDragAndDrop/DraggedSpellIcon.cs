using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class DraggedSpellIcon : MonoBehaviour
    {
        [SerializeField] private Image iconImage;

        public void LoadSpellIcon(Sprite newIcon)
        {
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = newIcon;
        }

        public void HideIcon()
        {
            iconImage.gameObject.SetActive(false);
        }
        
        public void UpdateIconPosition(Vector2 newPos)
        {
            iconImage.rectTransform.position = newPos;
        }
    }
}
