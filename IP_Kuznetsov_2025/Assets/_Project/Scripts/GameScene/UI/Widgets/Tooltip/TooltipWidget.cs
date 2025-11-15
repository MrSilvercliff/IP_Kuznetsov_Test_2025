using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Tooltip;
using _Project.Scripts.Project.Configs;
using _Project.Scripts.Project.Extensions;
using _Project.Scripts.Project.Monobeh;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Tooltip
{
    public class TooltipWidget : ProjectMonoBehaviour
    {
        [Header("TOOLTIP WIDGET")]
        [SerializeField] private Image _itemIconImage;
        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private TMP_Text _itemDescriptionText;

        [Inject] private IProjectSpriteConfig _projectSpriteConfig;

        public void Setup(ITooltipInfo tooltipInfo)
        {
            SetupPosition(tooltipInfo.Position);
            SetupInventorySlot(tooltipInfo.InventorySlotController);
        }

        private void SetupPosition(Vector3 position)
        { 
            transform.position = position;
            transform.ResetLocalZ();
        }

        private void SetupInventorySlot(IInventorySlotController inventorySlotController)
        {
            if (inventorySlotController.IsEmpty)
                return;

            var slotItem = inventorySlotController.Item;
            SetupIcon(slotItem);
            SetupNameText(slotItem);
            SetupCountText(slotItem);
            SetupDescriptionText(slotItem);
        }

        private void SetupIcon(IGameItem gameItem)
        {
            var icondId = gameItem.IconId;
            var sprite = _projectSpriteConfig.GetGameItemIcon(icondId);
            _itemIconImage.sprite = sprite;
        }

        private void SetupNameText(IGameItem gameItem)
        { 
            // TODO: localization
            _itemNameText.text = gameItem.Name;
        }

        private void SetupCountText(IGameItem gameItem)
        {
            _itemCountText.gameObject.SetActive(gameItem.IsStackable);
            var countText = $"(x{gameItem.Count})";
            _itemCountText.text = countText;
        }

        private void SetupDescriptionText(IGameItem gameItem)
        {
            // TODO: localization
            _itemDescriptionText.text = gameItem.Description;
        }
    }
}