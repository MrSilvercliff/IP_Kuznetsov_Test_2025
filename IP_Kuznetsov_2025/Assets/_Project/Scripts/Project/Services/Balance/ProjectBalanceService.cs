using _Project.Scripts.Project.Services.Balance.Storages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Balance
{
    public interface IProjectBalanceService : IBalanceServiceAbstractAsync
    {
        IInventoryConfigBalanceStorage InventoryConfig { get; }
        IGameItemBalanceStorage GameItems { get; }
        IWorkbenchConfigBalanceStorage WorkbenchConfig { get; }
        ICraftRecipeBalanceStorage CraftRecipes { get; }
    }

    public class ProjectBalanceService : BalanceServiceAbstractAsync, IProjectBalanceService
    {
        public IInventoryConfigBalanceStorage InventoryConfig { get; }
        public IGameItemBalanceStorage GameItems { get; }
        public IWorkbenchConfigBalanceStorage WorkbenchConfig { get; }
        public ICraftRecipeBalanceStorage CraftRecipes { get; }

        public ProjectBalanceService() 
        {
            InventoryConfig = new InventoryConfigBalanceStorage();
            GameItems = new GameItemBalanceStorage();
            WorkbenchConfig = new WorkbenchConfigBalanceStorage();
            CraftRecipes = new CraftRecipeBalanceStorage();
        }

        protected override HashSet<IProjectService> GetStoragesToInit()
        {
            var result = new HashSet<IProjectService>();

            result.Add(InventoryConfig);
            result.Add(GameItems);
            result.Add(WorkbenchConfig);
            result.Add(CraftRecipes);

            return result;
        }
    }
}