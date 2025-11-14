using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.Project.Services.Balance;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Inventory
{
    public class InventoryControllerPool : MemoryPool<InventoryController>
    {
        [Inject] private IProjectBalanceService _balanceService;
        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;

        public InventoryController SpawnDefaultInventoryController()
        {
            var result = Spawn();
            var slotsCount = _balanceService.InventoryConfig.SlotsCount;
            FillInventoryControllerSlots(result, slotsCount);
            return result;
        }

        public InventoryController SpawnWorkbenchInventoryController()
        {
            var result = Spawn();
            var slotsCount = _balanceService.WorkbenchConfig.WorkbenchInventorySlotCount;
            FillInventoryControllerSlots(result, slotsCount);
            return result;
        }

        protected override void OnDespawned(InventoryController item)
        {
            FlushInventoryControllerSlots(item);
        }

        private void FillInventoryControllerSlots(IInventoryController inventoryController, int slotsCount)
        { 
            var slotControllerPool = _gameSceneObjectPoolService.InventorySlotControllerPool;

            var slotControllersList = (List<IInventorySlotController>)inventoryController.SlotControllers;

            for (int i = 0; i < slotsCount; i++)
            {
                var slotController = slotControllerPool.Spawn();
                slotControllersList.Add(slotController);
            }
        }

        private void FlushInventoryControllerSlots(IInventoryController inventoryController)
        {
            var slotControllerPool = _gameSceneObjectPoolService.InventorySlotControllerPool;

            var slotControllersList = (List<IInventorySlotController>)inventoryController.SlotControllers;

            for (int i = 0; i < slotControllersList.Count; i++)
            { 
                var slotController = (InventorySlotController)slotControllersList[i];
                slotControllerPool.Despawn(slotController);
            }

            slotControllersList.Clear();
        }
    }
}