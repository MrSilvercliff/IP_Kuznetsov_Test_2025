using _Project.Scripts.GameScene.Services.Player;
using _Project.Scripts.GameScene.UI.Widgets.Inventory;
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

        [Inject] private IPlayerService _playerService;

        protected override async Task<bool> OnInit()
        {
            await base.OnInit();
            _inventoryWidget.Init();
            return true;
        }

        protected override Task<bool> OnSetup(IWindowSetup setup)
        {
            _inventoryWidget.Setup(_playerService.Inventory.InventoryController);
            return Task.FromResult(true);
        }
    }
}