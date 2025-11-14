using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage.Async;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface IWorkbenchConfigBalanceStorage : IBalanceStorageConfigAsyncBase<IWorkbenchConfigBalanceModel, WorkbenchConfigBalanceModel>
    { 
        int WorkbenchInventorySlotCount { get; }
    }

    public class WorkbenchConfigBalanceStorage : BalanceStorageConfigAsyncBase<IWorkbenchConfigBalanceModel, WorkbenchConfigBalanceModel>, IWorkbenchConfigBalanceStorage
    {
        public int WorkbenchInventorySlotCount => _balanceModel.WorkbenchInventorySlotCount;

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        protected override void OnBalanceModelAdded(IWorkbenchConfigBalanceModel balanceModel)
        {
        }
    }
}