using _Project.Scripts.GameScene.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory
{
    public class InventorySlotWidgetView : MonoBehaviour
    {
        [Header("EMPTY STATE")]
        [SerializeField] private GameObject _emptyState;

        [Header("FULL STATE")]
        [SerializeField] private GameObject _fullState;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _countText;

        public void Refresh(IInventorySlotController inventorySlotController)
        {
            if (inventorySlotController.IsEmpty)
                ShowEmptyState(inventorySlotController);
            else
                ShowFullState(inventorySlotController);
        }

        private void ShowEmptyState(IInventorySlotController inventorySlotController)
        { 
            _emptyState.SetActive(true);
            _fullState.SetActive(false);
        }

        private void ShowFullState(IInventorySlotController inventorySlotController)
        {
            _emptyState.SetActive(true);
            _fullState.SetActive(false);

            var slotItem = inventorySlotController.Item;

            _countText.gameObject.SetActive(slotItem.IsStackable);
            _countText.text = slotItem.Count.ToString();
        }
    }
}