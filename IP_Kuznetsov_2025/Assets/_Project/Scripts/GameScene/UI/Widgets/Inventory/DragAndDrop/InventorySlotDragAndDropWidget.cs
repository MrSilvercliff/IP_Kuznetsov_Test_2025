using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop
{
    public class InventorySlotDragAndDropWidget : MonoBehaviour, IMonoUpdatable, IInventorySlotDragAndDropListener
    {
        [SerializeField] private InventorySlotDragAndDropWidgetView _view;

        [Inject] private IMonoUpdater _monoUpdater;
        [Inject] private IDragAndDropController _dragAndDropController;

        private bool _dragInProcess;
        private bool _dropInProcess;

        private IInventorySlotController _dragInventorySlotController;
        private IInventorySlotController _dropInventorySlotController;

        private void OnEnable()
        {
            _monoUpdater.Subscribe(this);
            _dragAndDropController.Subscribe(this);
        }

        private void OnDisable()
        {
            _monoUpdater.UnSubscribe(this);
            _dragAndDropController.UnSubscribe(this);
        }

        public void Init()
        {
            _dragInProcess = false;
            _dropInProcess = false;
            _view.Init();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_dropInProcess)
                return;

            _view.OnUpdate(deltaTime);
        }

        public void OnDrag(IInventorySlotController inventorySlotController)
        {
            if (_dropInProcess)
                return;

            _dragInProcess = true;
            _dragInventorySlotController = inventorySlotController;
            _view.OnDragStart(inventorySlotController);
        }

        public void OnDrop(IInventorySlotController inventorySlotController)
        {
            if (_dropInProcess)
                return;

            _dropInProcess = true;

            _dragInProcess = false;
            _view.OnDragStop();

            _dropInventorySlotController = inventorySlotController;
            ProcessDrop();

            _dropInProcess = false;
        }

        private void ProcessDrop()
        { 
        }
    }
}