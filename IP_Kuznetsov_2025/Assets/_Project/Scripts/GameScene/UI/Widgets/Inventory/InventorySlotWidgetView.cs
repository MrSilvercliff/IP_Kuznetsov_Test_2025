using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.Project.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        [Inject] private IProjectSpriteConfig _spriteConfig;

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
            _emptyState.SetActive(false);
            _fullState.SetActive(true);

            var slotItem = inventorySlotController.Item;

            _iconImage.sprite = _spriteConfig.GetGameItemIcon(slotItem.IconId);
            _countText.gameObject.SetActive(slotItem.IsStackable);
            _countText.text = slotItem.Count.ToString();
        }
    }
}