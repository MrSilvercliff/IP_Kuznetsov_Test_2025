using Defective.JSON;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface IInventoryBalanceModel : IBalanceModelBase
    { 
        int SlotsCount { get; }
    }

    public class InventoryBalanceModel : BalanceModelBase, IInventoryBalanceModel
    {
        public int SlotsCount => _slotsCount;

        private int _slotsCount;

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            _slotsCount = json["slots_count"].intValue;
        }

        public override void DebugPrint()
        {
        }
    }
}