using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Inventory;
using _Project.Scripts.GameScene.Services.Player;
using _Project.Scripts.GameScene.UI.Events;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.EventBus.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftProcessService : IProjectService
    {
        void CraftItem();
    }

    public class CraftProcessService : ICraftProcessService
    {
        [Inject] private IEventBusAsync _eventBusAsync;
        [Inject] private IProjectBalanceService _projectBalanceService;
        [Inject] private ICraftInventoryService _craftInventoryService;
        [Inject] private IPlayerService _playerService;
        [Inject] private IInventoryService _inventoryService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void CraftItem()
        {
            var checkResult = CheckPlayerInventory();

            if (!checkResult)
            {
                LogUtils.Error(this, $"CANT CRAFT! THERE ARE NO EMPTY SLOTS IN PLAYER INVENTORY!");
                return;
            }    

            var craftRecipeBalanceModel = _craftInventoryService.ActualCraftRecipeBalanceModel;
            SpendCraftRecipeItems(craftRecipeBalanceModel);
            AddCraftRecipeResultItem(craftRecipeBalanceModel);

            var evnt = new CraftSuccessEvent();
            _eventBusAsync.Fire(evnt);
        }

        private bool CheckPlayerInventory()
        {
            var playerInventory = _playerService.Inventory.InventoryController;
            var result = _inventoryService.HasEmptyInventorySlot(playerInventory, out var emptyInventorySlotController);
            return result;
        }

        private void SpendCraftRecipeItems(ICraftRecipeBalanceModel craftRecipeBalanceModel)
        {
            var craftInventorySlots = _craftInventoryService.InventoryController.SlotControllers;
            var recipeItemCounts = craftRecipeBalanceModel.CraftRecipeItemsCount;

            for (int i = 0; i < craftInventorySlots.Count; i++)
            {
                var slot = craftInventorySlots[i];
                var recipeItemCount = recipeItemCounts[i];

                if (slot.IsEmpty)
                    continue;

                SpendCraftRecipeItem(slot, recipeItemCount);
            }
        }

        private void SpendCraftRecipeItem(IInventorySlotController craftInventorySlotController, int count)
        {
            var slotItem = craftInventorySlotController.Item;
            var slotItemCount = slotItem.Count;

            if (slotItemCount == count)
            {
                _inventoryService.ClearInventorySlot(craftInventorySlotController);
                return;
            }

            var newItemCount = slotItemCount - count;
            slotItem.SetCount(newItemCount);
        }

        private void AddCraftRecipeResultItem(ICraftRecipeBalanceModel craftRecipeBalanceModel)
        {
            var gameItemsBalanceStorage = _projectBalanceService.GameItems;
            var itemId = craftRecipeBalanceModel.ResultGameItemId;
            var itemCount = craftRecipeBalanceModel.ResultGameItemCount;

            var tryResult = gameItemsBalanceStorage.TryGetById(itemId, out var gameItemBalanceModel);

            if (!tryResult)
            {
                LogUtils.Error(this, $"CANT CRAFT! RESULT GAME ITEM BALANCE MODEL DOES NOT EXIST!");
                return;
            }

            var playerInventory = _playerService.Inventory.InventoryController;
            _inventoryService.TryAddItemToEmptySlot(playerInventory, gameItemBalanceModel, itemCount);
        }
    }
}