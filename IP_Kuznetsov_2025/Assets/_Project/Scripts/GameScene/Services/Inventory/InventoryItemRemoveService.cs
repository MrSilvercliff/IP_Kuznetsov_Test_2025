using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventoryItemRemoveService : IProjectService
    {
        void ClearInventory(IInventoryController inventoryController);
    }

    public class InventoryItemRemoveService : IInventoryItemRemoveService
    {
        [Inject] private IGameSceneObjectPoolService _objectPoolService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void ClearInventory(IInventoryController inventoryController)
        {
            var slotControllers = inventoryController.SlotControllers;

            for (int i = 0; i < slotControllers.Count; i++)
            {
                var slotController = slotControllers[i];
                ClearInventorySlot(slotController);
            }
        }

        private bool ClearInventorySlot(IInventorySlotController targetInventorySlotController)
        {
            if (targetInventorySlotController.IsEmpty)
                return true;

            var gameItemPool = _objectPoolService.GameItemPool;
            var item = (GameItem)targetInventorySlotController.Item;
            gameItemPool.Despawn(item);
            targetInventorySlotController.SetItem(null);
            return true;
        }
    }
}