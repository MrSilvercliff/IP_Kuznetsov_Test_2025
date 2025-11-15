using _Project.Scripts.Application.Project.Services.Balance;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.Configs;

namespace _Project.Scripts.Project.Configs
{
    [CreateAssetMenu(fileName = "ProjectBalanceConfig", menuName = "Project/Configs/Project/Project Balance Config")]
    public class ProjectBalanceConfig : BalanceConfigBase
    {
        public override void ParseBalance()
        {
            var googleSheetParser = new BalanceGoogleSheetParser();
            googleSheetParser.Setup(this);
            googleSheetParser.ParseBalance();
        }
    }
}