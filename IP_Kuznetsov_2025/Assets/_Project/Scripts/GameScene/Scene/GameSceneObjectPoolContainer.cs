using _Project.Scripts.Project.ObjectPools;
using UnityEngine;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneObjectPoolContainer : MonoBehaviour
    {
        public ObjectPoolContainerItem InventorySlotWidget => _inventorySlotWidget;

        [SerializeField] private ObjectPoolContainerItem _inventorySlotWidget;
    }
}