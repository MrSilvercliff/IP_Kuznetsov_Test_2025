using _Project.Scripts.GameScene.Inventory;
using Plugins.ZerglingUnityPlugins.Tools.Scripts.Repositories;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Tooltip
{
    public interface ITooltipRepository : IRepositoryDictionary<IInventorySlotController, ITooltipInfo>, IProjectService
    { 
    }

    public class TooltipRepository : RepositoryDictionary<IInventorySlotController, ITooltipInfo>, ITooltipRepository
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public override void Add(ITooltipInfo item)
        {
            var inventorySlotController = item.InventorySlotController;
            Add(inventorySlotController, item);
        }

        public override void Add(IInventorySlotController key, ITooltipInfo item)
        {
            _itemsDictionary.TryAdd(key, item);
        }

        public override void Remove(IInventorySlotController key)
        {
            _itemsDictionary.Remove(key);
        }

        public override void Remove(ITooltipInfo item)
        {
            var key = item.InventorySlotController;
            Remove(key);
        }
    }
}