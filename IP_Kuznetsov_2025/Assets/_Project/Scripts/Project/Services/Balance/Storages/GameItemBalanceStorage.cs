using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage.Async;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface IGameItemBalanceStorage : IBalanceStorageDictionaryAsyncBase<IGameItemBalanceModel, GameItemBalanceModel>
    { 
    }

    public class GameItemBalanceStorage : BalanceStorageDictionaryAsyncBase<IGameItemBalanceModel, GameItemBalanceModel>, IGameItemBalanceStorage
    {
        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        protected override void OnBalanceModelAdded(IGameItemBalanceModel balanceModel)
        {
        }
    }
}