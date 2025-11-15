using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftService : IProjectService
    { 
        ICraftInventoryService InventoryService { get; }
        void CraftItem();
    }

    public class CraftService : ICraftService
    {
        public ICraftInventoryService InventoryService => _craftInventoryService;

        [Inject] private ICraftInventoryService _craftInventoryService;
        [Inject] private ICraftInventoryAnalyzeService _craftInventoryAnalyzeService;
        [Inject] private ICraftProcessService _craftProcessService;

        public async Task<bool> Init()
        {
            await _craftInventoryService.Init();
            await _craftInventoryAnalyzeService.Init();
            await _craftProcessService.Init();
            return true;
        }

        public bool Flush()
        {
            _craftInventoryAnalyzeService.Flush();
            _craftInventoryService.Flush();
            _craftProcessService.Flush();
            return true;
        }

        public void CraftItem()
        {
            _craftProcessService.CraftItem();
        }
    }
}