using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Services.ObjectPools
{
    public interface IGameObjectPoolService
    { 
        GameItem.Pool GameItemPool { get; }
        InventoryController.Pool InventoryControllerPool { get; }
        InventorySlotController.Pool InventorySlotControllerPool { get; }
    }

    public class GameSceneObjectPoolService : IGameObjectPoolService
    {
        public GameItem.Pool GameItemPool => _gameItemPool;
        public InventoryController.Pool InventoryControllerPool => _inventoryControllerPool;
        public InventorySlotController.Pool InventorySlotControllerPool => _inventorySlotControllerPool;

        [Inject] private GameItem.Pool _gameItemPool;
        [Inject] private InventoryController.Pool _inventoryControllerPool;
        [Inject] private InventorySlotController.Pool _inventorySlotControllerPool;
    }
}