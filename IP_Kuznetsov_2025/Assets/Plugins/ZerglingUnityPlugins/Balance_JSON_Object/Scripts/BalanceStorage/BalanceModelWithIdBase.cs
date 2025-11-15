using UnityEngine;

namespace ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage
{
    public interface IBalanceModelWithIdBase : IBalanceModelBase
    {
        string Id { get; }
    }

    public abstract class BalanceModelWithIdBase : BalanceModelBase, IBalanceModelWithIdBase
    {
        public string Id => _id;

        protected string _id;
    }
}
