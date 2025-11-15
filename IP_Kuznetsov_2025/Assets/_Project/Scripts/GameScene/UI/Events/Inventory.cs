using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.EventBus;

namespace _Project.Scripts.GameScene.UI.Events
{
    public class InventorySlotChangedEvent : IEvent
    {
        public IInventorySlotController InventorySlotController { get; }

        public InventorySlotChangedEvent(IInventorySlotController inventorySlotController)
        {
            InventorySlotController = inventorySlotController;
        }
    }
}