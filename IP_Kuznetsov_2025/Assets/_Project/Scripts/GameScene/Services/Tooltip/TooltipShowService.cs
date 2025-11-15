using _Project.Scripts.GameScene.GameItems;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Tooltip
{
    public interface ITooltipShowService : IProjectService
    {
        void ShowGameItemTooltip(IGameItem gameItem, Vector3 position);
        void HideGameItemTooltip(IGameItem gameItem, Vector3 position);
    }

    public class TooltipShowService : ITooltipShowService
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void ShowGameItemTooltip(IGameItem gameItem, Vector3 position)
        {
        }

        public void HideGameItemTooltip(IGameItem gameItem, Vector3 position)
        {
        }
    }
}