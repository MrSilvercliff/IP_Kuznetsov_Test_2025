using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
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
        [Inject] private IGameSceneObjectPoolService _objectPoolService;

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
            if (fromInventorySlotController == toInventorySlotController)
                return;

            if (toInventorySlotController.IsEmpty)
            {
                MoveItemToEmptySlot(fromInventorySlotController, toInventorySlotController);
            }
            else
            {
                var fromSlotItem = fromInventorySlotController.Item;
                var toSlotItem = toInventorySlotController.Item;

                if (fromSlotItem.Id == toSlotItem.Id && fromSlotItem.IsStackable)
                    MergeItemsInSlot(fromInventorySlotController, toInventorySlotController);
                else
                    SwapItemsInSlots(fromInventorySlotController, toInventorySlotController);
            }

            var evntFrom = new InventorySlotChangedEvent(fromInventorySlotController);
            _eventBus.Fire(evntFrom);

            var evntTo = new InventorySlotChangedEvent(toInventorySlotController);
            _eventBus.Fire(evntTo);
        }

        private void MoveItemToEmptySlot(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        {
            var fromSlotItem = fromInventorySlotController.Item;
            fromInventorySlotController.SetItem(null);
            toInventorySlotController.SetItem(fromSlotItem);
        }

        private void MergeItemsInSlot(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        {
            var fromSlotItem = fromInventorySlotController.Item;
            var fromSlotItemCount = fromSlotItem.Count;

            var toSlotItem = toInventorySlotController.Item;
            var toSlotItemCount = toSlotItem.Count;

            var stackSize = toSlotItem.StackSize;
            var itemsToMove = stackSize - toSlotItemCount;

            if (itemsToMove >= fromSlotItemCount)
            {
                // merging stacks with emptying "from slot"

                fromInventorySlotController.SetItem(null);
                toSlotItemCount += fromSlotItemCount;
                toSlotItem.SetCount(toSlotItemCount);
                ReturnGameItemToPool(fromSlotItem);
                return;
            }

            // merging stacks without emptying "from slot"
            toSlotItem.SetCount(stackSize);
            fromSlotItemCount -= itemsToMove;
            fromSlotItem.SetCount(fromSlotItemCount);
        }

        private void SwapItemsInSlots(IInventorySlotController fromInventorySlotController, IInventorySlotController toInventorySlotController)
        {
            var fromSlotItem = fromInventorySlotController.Item;
            var toSlotItem = toInventorySlotController.Item;

            fromInventorySlotController.SetItem(toSlotItem);
            toInventorySlotController.SetItem(fromSlotItem);
        }

        private void ReturnGameItemToPool(IGameItem gameItem)
        {
            var gameItemPool = _objectPoolService.GameItemPool;
            gameItemPool.Despawn((GameItem)gameItem);
        }
    }
}