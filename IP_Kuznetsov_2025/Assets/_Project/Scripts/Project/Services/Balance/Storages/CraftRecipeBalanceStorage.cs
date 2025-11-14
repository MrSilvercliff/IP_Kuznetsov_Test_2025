using _Project.Scripts.Project.Services.Balance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage.Async;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface ICraftRecipeBalanceStorage : IBalanceStorageDictionaryAsyncBase<ICraftRecipeBalanceModel, CraftRecipeBalanceModel>
    { 
        IReadOnlyList<ICraftRecipeBalanceModel> GetByCenterGameItemId(string centerGameItemId);
    }

    public class CraftRecipeBalanceStorage : BalanceStorageDictionaryAsyncBase<ICraftRecipeBalanceModel, CraftRecipeBalanceModel>, ICraftRecipeBalanceStorage
    {
        private Dictionary<string, List<ICraftRecipeBalanceModel>> _craftRecipesByCenterGameItemId;

        public CraftRecipeBalanceStorage()
        {
            _craftRecipesByCenterGameItemId = new();
        }

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        protected override void OnBalanceModelAdded(ICraftRecipeBalanceModel balanceModel)
        {
            AddByCenterGameItemId(balanceModel);
        }

        private void AddByCenterGameItemId(ICraftRecipeBalanceModel balanceModel)
        {
            var craftRecipeItemsId = balanceModel.CraftRecipeItemsId;
            var centerItemIndex = Mathf.FloorToInt((float)craftRecipeItemsId.Count / 2);
            var centerItemId = craftRecipeItemsId[centerItemIndex];

            if (!_craftRecipesByCenterGameItemId.TryGetValue(centerItemId, out var list))
            { 
                list = new List<ICraftRecipeBalanceModel>();
                _craftRecipesByCenterGameItemId[centerItemId] = list;
            }

            list.Add(balanceModel);
        }

        public IReadOnlyList<ICraftRecipeBalanceModel> GetByCenterGameItemId(string centerGameItemId)
        {
            if (!_craftRecipesByCenterGameItemId.TryGetValue(centerGameItemId, out var result))
                return null;

            return result;
        }
    }
}