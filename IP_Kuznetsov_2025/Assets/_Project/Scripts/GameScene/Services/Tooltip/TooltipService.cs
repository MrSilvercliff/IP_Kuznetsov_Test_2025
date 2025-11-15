using _Project.Scripts.GameScene.Inventory;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Tooltip
{
    public interface ITooltipService : IProjectService
    {
        void ShowInventorySlotTooltip(IInventorySlotController inventorySlotController, Vector3 position);
        void HideInventorySlotTooltip(IInventorySlotController inventorySlotController);
    }

    public class TooltipService : ITooltipService
    {
        [Inject] private ITooltipRepository _repository;
        [Inject] private ITooltipShowService _showService;

        public Task<bool> Init()
        {
            _repository.Init();
            _showService.Init();
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            _showService.Flush();
            _repository.Flush();
            return true;
        }

        public void ShowInventorySlotTooltip(IInventorySlotController inventorySlotController, Vector3 position)
        {
            _showService.ShowInventorySlotTooltip(inventorySlotController, position);
        }

        public void HideInventorySlotTooltip(IInventorySlotController inventorySlotController)
        {
            _showService.HideInventorySlotTooltip(inventorySlotController);
        }
    }
}