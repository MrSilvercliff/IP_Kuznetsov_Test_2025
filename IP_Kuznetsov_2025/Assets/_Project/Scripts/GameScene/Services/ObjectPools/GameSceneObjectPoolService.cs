using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Tooltip;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Services.ObjectPools
{
    public interface IGameSceneObjectPoolService
    { 
        GameItem.Pool GameItemPool { get; }
        InventoryControllerPool InventoryControllerPool { get; }
        InventorySlotController.Pool InventorySlotControllerPool { get; }
        TooltipInfo.Pool TooltipInfoPool { get; }

        InventorySlotWidget.Pool InventorySlotWidgetPool { get; }
        InventorySlotDraggableWidget.Pool InventorySlotDraggableWidgetPool { get; }
    }

    public class GameSceneObjectPoolService : IGameSceneObjectPoolService
    {
        public GameItem.Pool GameItemPool => _gameItemPool;
        public InventoryControllerPool InventoryControllerPool => _inventoryControllerPool;
        public InventorySlotController.Pool InventorySlotControllerPool => _inventorySlotControllerPool;
        public TooltipInfo.Pool TooltipInfoPool => _tooltipInfoPool;

        public InventorySlotWidget.Pool InventorySlotWidgetPool => _inventorySlotWidgetPool;
        public InventorySlotDraggableWidget.Pool InventorySlotDraggableWidgetPool => _inventorySlotDraggableWidgetPool;

        [Inject] private GameItem.Pool _gameItemPool;
        [Inject] private InventoryControllerPool _inventoryControllerPool;
        [Inject] private InventorySlotController.Pool _inventorySlotControllerPool;
        [Inject] private TooltipInfo.Pool _tooltipInfoPool;

        [Inject] private InventorySlotWidget.Pool _inventorySlotWidgetPool;
        [Inject] private InventorySlotDraggableWidget.Pool _inventorySlotDraggableWidgetPool;
    }
}