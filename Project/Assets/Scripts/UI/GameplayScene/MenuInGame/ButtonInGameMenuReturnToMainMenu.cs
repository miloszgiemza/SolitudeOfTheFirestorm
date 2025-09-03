using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySceneUI
{
    public class ButtonInGameMenuReturnToMainMenu : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.LoadMainMenuScene();
        }
    }

}
