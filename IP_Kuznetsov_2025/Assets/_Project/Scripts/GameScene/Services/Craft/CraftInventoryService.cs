using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftInventoryService : IProjectService
    { 
        public IInventoryController InventoryController { get; }
        public IInventorySlotController ResultItemSlotController { get; }
    }

    public class CraftInventoryService : ICraftInventoryService
    {
        public IInventoryController InventoryController => _inventoryController;
        public IInventorySlotController ResultItemSlotController => _resultItemSlotController;

        [Inject] private IGameSceneObjectPoolService _objectPoolService;

        private IInventoryController _inventoryController;
        private IInventorySlotController _resultItemSlotController;

        public Task<bool> Init()
        {
            var inventoryControllerPool = _objectPoolService.InventoryControllerPool;
            _inventoryController = inventoryControllerPool.SpawnWorkbenchInventoryController();

            var inventorySlotControllerPool = _objectPoolService.InventorySlotControllerPool;
            _resultItemSlotController = inventorySlotControllerPool.Spawn();

            return Task.FromResult(true);
        }

        public bool Flush()
        {
            var inventoryControllerPool = _objectPoolService.InventoryControllerPool;
            inventoryControllerPool.Despawn((InventoryController)_inventoryController);

            var inventorySlotControllerPool = _objectPoolService.InventorySlotControllerPool;
            inventorySlotControllerPool.Despawn((InventorySlotController)_resultItemSlotController);

            return true;
        }
    }
}