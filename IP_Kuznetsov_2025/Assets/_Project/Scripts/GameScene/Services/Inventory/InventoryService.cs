using _Project.Scripts.GameScene.Inventory;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventoryService : IProjectService
    {
        void FillRandom(IInventoryController targetInventory);
        void ClearInventory(IInventoryController inventoryController);
    }

    public class InventoryService : IInventoryService
    {
        [Inject] private IInventoryItemAddService _itemAddService;
        [Inject] private IInventoryItemRemoveService _itemRemoveService;
        [Inject] private IInventorySlotService _inventorySlotService;

        public async Task<bool> Init()
        {
            return true;
        }

        public bool Flush()
        {
            return true;
        }

        public void FillRandom(IInventoryController targetInventory)
        {
            _itemAddService.FillRandom(targetInventory);
        }

        public void ClearInventory(IInventoryController inventoryController)
        {
            _itemRemoveService.ClearInventory(inventoryController);
        }
    }
}