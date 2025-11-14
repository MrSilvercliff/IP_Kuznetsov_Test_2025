using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.UI.Events;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.EventBus.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventoryItemMoveService : IProjectService
    {
        void MoveItemBetweenSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController);
    }

    public class InventoryItemMoveService : IInventoryItemMoveService
    {
        [Inject] private IEventBusAsync _eventBus;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void MoveItemBetweenSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        {
            if (toInventorySlotController.IsEmpty)
            {
                MoveItemToEmptySlot(fromInventorySlotController, toInventorySlotController);
                return;
            }

            // toInventorySlot not empty
            var fromSlotItem = fromInventorySlotController.Item;
            var toSlotItem = toInventorySlotController.Item;

            if (fromSlotItem.Id == toSlotItem.Id)
                MergeItemsInSlot(fromInventorySlotController, toInventorySlotController);
            else
                SwapItemsInSlots(fromInventorySlotController, toInventorySlotController);
        }

        private void MoveItemToEmptySlot(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        {
            var fromSlotItem = fromInventorySlotController.Item;
            fromInventorySlotController.SetItem(null);
            toInventorySlotController.SetItem(fromSlotItem);

            var evntFrom = new InventorySlotChangedEvent(fromInventorySlotController);
            _eventBus.Fire(evntFrom);

            var evntTo = new InventorySlotChangedEvent(toInventorySlotController);
            _eventBus.Fire(evntTo);
        }

        private void MergeItemsInSlot(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        { 
        }

        private void SwapItemsInSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        { 
        }
    }
}