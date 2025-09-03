using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySceneUI
{
    public class ButtonInGameMenuUnpause : BaseButton
    {
        private MenuInGameController menuInGameController;

        protected override void Awake()
        {
            base.Awake();
            menuInGameController = GetComponentInParent<MenuInGameController>();
        }

        protected override void DoThisOnClick()
        {
            menuInGameController.HideMenu();
        }
    }
}

