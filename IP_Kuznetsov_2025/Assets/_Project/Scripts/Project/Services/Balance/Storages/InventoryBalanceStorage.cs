using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage.Async;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface IInventoryBalanceStorage : IBalanceStorageConfigAsyncBase<IInventoryBalanceModel, InventoryBalanceModel>
    { 
        int SlotsCount { get; }
    }

    public class InventoryBalanceStorage : BalanceStorageConfigAsyncBase<IInventoryBalanceModel, InventoryBalanceModel>, IInventoryBalanceStorage
    {
        public int SlotsCount => _balanceModel.SlotsCount;

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        protected override void OnBalanceModelAdded(IInventoryBalanceModel balanceModel)
        {
        }
    }
}