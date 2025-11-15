using Defective.JSON;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface IWorkbenchConfigBalanceModel : IBalanceModelBase
    { 
        int WorkbenchInventorySlotCount { get; }
    }

    public class WorkbenchConfigBalanceModel : BalanceModelBase, IWorkbenchConfigBalanceModel
    {
        public int WorkbenchInventorySlotCount { get; private set; }

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            WorkbenchInventorySlotCount = json["workbench_inventory_slots_count"].intValue;
        }

        public override void DebugPrint()
        {
        }
    }
}