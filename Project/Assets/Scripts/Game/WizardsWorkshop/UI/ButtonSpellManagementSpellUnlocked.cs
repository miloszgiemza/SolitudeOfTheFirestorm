using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public interface ISpellUnlocked 
    { 
        public int PosOnList { get; }
    }

    public class ButtonSpellManagementSpellUnlocked : ButtonSpellsManagementControllerSpell, ISpellUnlocked, IReturnObjectDataForTooltip
    {
        public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
        {
            return PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerUnlockedSpells[PosOnList].ReturnTooltipText(GameController.Instance.GameLanguage);
        }
    }
}

