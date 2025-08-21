using UnityEngine;

using GameDatabase;

namespace DebugMode
{
    public class DebugModeItemsChoiceController : MonoBehaviour
    {
        private PlayerInventoryController playerInventoryController;

        private void Awake()
        {
            playerInventoryController = FindFirstObjectByType<PlayerInventoryController>();
        }

        public void TryUseSmallScroll(int smallScrollToUseIndexInDatabase)
        {
            Database.Instance.DatabaseInventory.SmallScrollsAll[smallScrollToUseIndexInDatabase].TryUseItem();
        }

        public void TryUseMixture(int mixtureToUseIndexInDatabase)
        {
            Database.Instance.DatabaseInventory.MixturesAll[mixtureToUseIndexInDatabase].TryUseItem();
        }

        public void TryToUseBigScroll(int bigScrollToUseIndexInDatabase)
        {
            Database.Instance.DatabaseInventory.BigScrollsAll[bigScrollToUseIndexInDatabase].TryUseItem();
        }
    }
}
