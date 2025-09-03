using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySceneUI
{
    public class MenuInGameController : MonoBehaviour
    {
        [SerializeField] private GameObject menu;

        private void Start()
        {
            HideMenu();
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
        }

        public void HideMenu()
        {
            menu.SetActive(false);
        }
    }
}
