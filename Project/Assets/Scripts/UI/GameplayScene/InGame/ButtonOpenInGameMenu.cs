using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySceneUI
{
    public class ButtonOpenInGameMenu : BaseButton
    {
        private MenuInGameController menuInGameController;

        protected override void Awake()
        {
            base.Awake();
            menuInGameController = FindObjectOfType<MenuInGameController>();
        }

        protected override void DoThisOnClick()
        {
            menuInGameController.ShowMenu();
        }
    }
}

