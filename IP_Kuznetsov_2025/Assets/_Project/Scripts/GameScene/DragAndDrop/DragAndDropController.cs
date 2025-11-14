using _Project.Scripts.GameScene.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.DragAndDrop
{
    public interface IDragAndDropController : IProjectService, IInventorySlotDragAndDropListener
    {
        void Subscribe(IInventorySlotDragAndDropListener inventorySlotDragAndDropListener);
        void UnSubscribe(IInventorySlotDragAndDropListener inventorySlotDragAndDropListener);
    }

    public class DragAndDropController : IDragAndDropController
    {
        private List<IInventorySlotDragAndDropListener> _inventorySlotDragAndDropListeners;

        public DragAndDropController()
        {
            _inventorySlotDragAndDropListeners = new();
        }

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        #region Listeners_Subscribes

        public void Subscribe(IInventorySlotDragAndDropListener inventorySlotDragAndDropListener)
        {
            if (_inventorySlotDragAndDropListeners.Contains(inventorySlotDragAndDropListener))
                return;

            _inventorySlotDragAndDropListeners.Add(inventorySlotDragAndDropListener);
        }

        public void UnSubscribe(IInventorySlotDragAndDropListener inventorySlotDragAndDropListener)
        {
            _inventorySlotDragAndDropListeners.Remove(inventorySlotDragAndDropListener);
        }

        #endregion Listeners_Subscribes

        #region Inventory_Slot_DragAndDrop_Listener

        public void OnDrag(IInventorySlotController inventorySlotController)
        {
            for (int i = 0; i < _inventorySlotDragAndDropListeners.Count; i++) 
                _inventorySlotDragAndDropListeners[i].OnDrag(inventorySlotController);
        }

        public void OnDrop(IInventorySlotController inventorySlotController)
        {
            for (int i = 0; i < _inventorySlotDragAndDropListeners.Count; i++)
                _inventorySlotDragAndDropListeners[i].OnDrop(inventorySlotController);
        }

        #endregion Inventory_Slot_DragAndDrop_Listener
    }
}