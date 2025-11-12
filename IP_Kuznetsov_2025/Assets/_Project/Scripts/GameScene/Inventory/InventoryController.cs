using _Project.Scripts.GameScene.Services.ObjectPools;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Inventory
{
    public interface IInventoryController
    { 
        IReadOnlyList<IInventorySlotController> SlotControllers { get; }
    }

    public class InventoryController : IInventoryController
    {
        public IReadOnlyList<IInventorySlotController> SlotControllers => _slotControllers;

        private List<IInventorySlotController> _slotControllers;

        public InventoryController()
        {
            _slotControllers = new();
        }
    }
}