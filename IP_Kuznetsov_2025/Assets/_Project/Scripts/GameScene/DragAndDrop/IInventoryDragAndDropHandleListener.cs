using UnityEngine;

namespace _Project.Scripts.GameScene.DragAndDrop
{
    public interface IInventoryDragAndDropHandleListener
    {
        void OnDragStart();
        void OnDragEnd();
    }
}