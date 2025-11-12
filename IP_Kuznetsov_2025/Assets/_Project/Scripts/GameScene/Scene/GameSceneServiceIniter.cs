using _Project.Scripts.GameScene.Services.Inventory;
using _Project.Scripts.GameScene.Services.Player;
using _Project.Scripts.Project.Services.ServiceInit;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Panels;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Popups;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Views;

namespace _Project.Scripts.GameScene.Scene
{
    public interface IGameSceneServiceIniter : IServiceIniter
    { 
    }

    public class GameSceneServiceIniter : ServiceIniter, IGameSceneServiceIniter
    {
        #region First

        // windows
        [Inject] private IViewController _viewController;
        [Inject] private IPopupController _popupController;
        [Inject] private IPanelSettingsRepository _panelSettingsRepository;
        [Inject] private IPanelController _panelController;

        #endregion First



        #region Second

        [Inject] private IPlayerService _playerService;
        [Inject] private IInventoryService _inventoryService;

        #endregion Second

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> InitServices(int stage)
        {
            var result = true;

            switch (stage)
            {
                case 1:
                    result = await InitServicesFirst();
                    break;

                case 2:
                    result = await InitServicesSecond();
                    break;
            }

            return result;
        }

        private async Task<bool> InitServicesFirst()
        {
            AddService(_panelSettingsRepository);
            AddService(_panelController);
            AddService(_viewController);
            AddService(_popupController);

            var result = await InitServices();
            return result;
        }

        private async Task<bool> InitServicesSecond()
        {
            AddService(_playerService);
            AddService(_inventoryService);

            var result = await InitServices();
            return result;
        }
    }
}