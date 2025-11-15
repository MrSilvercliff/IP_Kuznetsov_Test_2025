using _Project.Scripts.GameScene.Services.Tooltip;
using _Project.Scripts.Project.ObjectPools;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot
{
    public class InventorySlotDraggableTooltipWidget : InventorySlotDraggableWidget, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("INVENTORY SLOT DRAGGABLE TOOLTIP WIDGET")]
        [SerializeField] private Transform _tooltipAnchor;

        [Inject] private ITooltipService _tooltipService;

        private bool _needShowTooltip;

        public override void OnCreated()
        {
            base.OnCreated();
            _needShowTooltip = false;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            if (_isDragging)
                _tooltipService.HideInventorySlotTooltip(_inventorySlotController);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            if (_needShowTooltip)
                _tooltipService.ShowInventorySlotTooltip(_inventorySlotController, _tooltipAnchor.position);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);

            if (_needShowTooltip)
                _tooltipService.ShowInventorySlotTooltip(_inventorySlotController, _tooltipAnchor.position);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isDragging)
            {
                _needShowTooltip = true;
                return;
            }

            if (_dragAndDropController.DragInProcess)
            {
                _needShowTooltip = true;
                return;
            }

            if (_inventorySlotController.IsEmpty)
            {
                _needShowTooltip = true;
                return;
            }

            _tooltipService.ShowInventorySlotTooltip(_inventorySlotController, _tooltipAnchor.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _needShowTooltip = false;
            _tooltipService.HideInventorySlotTooltip(_inventorySlotController);
        }

        public class Pool : ProjectMonoMemoryPool<InventorySlotDraggableWidget> { }
    }
}