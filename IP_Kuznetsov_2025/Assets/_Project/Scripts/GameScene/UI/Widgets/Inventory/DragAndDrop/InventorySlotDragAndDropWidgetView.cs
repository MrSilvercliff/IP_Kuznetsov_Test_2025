using _Project.Scripts.GameScene.Input;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop
{
    public class InventorySlotDragAndDropWidgetView : MonoBehaviour, IMonoUpdatable
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _draggableRectTransform;
        [SerializeField] private InventorySlotWidget _draggedWidget;

        [Inject] private IInputController _inputController;

        private Vector2 _ratioMultiplier; // ratio between canvas rect size and screen size

        public void Init()
        {
            var screen = new Vector2(Screen.width, Screen.height);
            _ratioMultiplier = new Vector2(_rectTransform.rect.width / screen.x, _rectTransform.rect.height / screen.y);
        }

        public void OnDragStart(IInventorySlotController inventorySlotController)
        {
            _draggedWidget.Setup(inventorySlotController);
            _draggedWidget.SetActive(true);
        }

        public void OnDragStop()
        { 
            _draggedWidget.SetActive(false);
            _draggedWidget.Setup(null);
        }

        public void OnUpdate(float deltaTime)
        {
            var pointerPosition = _inputController.PointerPosition;
            
            var anchoredX = pointerPosition.x * _ratioMultiplier.x;
            var anchoredY = pointerPosition.y * _ratioMultiplier.y;
            var anchoredPosition = new Vector2(anchoredX, anchoredY);
            
            _draggableRectTransform.anchoredPosition = anchoredPosition;
        }
    }
}