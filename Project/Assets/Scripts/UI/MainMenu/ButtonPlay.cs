using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuUI
{
    public class ButtonPlay : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.LoadLevelChoiceScene();
        }
    }
}
