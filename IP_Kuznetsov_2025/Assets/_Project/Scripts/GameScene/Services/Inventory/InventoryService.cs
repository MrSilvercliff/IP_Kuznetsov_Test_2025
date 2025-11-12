using _Project.Scripts.GameScene.Inventory;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Inventory
{
    public interface IInventoryService : IProjectService
    {
    }

    public class InventoryService : IInventoryService
    {
        public async Task<bool> Init()
        {
            return true;
        }

        public bool Flush()
        {
            return true;
        }
    }
}