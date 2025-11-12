using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Services.ObjectPools
{
    public interface IGameSceneObjectPoolService
    { 
        GameItem.Pool GameItemPool { get; }
        InventoryControllerPool InventoryControllerPool { get; }
        InventorySlotController.Pool InventorySlotControllerPool { get; }
    }

    public class GameSceneObjectPoolService : IGameSceneObjectPoolService
    {
        public GameItem.Pool GameItemPool => _gameItemPool;
        public InventoryControllerPool InventoryControllerPool => _inventoryControllerPool;
        public InventorySlotController.Pool InventorySlotControllerPool => _inventorySlotControllerPool;

        [Inject] private GameItem.Pool _gameItemPool;
        [Inject] private InventoryControllerPool _inventoryControllerPool;
        [Inject] private InventorySlotController.Pool _inventorySlotControllerPool;
    }
}