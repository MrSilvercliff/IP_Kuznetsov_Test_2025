using _Project.Scripts.GameScene.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.DragAndDrop
{
    public interface IDragAndDropController : IProjectService, IInventoryDragAndDropListener
    {
        bool DragInProcess { get; }

        void Subscribe(IInventoryDragAndDropListener inventorySlotDragAndDropListener);
        void UnSubscribe(IInventoryDragAndDropListener inventorySlotDragAndDropListener);

        void Subscribe(IInventoryDragAndDropHandleListener inventorySlotDragAndDropHandleListener);
        void UnSubscribe(IInventoryDragAndDropHandleListener inventorySlotDragAndDropHandleListener);
    }

    public class DragAndDropController : IDragAndDropController
    {
        public bool DragInProcess { get; private set; }

        private List<IInventoryDragAndDropListener> _inventorySlotDragAndDropListeners;
        private List<IInventoryDragAndDropHandleListener> _inventorySlotDragAndHandleDropListeners;

        public DragAndDropController()
        {
            DragInProcess = false;
            _inventorySlotDragAndDropListeners = new();
            _inventorySlotDragAndHandleDropListeners = new();
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

        public void Subscribe(IInventoryDragAndDropListener inventorySlotDragAndDropListener)
        {
            if (_inventorySlotDragAndDropListeners.Contains(inventorySlotDragAndDropListener))
                return;

            _inventorySlotDragAndDropListeners.Add(inventorySlotDragAndDropListener);
        }

        public void UnSubscribe(IInventoryDragAndDropListener inventorySlotDragAndDropListener)
        {
            _inventorySlotDragAndDropListeners.Remove(inventorySlotDragAndDropListener);
        }

        public void Subscribe(IInventoryDragAndDropHandleListener inventorySlotDragAndDropHandleListener)
        {
            if (_inventorySlotDragAndHandleDropListeners.Contains(inventorySlotDragAndDropHandleListener))
                return;

            _inventorySlotDragAndHandleDropListeners.Add(inventorySlotDragAndDropHandleListener);
        }

        public void UnSubscribe(IInventoryDragAndDropHandleListener inventorySlotDragAndDropHandleListener)
        {
            _inventorySlotDragAndHandleDropListeners.Remove(inventorySlotDragAndDropHandleListener);
        }

        #endregion Listeners_Subscribes



        #region Inventory_Slot_DragAndDrop_Listener

        public void OnDrag(IInventorySlotController inventorySlotController)
        {
            if (DragInProcess)
                return;

            DragInProcess = true;

            for (int i = 0; i < _inventorySlotDragAndHandleDropListeners.Count; i++)
                _inventorySlotDragAndHandleDropListeners[i].OnDragStart();

            for (int i = 0; i < _inventorySlotDragAndDropListeners.Count; i++) 
                _inventorySlotDragAndDropListeners[i].OnDrag(inventorySlotController);
        }

        public void OnDrop(IInventorySlotController inventorySlotController)
        {
            if (!DragInProcess)
                return;

            for (int i = 0; i < _inventorySlotDragAndDropListeners.Count; i++)
                _inventorySlotDragAndDropListeners[i].OnDrop(inventorySlotController);

            for (int i = 0; i < _inventorySlotDragAndHandleDropListeners.Count; i++)
                _inventorySlotDragAndHandleDropListeners[i].OnDragEnd();

            DragInProcess = false;
        }

        public void OnDrop(bool isSafe)
        {
            if (!DragInProcess)
                return;

            for (int i = 0; i < _inventorySlotDragAndDropListeners.Count; i++)
                _inventorySlotDragAndDropListeners[i].OnDrop(isSafe);

            for (int i = 0; i < _inventorySlotDragAndHandleDropListeners.Count; i++)
                _inventorySlotDragAndHandleDropListeners[i].OnDragEnd();

            DragInProcess = false;
        }

        #endregion Inventory_Slot_DragAndDrop_Listener
    }
}