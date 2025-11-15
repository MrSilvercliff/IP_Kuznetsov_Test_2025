using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventorySlotService : IProjectService
    { 
        bool HasEmptyInventorySlot(IInventoryController inventoryController, out IInventorySlotController emptyInventorySlotController);
    }

    public class InventorySlotService : IInventorySlotService
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public bool HasEmptyInventorySlot(IInventoryController inventoryController, out IInventorySlotController emptyInventorySlotController)
        {
            var result = false;
            emptyInventorySlotController = null;

            var slotControllers = inventoryController.SlotControllers;

            for (int i = 0; i < slotControllers.Count; i++)
            { 
                var slotController = slotControllers[i];

                if (slotController.IsEmpty)
                {
                    result = true;
                    emptyInventorySlotController = slotController;
                    break;
                }
            }

            return result;
        }
    }
}