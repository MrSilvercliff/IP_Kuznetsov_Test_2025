using _Project.Scripts.GameScene.Services.ObjectPools;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Inventory
{
    public interface IInventoryController
    {
        event Action<IInventoryController, IInventorySlotController> SlotItemChangedEvent;

        IReadOnlyList<IInventorySlotController> SlotControllers { get; }
    }

    public class InventoryController : IInventoryController
    {
        public event Action<IInventoryController, IInventorySlotController> SlotItemChangedEvent;

        public IReadOnlyList<IInventorySlotController> SlotControllers => _slotControllers;

        private List<IInventorySlotController> _slotControllers;

        public InventoryController()
        {
        }

        public void Setup(List<IInventorySlotController> list)
        {
            _slotControllers = list;

            for (int i = 0; i < _slotControllers.Count; i++)
                _slotControllers[i].ItemChangedEvent += OnSlotItemChangedEvent;
        }

        public void Flush()
        {
            for (int i = 0; i < _slotControllers.Count; i++)
                _slotControllers[i].ItemChangedEvent -= OnSlotItemChangedEvent;

            _slotControllers.Clear();
        }

        private void OnSlotItemChangedEvent(IInventorySlotController slotController)
        {
            SlotItemChangedEvent?.Invoke(this, slotController);
        }
    }
}