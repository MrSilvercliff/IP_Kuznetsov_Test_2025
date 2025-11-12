using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.Project.Monobeh;
using _Project.Scripts.Project.ObjectPools;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.ObjectPool;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory
{
    public class InventorySlotWidget : ProjectMonoBehaviour, IPoolable
    {
        [Header("INVENTORY SLOT WIDGET")]
        [SerializeField] private InventorySlotWidgetView _view; 

        private IInventorySlotController _inventorySlotController;

        public void Setup(IInventorySlotController inventorySlotController)
        { 
            _inventorySlotController = inventorySlotController;
            _view.Refresh(inventorySlotController);
        }

        public void OnCreated()
        {
            RefreshInstanceId();
            _inventorySlotController = null;
        }

        public void OnSpawned()
        {
        }

        public void OnDespawned()
        {
            _inventorySlotController = null;
        }

        public class Pool : ProjectMonoMemoryPool<InventorySlotWidget> { }
    }
}