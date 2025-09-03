using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuUI
{
    public class ButtonWizardsWorkshop : BaseButton
    {
        protected override void DoThisOnClick()
        {
            GameController.Instance.LoadWizardsWorkshopScene();
        }
    }
}
