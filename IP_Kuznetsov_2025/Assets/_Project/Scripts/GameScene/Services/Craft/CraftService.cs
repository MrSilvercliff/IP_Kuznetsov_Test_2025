using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Craft
{
    public interface ICraftService : IProjectService
    { 
    }

    public class CraftService : ICraftService
    {
        [Inject] private ICraftInventoryService _craftInventoryService;
        [Inject] private ICraftInventoryAnalyzeService _craftInventoryAnalyzeService;

        public async Task<bool> Init()
        {
            await _craftInventoryService.Init();
            await _craftInventoryAnalyzeService.Init();
            return true;
        }

        public bool Flush()
        {
            _craftInventoryAnalyzeService.Flush();
            _craftInventoryService.Flush();
            return true;
        }
    }
}