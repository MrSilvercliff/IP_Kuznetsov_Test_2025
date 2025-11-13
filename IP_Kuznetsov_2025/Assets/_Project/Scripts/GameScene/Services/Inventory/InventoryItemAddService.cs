using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventoryItemAddService : IProjectService
    {
        bool TryAddItemToEmptySlot(IInventoryController targetInventory, IGameItemBalanceModel gameItemBalanceModel, int count = 1);
        bool TryAddItem(IInventoryController targetInventory, IGameItemBalanceModel gameItemBalanceModel, int count = 1);
        void FillRandom(IInventoryController targetInventory);
    }

    public class InventoryItemAddService : IInventoryItemAddService
    {
        [Inject] private IProjectBalanceService _balanceService;
        [Inject] private IGameSceneObjectPoolService _objectPoolService;
        [Inject] private IInventorySlotService _inventorySlotService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public bool TryAddItemToEmptySlot(IInventoryController targetInventory, IGameItemBalanceModel gameItemBalanceModel, int count = 1)
        {
            var hasEmptySlot = _inventorySlotService.HasEmptyInventorySlot(targetInventory, out var emptyInventorySlotController);

            if (!hasEmptySlot)
                return false;

            SetNewGameItemToSlot(emptyInventorySlotController, gameItemBalanceModel, count);
            return true;
        }

        public bool TryAddItem(IInventoryController targetInventory, IGameItemBalanceModel gameItemBalanceModel, int count = 1)
        {
            throw new System.NotImplementedException();
        }

        public void FillRandom(IInventoryController targetInventory)
        {
            var allGameItemsList = _balanceService.GameItems.GetAllAsList();
            var allGameItemsCount = allGameItemsList.Count;
            var randomGameItemIndex = 0;
            var slotControllers = targetInventory.SlotControllers;
            IGameItemBalanceModel gameItemBalanceModel = null;

            var gameItemPool = _objectPoolService.GameItemPool;

            for (int i = 0; i < slotControllers.Count; i++)
            {
                var slotController = slotControllers[i];

                randomGameItemIndex = Random.Range(0, allGameItemsCount);
                gameItemBalanceModel = allGameItemsList[randomGameItemIndex];
                var count = 1;

                if (gameItemBalanceModel.IsStackable)
                    count = Random.Range(1, gameItemBalanceModel.StackSize + 1); // +1 for include max stack size

                SetNewGameItemToSlot(slotController, gameItemBalanceModel, count);
            }
        }

        private void SetNewGameItemToSlot(IInventorySlotController slotController, IGameItemBalanceModel gameItemBalanceModel, int count = 1)
        {
            var gameItemPool = _objectPoolService.GameItemPool;
            var newGameItem = gameItemPool.Spawn();
            newGameItem.Setup(gameItemBalanceModel);
            newGameItem.SetCount(count);
            slotController.SetItem(newGameItem);
        }
    }
}