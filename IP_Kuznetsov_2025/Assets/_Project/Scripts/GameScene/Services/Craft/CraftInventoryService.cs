using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftInventoryService : IProjectService
    { 
        IInventoryController InventoryController { get; }
        IInventorySlotController ResultItemSlotController { get; }
        ICraftRecipeBalanceModel ActualCraftRecipeBalanceModel { get; }

        void SetActualCraftRecipe(ICraftRecipeBalanceModel craftRecipeBalanceModel);
    }

    public class CraftInventoryService : ICraftInventoryService
    {
        public IInventoryController InventoryController => _inventoryController;
        public IInventorySlotController ResultItemSlotController => _resultItemSlotController;
        public ICraftRecipeBalanceModel ActualCraftRecipeBalanceModel => _actualCraftRecipeBalanceModel;

        [Inject] private IGameSceneObjectPoolService _objectPoolService;

        private IInventoryController _inventoryController;
        private IInventorySlotController _resultItemSlotController;
        private ICraftRecipeBalanceModel _actualCraftRecipeBalanceModel;

        public CraftInventoryService()
        {
            _inventoryController = null;
            _resultItemSlotController = null;
            _actualCraftRecipeBalanceModel = null;
        }

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

        public void SetActualCraftRecipe(ICraftRecipeBalanceModel craftRecipeBalanceModel)
        {
            _actualCraftRecipeBalanceModel = craftRecipeBalanceModel;
        }
    }
}