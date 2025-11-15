using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Services.Tooltip
{
    public interface ITooltipInfo
    { 
        IInventorySlotController InventorySlotController { get; }
        Vector3 Position { get; }

        void Setup(IInventorySlotController inventorySlotController, Vector3 position);
    }

    public class TooltipInfo : ITooltipInfo
    {
        public IInventorySlotController InventorySlotController => _inventorySlotController;
        public Vector3 Position => _position;

        private IInventorySlotController _inventorySlotController;
        private Vector3 _position;

        public TooltipInfo()
        {
            _inventorySlotController = null;
            _position = Vector3.zero;
        }

        public void Setup(IInventorySlotController inventorySlotController, Vector3 position)
        {
            _inventorySlotController = inventorySlotController;
            _position = position;
        }

        public class Pool : MemoryPool<TooltipInfo> { }
    }
}