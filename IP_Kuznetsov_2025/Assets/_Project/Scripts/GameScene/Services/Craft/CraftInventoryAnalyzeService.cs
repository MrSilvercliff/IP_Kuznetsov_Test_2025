using _Project.Scripts.GameScene.Inventory;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftInventoryAnalyzeService : IProjectService
    { 
    }

    public class CraftInventoryAnalyzeService : ICraftInventoryAnalyzeService
    {
        [Inject] private ICraftInventoryService _inventoryService;

        public Task<bool> Init()
        {
            _inventoryService.InventoryController.SlotItemChangedEvent += OnInventorySlotChangedEvent;
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            _inventoryService.InventoryController.SlotItemChangedEvent -= OnInventorySlotChangedEvent;
            return true;
        }

        private void OnInventorySlotChangedEvent(IInventoryController inventoryController, IInventorySlotController inventorySlotController)
        {
        }
    }
}