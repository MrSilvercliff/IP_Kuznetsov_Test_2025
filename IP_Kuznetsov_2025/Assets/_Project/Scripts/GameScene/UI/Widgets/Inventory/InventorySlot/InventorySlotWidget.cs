using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.UI.Events;
using _Project.Scripts.Project.Monobeh;
using _Project.Scripts.Project.ObjectPools;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.EventBus.Async;
using ZerglingUnityPlugins.Tools.Scripts.ObjectPool;
using IPoolable = ZerglingUnityPlugins.Tools.Scripts.ObjectPool.IPoolable;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot
{
    public class InventorySlotWidget : ProjectMonoBehaviour, IPoolable
    {
        [Header("INVENTORY SLOT WIDGET")]
        [SerializeField] protected InventorySlotWidgetView _view;

        [Inject] private IEventBusAsync _eventBus;

        protected IInventorySlotController _inventorySlotController;

        private void OnEnable()
        {
            _eventBus.Subscribe<InventorySlotChangedEvent>(OnInventorySlotChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus.UnSubscribe<InventorySlotChangedEvent>(OnInventorySlotChangedEvent);
        }

        public void Setup(IInventorySlotController inventorySlotController)
        { 
            _inventorySlotController = inventorySlotController;
            _view.Refresh(inventorySlotController);
        }

        public virtual void OnCreated()
        {
            RefreshInstanceId();
            _inventorySlotController = null;
        }

        public void OnSpawned()
        {
        }

        public virtual void OnDespawned()
        {
            _inventorySlotController = null;
        }

        private async Task OnInventorySlotChangedEvent(InventorySlotChangedEvent evnt)
        {
            if (_inventorySlotController == null)
                return;

            if (_inventorySlotController != evnt.InventorySlotController)
                return;

            _view.Refresh(_inventorySlotController);
        }

        public class Pool : ProjectMonoMemoryPool<InventorySlotWidget> { }
    }
}