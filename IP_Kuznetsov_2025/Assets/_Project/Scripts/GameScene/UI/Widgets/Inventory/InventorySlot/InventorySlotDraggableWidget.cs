using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.Project.Monobeh;
using _Project.Scripts.Project.ObjectPools;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot
{
    public class InventorySlotDraggableWidget : InventorySlotWidget, IDragHandler, IEndDragHandler, IDropHandler
    {
        [Inject] private IDragAndDropConfig _dragAndDropConfig;
        [Inject] protected IDragAndDropController _dragAndDropController;

        private InventorySlotDraggableWidgetView _draggableWidgetView;

        protected bool _isDragging;

        public override void OnCreated()
        {
            base.OnCreated();
            _draggableWidgetView = (InventorySlotDraggableWidgetView)_view;
            _isDragging = false;
        }

        public override void OnDespawned()
        {
            base.OnDespawned();
            _isDragging = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
                return;

            if (_dragAndDropController.DragInProcess)
                return;

            if (_inventorySlotController.IsEmpty)
                return;

            if (eventData.delta.sqrMagnitude < _dragAndDropConfig.DragThresholdSquared)
                return;

            _isDragging = true;
            _tooltipWidget?.OnPointerExit(null);
            _dragAndDropController.OnDrag(_inventorySlotController);
            _draggableWidgetView.OnDrag(eventData);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            _draggableWidgetView.OnEndDrag(eventData);
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            _isDragging = false;
            _dragAndDropController.OnDrop(_inventorySlotController);
            _draggableWidgetView.OnDrop(eventData);
        }

        public class Pool : ProjectMonoMemoryPool<InventorySlotDraggableWidget> { }
    }
}