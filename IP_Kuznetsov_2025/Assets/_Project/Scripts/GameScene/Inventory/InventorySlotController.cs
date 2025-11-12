using _Project.Scripts.GameScene.GameItems;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Inventory
{
    public interface IInventorySlotController
    { 
        bool IsEmpty { get; }
        IGameItem Item { get; }

        void SetItem(IGameItem gameItem);
    }

    public class InventorySlotController : IInventorySlotController
    {
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
        }

        public class Pool : MemoryPool<InventorySlotController> { }
    }
}