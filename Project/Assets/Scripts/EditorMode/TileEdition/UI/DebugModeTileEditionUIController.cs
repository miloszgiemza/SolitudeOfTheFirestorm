using UnityEngine;

using DebugMode;

namespace DebugModeUI
{
    public class DebugModeTileEditionUIController : MonoBehaviour
    {
        private DebugModeTileEditionController debugModeTileEditionController;

        [SerializeField] private GameObject tileEditionWindow;

        private void Awake()
        {
            debugModeTileEditionController = FindFirstObjectByType<DebugModeTileEditionController>();
        }

        private void OnEnable()
        {
            debugModeTileEditionController.OnTileSelected += ShowTileEditionWIndow;
        }

        private void Start()
        {
            HideTileEditionWIndow();
        }

        private void OnDisable()
        {
            debugModeTileEditionController.OnTileSelected -= ShowTileEditionWIndow;
        }

        public void ShowTileEditionWIndow()
        {
            tileEditionWindow.SetActive(true);
        }

        public void HideTileEditionWIndow()
        {
            tileEditionWindow.SetActive(false);
        }
    }
}
