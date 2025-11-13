using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.Project.Extensions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory
{
    public class InventoryWidget : MonoBehaviour
    {
        [SerializeField] private Transform _inventorySlotWidgetContainer;

        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;

        private IInventoryController _inventoryController;
        private List<InventorySlotWidget> _inventorySlotWidgets;

        public void Init()
        {
            _inventorySlotWidgets = new();
        }

        public void Setup(IInventoryController inventoryController)
        { 
            _inventoryController = inventoryController;
            FlushSlotWidgets();
            SetupSlotWidgets();
        }

        public void Flush()
        {
            FlushSlotWidgets();
        }

        private void SetupSlotWidgets()
        {
            var inventorySlotWidgetPool = _gameSceneObjectPoolService.InventorySlotWidgetPool;
            var slotControllers = _inventoryController.SlotControllers;

            for (int i = 0; i < slotControllers.Count; i++)
            {
                var slotController = slotControllers[i];

                var widget = inventorySlotWidgetPool.Spawn();
                widget.Setup(slotController);
                widget.transform.SetParent(_inventorySlotWidgetContainer);
                widget.transform.ResetLocalPosition();
                widget.transform.ResetLocalRotation();
                widget.transform.ResetLocalScale();
                widget.SetActive(true);

                _inventorySlotWidgets.Add(widget);
            }
        }

        private void FlushSlotWidgets()
        {
            var inventorySlotWidgetPool = _gameSceneObjectPoolService.InventorySlotWidgetPool;

            for (int i = 0; i < _inventorySlotWidgets.Count; i++)
            {
                var widget = _inventorySlotWidgets[i];
                inventorySlotWidgetPool.Despawn(widget);
            }

            _inventorySlotWidgets.Clear();
        }
    }
}