using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.Project.Services.Balance.Models;
using Codice.Client.Common.WebApi.Responses;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventoryService : IProjectService
    {
        void FillRandom(IInventoryController targetInventory);
        bool TryAddItemToEmptySlot(IInventoryController targetInventory, IGameItemBalanceModel gameItemBalanceModel, int count = 1);

        void ClearInventory(IInventoryController inventoryController);
        bool ClearInventorySlot(IInventorySlotController inventorySlotController);

        void MoveItemBetweenSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController);

        bool HasEmptyInventorySlot(IInventoryController inventoryController, out IInventorySlotController emptyInventorySlotController);
    }

    public class InventoryService : IInventoryService
    {
        [Inject] private IInventoryItemAddService _itemAddService;
        [Inject] private IInventoryItemRemoveService _itemRemoveService;
        [Inject] private IInventoryItemMoveService _itemMoveService;
        [Inject] private IInventorySlotService _slotService;

        public async Task<bool> Init()
        {
            return true;
        }

        public bool Flush()
        {
            return true;
        }

        public void FillRandom(IInventoryController targetInventory)
        {
            _itemAddService.FillRandom(targetInventory);
        }

        public bool TryAddItemToEmptySlot(IInventoryController targetInventory, IGameItemBalanceModel gameItemBalanceModel, int count = 1)
        {
            var result = _itemAddService.TryAddItemToEmptySlot(targetInventory, gameItemBalanceModel, count);
            return result;
        }

        public void ClearInventory(IInventoryController inventoryController)
        {
            _itemRemoveService.ClearInventory(inventoryController);
        }

        public bool ClearInventorySlot(IInventorySlotController inventorySlotController)
        {
            var result = _itemRemoveService.ClearInventorySlot(inventorySlotController);
            return result;
        }

        public void MoveItemBetweenSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        {
            _itemMoveService.MoveItemBetweenSlots(fromInventorySlotController, toInventorySlotController);
        }

        public bool HasEmptyInventorySlot(IInventoryController inventoryController, out IInventorySlotController emptyInventorySlotController)
        {
            var result = _slotService.HasEmptyInventorySlot(inventoryController, out emptyInventorySlotController);
            return result;
        }
    }
}