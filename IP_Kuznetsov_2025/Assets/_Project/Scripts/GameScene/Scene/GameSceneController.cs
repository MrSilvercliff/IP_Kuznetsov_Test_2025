using _Project.Scripts.GameScene.Services.Inventory;
using _Project.Scripts.GameScene.Services.Player;
using _Project.Scripts.GameScene.UI.Views.PlayerInventory;
using _Project.Scripts.Project.Scenes;
using _Project.Scripts.Project.Services.ServiceInit;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Views;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneController : SceneController
    {
        [Inject] private IProjectServiceIniter _projectServiceIniter;
        [Inject] private IGameSceneServiceIniter _gameSceneServiceIniter;

        [Inject] private IViewController _viewController;

        [Inject] private IPlayerService _playerService;
        [Inject] private IInventoryService _inventoryService;

        protected override async Task OnAwake()
        {
            await _projectServiceIniter.Init();
            await _gameSceneServiceIniter.Init();
        }

        protected override async Task OnStart()
        {
            await _projectServiceIniter.InitServices(0);
            await _gameSceneServiceIniter.InitServices(1);
            await _gameSceneServiceIniter.InitServices(2);

            // fill player inventory with random items
            var playerInventory = _playerService.Inventory.InventoryController;
            _inventoryService.FillRandom(playerInventory);

            await _viewController.OpenView<PlayerInventoryView>();
        }

        protected override Task OnLateStart()
        {
            return Task.CompletedTask;
        }

        protected override void OnFlush()
        {
        }
    }
}