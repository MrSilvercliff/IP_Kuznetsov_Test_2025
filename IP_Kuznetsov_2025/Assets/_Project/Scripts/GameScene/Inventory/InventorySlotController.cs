using _Project.Scripts.GameScene.GameItems;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Inventory
{
    public interface IInventorySlotController
    {
        event Action<IInventorySlotController> ItemChangedEvent;

        bool IsEmpty { get; }
        IGameItem Item { get; }

        void SetItem(IGameItem gameItem);
    }

    public class InventorySlotController : IInventorySlotController
    {
        public event Action<IInventorySlotController> ItemChangedEvent;

        public bool IsEmpty => _gameItem == null;
        public IGameItem Item => _gameItem;

        private IGameItem _gameItem;

        public InventorySlotController()
        { 
            _gameItem = null;
        }

        public void SetItem(IGameItem gameItem)
        {
            _gameItem = gameItem;
            ItemChangedEvent?.Invoke(this);
        }

        public class Pool : MemoryPool<InventorySlotController> { }
    }
}