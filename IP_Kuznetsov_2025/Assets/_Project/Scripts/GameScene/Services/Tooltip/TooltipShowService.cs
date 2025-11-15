using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Tooltip
{
    public interface ITooltipShowService : IProjectService
    {
        void ShowInventorySlotTooltip(IInventorySlotController inventorySlotController, Vector3 position);
        void HideInventorySlotTooltip(IInventorySlotController inventorySlotController);
    }

    public class TooltipShowService : ITooltipShowService
    {
        [Inject] private IGameSceneObjectPoolService _objectPoolService;
        [Inject] private ITooltipRepository _repository;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            FlushTooltips();
            return true;
        }

        public void ShowInventorySlotTooltip(IInventorySlotController inventorySlotController, Vector3 position)
        {
            var tooltipInfoPool = _objectPoolService.TooltipInfoPool;
            var tooltipInfo = tooltipInfoPool.Spawn();
            tooltipInfo.Setup(inventorySlotController, position);
            _repository.Add(inventorySlotController, tooltipInfo);
        }

        public void HideInventorySlotTooltip(IInventorySlotController inventorySlotController)
        {
            var tryResult =  _repository.TryGet(inventorySlotController, out var tooltipInfo);

            if (!tryResult)
                return;

            _repository.Remove(inventorySlotController);
            DespawnTooltip((TooltipInfo)tooltipInfo);
        }

        private void FlushTooltips()
        {
            var allTooltips = _repository.GetAll();

            foreach (var tooltipInfo in allTooltips)
                DespawnTooltip((TooltipInfo)tooltipInfo);
        }

        private void DespawnTooltip(TooltipInfo tooltipInfo)
        {
            var tooltipInfoPool = _objectPoolService.TooltipInfoPool;
            tooltipInfoPool.Despawn(tooltipInfo);
        }
    }
}