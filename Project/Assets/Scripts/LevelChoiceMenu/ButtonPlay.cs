using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelChoiceScene
{
    public class ButtonPlay : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.LoadGameplayScene();
        }
    }
}
