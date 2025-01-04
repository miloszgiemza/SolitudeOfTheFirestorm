using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class ButtonPlay : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.LoadLevelChoiceScene();
        }
    }
}
