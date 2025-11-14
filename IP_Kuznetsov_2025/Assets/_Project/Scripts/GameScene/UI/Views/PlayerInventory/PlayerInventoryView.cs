using _Project.Scripts.GameScene.Services.Player;
using _Project.Scripts.GameScene.UI.Widgets.Inventory;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Basics;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Views;

namespace _Project.Scripts.GameScene.UI.Views.PlayerInventory
{
    public class PlayerInventoryView : ViewWindow
    {
        [SerializeField] private InventoryWidget _inventoryWidget;
        [SerializeField] private PlayerInventoryViewTestButtonsWidget _testButtonsWidget;
        [SerializeField] private InventorySlotDragAndDropWidget _dragAndDropWidget;

        [Inject] private IPlayerService _playerService;

        protected override async Task<bool> OnInit()
        {
            await base.OnInit();
            _inventoryWidget.Init();
            _dragAndDropWidget.Init();
            return true;
        }

        protected override Task<bool> OnSetup(IWindowSetup setup)
        {
            var playerInventoryController = _playerService.Inventory.InventoryController;
            _inventoryWidget.Setup(playerInventoryController);
            _testButtonsWidget.Setup(playerInventoryController);
            return Task.FromResult(true);
        }
    }
}