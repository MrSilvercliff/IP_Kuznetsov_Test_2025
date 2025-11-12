using _Project.Scripts.GameScene.Services.ObjectPools;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Player
{
    public interface IPlayerService : IProjectService
    { 
        IPlayerInventoryService Inventory { get; }
    }

    public class PlayerService : IPlayerService
    {
        public IPlayerInventoryService Inventory => _inventoryService;

        [Inject] private IPlayerInventoryService _inventoryService;

        public async Task<bool> Init()
        {
            await _inventoryService.Init();
            return true;
        }

        public bool Flush()
        {
            _inventoryService.Flush();
            return true;
        }
    }
}