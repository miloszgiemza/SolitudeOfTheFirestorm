using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardsWorkshop_PlayerProgressionModeUI
{
    public interface ISpellEquiped 
    {
        public int PosOnList { get; }
    }

    public class ButtonSpellManagementSpellEquiped : ButtonSpellsManagementControllerSpell, ISpellEquiped, IReturnObjectDataForTooltip
    {
        public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
        {
            return PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.PlayerEquipedSpells[PosOnList].ReturnTooltipText(GameController.Instance.GameLanguage);
        }
    }
}

