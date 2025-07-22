using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuSceneUI
{
    public class ButtonQuitGame : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.QuitGame();
        }
    }
}
