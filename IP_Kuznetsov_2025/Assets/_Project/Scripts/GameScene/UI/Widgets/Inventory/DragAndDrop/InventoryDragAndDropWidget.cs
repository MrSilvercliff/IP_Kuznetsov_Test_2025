using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.GameScene.Input;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop
{
    public class InventoryDragAndDropWidget : MonoBehaviour, IPointerInputListener, IInventoryDragAndDropListener
    {
        [SerializeField] private InventoryDragAndDropWidgetView _view;

        [Inject] private IInputController _inputController;
        [Inject] private IDragAndDropController _dragAndDropController;
        [Inject] private IInventoryService _inventoryService;

        private bool _dragInProcess;
        private bool _dropInProcess;

        private bool _dropIsSafe;

        private IInventorySlotController _dragInventorySlotController;
        private IInventorySlotController _dropInventorySlotController;

        private void OnEnable()
        {
            _dragAndDropController.Subscribe(this);
            _inputController.Subscribe(this);
        }

        private void OnDisable()
        {
            _dragAndDropController.UnSubscribe(this);
            _inputController.UnSubscribe(this);
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
            /*
            if (actionPhase != InputActionPhase.Canceled)
                return;

            OnDrop(pointerPosition);
            */
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
            Debug.Log($"On_Drop I_Inventory_Slot_Controller");

            if (!_dragInProcess)
                return;

            if (_dropInProcess)
                return;

            _dropInProcess = true;

            _dragInProcess = false;
            _view.OnDragStop();

            _dropInventorySlotController = inventorySlotController;
            _dropIsSafe = true;
            ProcessDrop();

            _dropInProcess = false;
        }

        public void OnDrop(bool isSafe)
        {
            Debug.Log($"On_Drop pointer_Position");

            if (!_dragInProcess)
                return;

            if (_dropInProcess)
                return;

            _dropInProcess = true;

            _dragInProcess = false;
            _view.OnDragStop();

            _dropInventorySlotController = null;
            _dropIsSafe = isSafe;
            ProcessDrop();

            _dropInProcess = false;
        }

        private void ProcessDrop()
        {
            if (_dropInventorySlotController == null)
                ProcessDropNotToSlot();
            else
                ProcessDropToSlot();

            _dragInventorySlotController = null;
            _dropInventorySlotController = null;
            _dropIsSafe = false;
        }

        private void ProcessDropNotToSlot()
        {
            if (_dropIsSafe)
                return;

            _inventoryService.ClearInventorySlot(_dragInventorySlotController);
        }

        private void ProcessDropToSlot()
        { 
        }
    }
}