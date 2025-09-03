using UnityEngine;

namespace DebugModeUI
{
    public class DebugModeUIAndCanvasController : MonoBehaviour
    {
        private GameplayController gameplayController;

        [SerializeField] private GameObject debugUICanvas;

        private void Awake()
        {
            gameplayController = GetComponentInParent<GameplayController>();
        }

        private void Start()
        {
            if (!gameplayController.DebugModeOn) debugUICanvas.SetActive(false);
        }
    }
}
