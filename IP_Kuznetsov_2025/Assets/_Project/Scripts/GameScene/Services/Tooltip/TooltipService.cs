using _Project.Scripts.GameScene.GameItems;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Tooltip
{
    public interface ITooltipService : IProjectService
    {
        void ShowGameItemTooltip(IGameItem gameItem, Vector3 position);
        void HideGameItemTooltip(IGameItem gameItem, Vector3 position);
    }

    public class TooltipService : ITooltipService
    {
        [Inject] private ITooltipShowService _showService;

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
            _showService.ShowGameItemTooltip(gameItem, position);
        }

        public void HideGameItemTooltip(IGameItem gameItem, Vector3 position)
        {
            _showService.HideGameItemTooltip(gameItem, position);
        }
    }
}