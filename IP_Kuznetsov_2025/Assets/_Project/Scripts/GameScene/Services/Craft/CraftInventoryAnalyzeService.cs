using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.GameScene.UI.Events;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.EventBus.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftInventoryAnalyzeService : IProjectService
    { 
    }

    public class CraftInventoryAnalyzeService : ICraftInventoryAnalyzeService
    {
        [Inject] private IEventBusAsync _eventBus;
        [Inject] private IProjectBalanceService _balanceService;
        [Inject] private IGameSceneObjectPoolService _objectPoolService;
        [Inject] private ICraftInventoryService _inventoryService;

        public Task<bool> Init()
        {
            _inventoryService.InventoryController.SlotItemChangedEvent += OnInventorySlotChangedEvent;
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            _inventoryService.InventoryController.SlotItemChangedEvent -= OnInventorySlotChangedEvent;
            return true;
        }

        private void OnInventorySlotChangedEvent(IInventoryController inventoryController, IInventorySlotController inventorySlotController)
        {
            //Debug.LogError($"OnInventorySlotChangedEvent");
            AnalyzeInventory(inventoryController);
        }

        private void AnalyzeInventory(IInventoryController inventoryController)
        {
            /*
            Debug.LogError($"====================");
            Debug.LogError($"====================");
            Debug.LogError($"====================");
            Debug.LogError($"====================");
            Debug.LogError("AnalyzeInventory 1");
            */

            var craftRecipesByCenterItem = GetCraftRecipesByCenterItem(inventoryController);

            if (craftRecipesByCenterItem == null)
            {
                SetCraftResultItem(null, 0);
                return;
            }

            //Debug.LogError("AnalyzeInventory 2");

            var actualCraftRecipe = FindActualCraftRecipe(inventoryController, craftRecipesByCenterItem);

            if (actualCraftRecipe == null)
            {
                SetCraftResultItem(null, 0);
                return;
            }

            //Debug.LogError("AnalyzeInventory 3");

            _inventoryService.SetActualCraftRecipe(actualCraftRecipe);

            var craftResultGameItemBalanceModel = GetCraftResultItem(actualCraftRecipe);
            var craftResultItemCount = actualCraftRecipe.ResultGameItemCount;
            SetCraftResultItem(craftResultGameItemBalanceModel, craftResultItemCount);

            //Debug.LogError("AnalyzeInventory 4");
        }

        private IReadOnlyList<ICraftRecipeBalanceModel> GetCraftRecipesByCenterItem(IInventoryController craftInventoryController)
        {
            //Debug.LogError($"GetCraftRecipesByCenterItem 1");

            var inventorySlots = craftInventoryController.SlotControllers;
            var centerSlotIndex = Mathf.FloorToInt((float)inventorySlots.Count / 2);
            var centerSlot = inventorySlots[centerSlotIndex];

            //Debug.LogError(centerSlotIndex);

            if (centerSlot.IsEmpty)
                return null;

            //Debug.LogError($"GetCraftRecipesByCenterItem 2");

            var centerItemId = centerSlot.Item.Id;
            var craftRecipeBalanceStorage = _balanceService.CraftRecipes;
            var result = craftRecipeBalanceStorage.GetByCenterGameItemId(centerItemId);
            return result;
        }

        private ICraftRecipeBalanceModel FindActualCraftRecipe(IInventoryController craftInventoryController, IReadOnlyList<ICraftRecipeBalanceModel> craftRecipesList)
        {
            ICraftRecipeBalanceModel result = null;

            for (int i = 0; i < craftRecipesList.Count; i++)
            {
                var craftRecipe = craftRecipesList[i];
                var checkResult = CheckCraftRecipe(craftInventoryController, craftRecipe);

                //Debug.LogError($"====================");
                //Debug.LogError($"craftRecipe.Id = {craftRecipe.Id}");
                //Debug.LogError($"checkResult = {checkResult}");

                if (checkResult)
                {
                    result = craftRecipe;
                    break;
                }
            }

            return result;
        }

        private bool CheckCraftRecipe(IInventoryController craftInventoryController, ICraftRecipeBalanceModel craftRecipeBalanceModel)
        {
            var result = true;

            var inventorySlots = craftInventoryController.SlotControllers;
            var craftRecipeItemsId = craftRecipeBalanceModel.CraftRecipeItemsId;
            var craftReciptItemsCount = craftRecipeBalanceModel.CraftRecipeItemsCount;

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                var inventorySlot = inventorySlots[i];
                var recipeItemId = craftRecipeItemsId[i];
                var recipeItemCount = craftReciptItemsCount[i];

                //DebugPrint(i, inventorySlot, recipeItemId, recipeItemCount);

                if (inventorySlot.IsEmpty && string.IsNullOrEmpty(recipeItemId))
                {
                    //Debug.LogError("CheckCraftRecipe 1");
                    continue;
                }

                if (inventorySlot.IsEmpty && !string.IsNullOrEmpty(recipeItemId))
                {
                    //Debug.LogError("CheckCraftRecipe 2");
                    result = false;
                    break;
                }

                if (!inventorySlot.IsEmpty && string.IsNullOrEmpty(recipeItemId))
                {
                    //Debug.LogError("CheckCraftRecipe 3");
                    result = false;
                    break;
                }

                var inventoryItemId = inventorySlot.Item.Id;
                var inventoryItemCount = inventorySlot.Item.Count;

                if (inventoryItemId != recipeItemId)
                {
                    //Debug.LogError("CheckCraftRecipe 4");
                    result = false;
                    break;
                }

                if (inventoryItemCount < recipeItemCount)
                {
                    //Debug.LogError("CheckCraftRecipe 5");
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void DebugPrint(int index, IInventorySlotController slotController, string craftRecipeItemId, int craftRecipeItemCount)
        {
            Debug.LogError($"--------------------");
            Debug.Log($"INDEX = {index}");

            if (slotController.IsEmpty)
                Debug.LogError($"InventorySlotController IS EMPTY");
            else
            {
                var slotItem = slotController.Item;
                Debug.LogError($"InventorySlotController NOT EMPTY");
                Debug.LogError($"slotItem.Id = [{slotItem.Id}]");
                Debug.LogError($"slotItem.Count = {slotItem.Count}");
            }

            Debug.LogError($"craftRecipeItemId = [{craftRecipeItemId}]");
            Debug.LogError($"craftRecipeItemCount = [{craftRecipeItemCount}]");
        }

        private IGameItemBalanceModel GetCraftResultItem(ICraftRecipeBalanceModel craftRecipeBalanceModel)
        {
            var gameItemBalanceStorage = _balanceService.GameItems;
            var tryResult = gameItemBalanceStorage.TryGetById(craftRecipeBalanceModel.ResultGameItemId, out var gameItemBalanceModel);

            if (!tryResult)
                return null;

            return gameItemBalanceModel;
        }

        private void SetCraftResultItem(IGameItemBalanceModel gameItemBalanceModel, int count)
        {
            if (gameItemBalanceModel == null)
                ReturnCraftResultGameItemToPool();
            else
                SetCraftResultNewGameItem(gameItemBalanceModel, count);

            var evnt = new CraftResultItemChangedEvent();
            _eventBus.Fire(evnt);
        }

        private void ReturnCraftResultGameItemToPool()
        {
            var resultItemSlotController = _inventoryService.ResultItemSlotController;

            if (resultItemSlotController.IsEmpty)
                return;

            var gameItem = (GameItem)resultItemSlotController.Item;
            var gameItemPool = _objectPoolService.GameItemPool;
            gameItemPool.Despawn(gameItem);
            resultItemSlotController.SetItem(null);
        }

        private void SetCraftResultNewGameItem(IGameItemBalanceModel gameItemBalanceModel, int count)
        {
            var resultItemSlotController = _inventoryService.ResultItemSlotController;

            if (!resultItemSlotController.IsEmpty)
            {
                resultItemSlotController.Item.Setup(gameItemBalanceModel);
                resultItemSlotController.Item.SetCount(count);
                return;
            }

            var gameItemPool = _objectPoolService.GameItemPool;
            var newItem = gameItemPool.Spawn();
            newItem.Setup(gameItemBalanceModel);
            newItem.SetCount(count);
            resultItemSlotController.SetItem(newItem);
        }
    }
}