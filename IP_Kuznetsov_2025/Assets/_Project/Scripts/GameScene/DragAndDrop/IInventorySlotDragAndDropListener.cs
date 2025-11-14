using _Project.Scripts.GameScene.Inventory;
using UnityEngine;

namespace _Project.Scripts.GameScene.DragAndDrop
{
    public interface IInventorySlotDragAndDropListener
    {
        void OnDrag(IInventorySlotController inventorySlotController);
        void OnDrop(IInventorySlotController inventorySlotController);
    }
}