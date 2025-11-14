using _Project.Scripts.GameScene.Inventory;
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
        void ClearInventory(IInventoryController inventoryController);
        bool ClearInventorySlot(IInventorySlotController inventorySlotController);
        void MoveItemBetweenSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController);
    }

    public class InventoryService : IInventoryService
    {
        [Inject] private IInventoryItemAddService _itemAddService;
        [Inject] private IInventoryItemRemoveService _itemRemoveService;
        [Inject] private IInventoryItemMoveService _itemMoveService;
        [Inject] private IInventorySlotService _inventorySlotService;

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
    }
}