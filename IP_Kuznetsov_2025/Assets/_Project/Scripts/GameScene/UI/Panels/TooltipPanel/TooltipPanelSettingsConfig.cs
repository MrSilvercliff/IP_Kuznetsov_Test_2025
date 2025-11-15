using UnityEngine;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Panels;

namespace _Project.Scripts.GameScene.UI.Panels.TooltipPanel
{
    public interface ITooltipPanelSettingsConfig : IPanelSettingsConfig
    { 
    }

    [CreateAssetMenu(fileName = "TooltipPanelSettingsConfig", menuName = "Project/Configs/Game Scene/UI/Panel Settings/Tooltip Panel Settings Config")]
    public class TooltipPanelSettingsConfig : PanelSettingsConfig, ITooltipPanelSettingsConfig
    {

    }
}