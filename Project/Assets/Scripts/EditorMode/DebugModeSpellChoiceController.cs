using UnityEngine;

using GameDatabase;

namespace DebugMode
{
    public class DebugModeSpellChoiceController : MonoBehaviour
    {
        public void ChooseCurrentSpellFromAllGameSpells(int spellIndexInGameDatabase)
        {
            if(!ReferenceEquals(PlayerStateIdleEvents.OnCastSpell, null))PlayerStateIdleEvents.OnCastSpell.Invoke(spellIndexInGameDatabase);
        }
    }
}
