using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.GameScene.Input;
using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop
{
    public class InventorySlotDragAndDropWidget : MonoBehaviour, IPointerInputListener, IInventorySlotDragAndDropListener
    {
        [SerializeField] private InventorySlotDragAndDropWidgetView _view;

        [Inject] private IInputController _inputController;
        [Inject] private IDragAndDropController _dragAndDropController;

        private bool _dragInProcess;
        private bool _dropInProcess;

        private IInventorySlotController _dragInventorySlotController;
        private IInventorySlotController _dropInventorySlotController;

        private void OnEnable()
        {
            _inputController.Subscribe(this);
            _dragAndDropController.Subscribe(this);
        }

        private void OnDisable()
        {
            _inputController.UnSubscribe(this);
            _dragAndDropController.UnSubscribe(this);
        }

        public void Init()
        {
            _dragInProcess = false;
            _dropInProcess = false;
            _view.Init();
        }

        public void OnPointerPositionInput(Vector2 pointerPosition)
        {
            if (!_dragInProcess)
                return;

            if (_dropInProcess)
                return;

            _view.OnPointerPositionInput(pointerPosition);
        }

        public void OnPointerLeftClickInput(InputActionPhase actionPhase, Vector2 pointerPosition)
        {
            if (actionPhase == InputActionPhase.Canceled)
            {
                
            }
        }

        public void OnDrag(IInventorySlotController inventorySlotController)
        {
            if (_dropInProcess)
                return;

            _dragInProcess = true;
            var pointerPosition = _inputController.PointerPosition;
            _dragInventorySlotController = inventorySlotController;
            _view.OnDragStart(inventorySlotController, pointerPosition);
        }

        public void OnDrop(IInventorySlotController inventorySlotController)
        {
            if (!_dragInProcess)
                return;

            if (_dropInProcess)
                return;

            _dropInProcess = true;

            _dragInProcess = false;
            _view.OnDragStop();

            _dropInventorySlotController = inventorySlotController;
            ProcessDrop();

            _dropInProcess = false;
        }

        private void OnDrop(Vector2 pointerPosition)
        { 
            ProcessDrop();
        }

        private void ProcessDrop()
        { 
        }
    }
}