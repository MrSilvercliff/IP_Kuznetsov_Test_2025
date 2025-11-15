using _Project.Scripts.GameScene.Services.Tooltip;
using _Project.Scripts.GameScene.UI.Events;
using _Project.Scripts.GameScene.UI.Widgets.Tooltip;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.EventBus.Async;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Panels;

namespace _Project.Scripts.GameScene.UI.Panels.TooltipPanel
{
    public class TooltipPanel : PanelWindow
    {
        [SerializeField] private TooltipWidget _tooltipWidget;

        [Inject] private IEventBusAsync _eventBus;
        [Inject] private ITooltipService _tooltipService;

        protected override Task OnPreOpen()
        {
            _eventBus.Subscribe<TooltipsChangedEvent>(OnTooltipsChangedEvent);
            return Task.CompletedTask;
        }

        protected override Task OnPreClose()
        {
            _eventBus.UnSubscribe<TooltipsChangedEvent>(OnTooltipsChangedEvent);
            return Task.CompletedTask;
        }

        private void RefreshTooltip()
        { 
            var allTooltipInfos = _tooltipService.GetAllTooltipInfos();

            if (allTooltipInfos.Count == 0)
            { 
                _tooltipWidget.SetActive(false);
                return;
            }

            var tooltipArray = (ITooltipInfo[])allTooltipInfos;
            var first = tooltipArray[0];
            _tooltipWidget.Setup(first);
            _tooltipWidget.SetActive(true);
        }

        private async Task OnTooltipsChangedEvent(TooltipsChangedEvent evnt)
        {
            RefreshTooltip();
        }
    }
}