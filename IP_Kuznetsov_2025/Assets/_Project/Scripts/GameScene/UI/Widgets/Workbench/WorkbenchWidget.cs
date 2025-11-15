using _Project.Scripts.GameScene.Services.Craft;
using _Project.Scripts.GameScene.UI.Events;
using _Project.Scripts.GameScene.UI.Widgets.Inventory;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.EventBus.Async;

namespace _Project.Scripts.GameScene.UI.Widgets.Workbench
{
    public class WorkbenchWidget : MonoBehaviour
    {
        [SerializeField] private InventoryWidget _inventoryWidget;
        [SerializeField] private InventorySlotWidget _resultItemSlotWidget;
        [SerializeField] private Button _buttonCraft;

        [Inject] private IEventBusAsync _eventBus;
        [Inject] private ICraftService _craftService;

        private void Awake()
        {
            _buttonCraft.onClick.AddListener(OnButtonCraftClick);
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<CraftResultItemChangedEvent>(OnCraftResultItemChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus.UnSubscribe<CraftResultItemChangedEvent>(OnCraftResultItemChangedEvent);
        }

        public void Init()
        {
            _inventoryWidget.Init();
        }

        public void Setup()
        { 
            RefreshInventoryWidget();
            RefreshResultItemSlotWidget();
            RefreshCraftButton();
        }

        private void RefreshInventoryWidget()
        {
            var inventoryController = _craftService.InventoryService.InventoryController;
            _inventoryWidget.Setup(inventoryController);
        }

        private void RefreshResultItemSlotWidget()
        {
            var craftResultSlotController = _craftService.InventoryService.ResultItemSlotController;
            _resultItemSlotWidget.Setup(craftResultSlotController);
        }

        private void RefreshCraftButton()
        {
            var craftResultSlotController = _craftService.InventoryService.ResultItemSlotController;
            _buttonCraft.interactable = !craftResultSlotController.IsEmpty;
        }

        private void OnButtonCraftClick()
        { 
        }

        private async Task OnCraftResultItemChangedEvent(CraftResultItemChangedEvent evnt)
        {
            RefreshResultItemSlotWidget();
            RefreshCraftButton();
        }
    }
}