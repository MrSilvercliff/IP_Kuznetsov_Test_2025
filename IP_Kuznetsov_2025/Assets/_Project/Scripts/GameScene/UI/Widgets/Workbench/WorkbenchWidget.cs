using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.GameScene.UI.Widgets.Inventory;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Workbench
{
    public class WorkbenchWidget : MonoBehaviour
    {
        [SerializeField] private InventoryWidget _inventoryWidget;
        [SerializeField] private InventorySlotWidget _resultItemSlotWidget;
        [SerializeField] private Button _buttonCraft;

        [Inject] private IGameSceneObjectPoolService _objectPoolService;

        private IInventoryController _inventoryController;
        private IInventorySlotController _resultItemSlotController;

        private void Awake()
        {
            _buttonCraft.onClick.AddListener(OnButtonCraftClick);
        }

        public void Init()
        {
            InitInventoryWidget();
            InitResultItemSlotWidget();
        }

        private void InitInventoryWidget()
        {
            var inventoryControllerPool = _objectPoolService.InventoryControllerPool;
            _inventoryController = inventoryControllerPool.SpawnWorkbenchInventoryController();
            _inventoryWidget.Init();
            _inventoryWidget.Setup(_inventoryController);
        }

        private void InitResultItemSlotWidget()
        {
            var inventorySlotControllerPool = _objectPoolService.InventorySlotControllerPool;
            _resultItemSlotController = inventorySlotControllerPool.Spawn();
            _resultItemSlotWidget.Setup(_resultItemSlotController);
        }

        private void OnButtonCraftClick()
        { 
        }
    }
}