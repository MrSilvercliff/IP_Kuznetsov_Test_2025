using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Player
{
    public interface IPlayerInventoryService : IProjectService
    { 
        IInventoryController InventoryController { get; }
    }

    public class PlayerInventoryService : IPlayerInventoryService
    {
        public IInventoryController InventoryController => _inventoryController;

        [Inject] private IGameSceneObjectPoolService _objectPoolService;

        private IInventoryController _inventoryController;

        public Task<bool> Init()
        {
            var inventoryControllerPool = _objectPoolService.InventoryControllerPool;
            _inventoryController = inventoryControllerPool.SpawnDefaultInventoryController();
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            var inventoryControllerPool = _objectPoolService.InventoryControllerPool;
            inventoryControllerPool.Despawn((InventoryController)_inventoryController);
            return true;
        }
    }
}