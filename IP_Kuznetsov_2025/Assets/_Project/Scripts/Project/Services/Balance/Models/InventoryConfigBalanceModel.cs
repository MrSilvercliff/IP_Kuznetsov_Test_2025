using Defective.JSON;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface IInventoryConfigBalanceModel : IBalanceModelBase
    { 
        int SlotsCount { get; }
    }

    public class InventoryConfigBalanceModel : BalanceModelBase, IInventoryConfigBalanceModel
    {
        public int SlotsCount { get; private set; }

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            SlotsCount = json["slots_count"].intValue;
        }

        public override void DebugPrint()
        {
        }
    }
}