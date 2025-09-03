using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public class ButtonReturnToMainMenu : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.LoadMainMenuScene();
        }
    }
}
