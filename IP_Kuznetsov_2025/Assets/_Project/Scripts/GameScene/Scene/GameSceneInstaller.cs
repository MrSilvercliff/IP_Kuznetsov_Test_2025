using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.GameScene.GameItems;
using _Project.Scripts.GameScene.Input;
using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Inventory;
using _Project.Scripts.GameScene.Services.ObjectPools;
using _Project.Scripts.GameScene.Services.Player;
using _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot;
using System;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.ZenjectExtentions.SceneInstallers;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneInstaller : SceneInstaller
    {
        [SerializeField] private DragAndDropConfig _dragAndDropConfig;
        [SerializeField] private GameSceneObjectPoolContainer _objectPoolContainer;
        [SerializeField] private UnityInputHandler _unityInputHandler;

        protected override void OnInstallBindings()
        {
            BindInput();

            BindDragAndDrop();

            BindConfigs();

            BindObjectPools();

            BindPlayerServices();

            BindInventoryServices();

            BindServiceIniter();
        }

        private void BindServiceIniter()
        {
            Container.Bind<IGameSceneServiceIniter>().To<GameSceneServiceIniter>().AsSingle();
        }

        private void BindInput()
        { 
            Container.Bind<IInputController>().To<InputController>().AsSingle();
            Container.Bind<IInputHandler>().FromInstance(_unityInputHandler).AsSingle();
        }

        private void BindDragAndDrop()
        {
            Container.Bind<IDragAndDropConfig>().FromInstance(_dragAndDropConfig).AsSingle();
            Container.Bind<IDragAndDropController>().To<DragAndDropController>().AsSingle();
        }

        private void BindConfigs()
        { 
        }

        private void BindObjectPools()
        { 
            BindNonMonoPools();
            BindMonoPools();

            Container.Bind<IGameSceneObjectPoolService>().To<GameSceneObjectPoolService>().AsSingle();
        }

        private void BindNonMonoPools()
        {
            Container.BindMemoryPool<GameItem, GameItem.Pool>();
            Container.BindMemoryPool<InventoryController, InventoryControllerPool>();
            Container.BindMemoryPool<InventorySlotController, InventorySlotController.Pool>();
        }

        private void BindMonoPools()
        { 
            BindInventorySlotWidgetPool();
            BindInventorySlotDraggableWidgetPool();
        }

        private void BindInventorySlotWidgetPool()
        {
            var objectPoolItem = _objectPoolContainer.InventorySlotWidget;

            var prefab = objectPoolItem.Prefab;
            var container = objectPoolItem.Container;
            var initSize = objectPoolItem.PoolInitialSize;

            Container.BindMemoryPool<InventorySlotWidget, InventorySlotWidget.Pool>()
                .WithInitialSize(initSize)
                .FromComponentInNewPrefab(prefab)
                .UnderTransform(container);
        }

        private void BindInventorySlotDraggableWidgetPool()
        {
            var objectPoolItem = _objectPoolContainer.InventorySlotDraggableWidget;

            var prefab = objectPoolItem.Prefab;
            var container = objectPoolItem.Container;
            var initSize = objectPoolItem.PoolInitialSize;

            Container.BindMemoryPool<InventorySlotDraggableWidget, InventorySlotDraggableWidget.Pool>()
                .WithInitialSize(initSize)
                .FromComponentInNewPrefab(prefab)
                .UnderTransform(container);
        }

        private void BindPlayerServices()
        { 
            Container.Bind<IPlayerInventoryService>().To<PlayerInventoryService>().AsSingle();
            Container.Bind<IPlayerService>().To<PlayerService>().AsSingle();
        }

        private void BindInventoryServices()
        {
            Container.Bind<IInventorySlotService>().To<InventorySlotService>().AsSingle();
            Container.Bind<IInventoryItemAddService>().To<InventoryItemAddService>().AsSingle();
            Container.Bind<IInventoryItemRemoveService>().To<InventoryItemRemoveService>().AsSingle();
            Container.Bind<IInventoryService>().To<InventoryService>().AsSingle();
        }
    }
}