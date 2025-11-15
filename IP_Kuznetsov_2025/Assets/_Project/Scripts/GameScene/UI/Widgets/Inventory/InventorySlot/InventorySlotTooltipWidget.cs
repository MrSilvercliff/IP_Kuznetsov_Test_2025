using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Tooltip;
using _Project.Scripts.Project.ObjectPools;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot
{
    public class InventorySlotTooltipWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _tooltipAnchor;

        [Inject] private IDragAndDropController _dragAndDropController;
        [Inject] private ITooltipService _tooltipService;

        private IInventorySlotController _inventorySlotController;

        public void Setup(IInventorySlotController inventorySlotController)
        { 
            _inventorySlotController = inventorySlotController;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_dragAndDropController.DragInProcess)
                return;

            if (_inventorySlotController.IsEmpty)
                return;

            _tooltipService.ShowInventorySlotTooltip(_inventorySlotController, _tooltipAnchor.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_dragAndDropController.DragInProcess)
                return;

            if (_inventorySlotController.IsEmpty)
                return;

            _tooltipService.HideInventorySlotTooltip(_inventorySlotController);
        }
    }
}