using _Project.Scripts.GameScene.Inventory;
using UnityEngine;

namespace _Project.Scripts.GameScene.DragAndDrop
{
    public interface IInventoryDragAndDropListener
    {
        void OnDrag(IInventorySlotController inventorySlotController);
        void OnDrop(IInventorySlotController inventorySlotController);
        void OnDrop(bool isSafe);
    }
}