using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Inventory
{
    public interface IInventoryController
    { 
        IReadOnlyList<IInventorySlotController> SlotControllers { get; }

        void Setup(List<IInventorySlotController> slotControllers);
    }

    public class InventoryController : IInventoryController
    {
        public IReadOnlyList<IInventorySlotController> SlotControllers => _slotControllers;

        private List<IInventorySlotController> _slotControllers;

        public InventoryController()
        {
            _slotControllers = null;
        }

        public void Setup(List<IInventorySlotController> slotControllers)
        {
            _slotControllers = slotControllers;
        }

        public class Pool : MemoryPool<InventoryController> { }
    }
}