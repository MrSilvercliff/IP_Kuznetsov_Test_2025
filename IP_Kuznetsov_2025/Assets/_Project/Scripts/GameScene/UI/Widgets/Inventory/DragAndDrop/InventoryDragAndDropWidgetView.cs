using _Project.Scripts.GameScene.Input;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop
{
    public class InventoryDragAndDropWidgetView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _draggableRectTransform;
        [SerializeField] private InventorySlotWidget _draggedWidget;

        private Vector2 _ratioMultiplier; // ratio between canvas rect size and screen size

        public void Init()
        {
            var screen = new Vector2(Screen.width, Screen.height);
            
            var ratioMultiplierX = _rectTransform.rect.width / screen.x;
            var ratioMultiplierY = _rectTransform.rect.height / screen.y;
            _ratioMultiplier = new Vector2(ratioMultiplierY, ratioMultiplierY);
        }

        public void OnDragStart(IInventorySlotController inventorySlotController, Vector2 pointerPosition)
        {
            _draggedWidget.Setup(inventorySlotController);
            OnPointerPositionInput(pointerPosition);
            _draggedWidget.SetActive(true);
        }

        public void OnDragStop()
        { 
            _draggedWidget.SetActive(false);
        }

        public void OnPointerPositionInput(Vector2 pointerPosition)
        {
            var anchoredX = pointerPosition.x * _ratioMultiplier.x;
            var anchoredY = pointerPosition.y * _ratioMultiplier.y;
            var anchoredPosition = new Vector2(anchoredX, anchoredY);
            
            _draggableRectTransform.anchoredPosition = anchoredPosition;
        }
    }
}