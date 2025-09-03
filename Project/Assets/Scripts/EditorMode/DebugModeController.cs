using UnityEngine;

namespace DebugMode
{
    public class DebugModeController : MonoBehaviour
    {
        GameplayController gameplayController;

        private void Awake()
        {
            gameplayController = GetComponentInParent<GameplayController>();
        }

        public void SetCurrentSpellFromAllGameSpells(BaseSpell newSpell)
        {
        }
    }
}
