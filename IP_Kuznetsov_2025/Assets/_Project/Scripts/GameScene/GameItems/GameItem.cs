using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance.Models;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.GameItems
{
    public interface IGameItem
    { 
        string Id { get; }
        string IconId { get; }
        GameItemType Type { get; }
        string Name { get; }
        string Description { get; }
        bool IsStackable { get; }
        int StackSize { get; }
        int Count { get; }

        void Setup(IGameItemBalanceModel balanceModel);
        void SetCount(int newCount);
    }

    public class GameItem : IGameItem
    {
        public string Id => _balanceModel.Id;
        public string IconId => _balanceModel.IconId;

        public GameItemType Type => _balanceModel.ItemType;

        public string Name => _balanceModel.Name;
        public string Description => _balanceModel.Description;

        public bool IsStackable => _balanceModel.IsStackable;
        public int StackSize => _balanceModel.StackSize;

        public int Count { get; private set; }

        private IGameItemBalanceModel _balanceModel;

        public GameItem()
        {
            _balanceModel = null;
            Count = 0;
        }

        public void Setup(IGameItemBalanceModel balanceModel)
        {
            _balanceModel = balanceModel;
        }

        public void SetCount(int newCount)
        {
            Count = newCount;
        }

        public class Pool : MemoryPool<GameItem> 
        {
            protected override void OnDespawned(GameItem item)
            {
                item.Setup(null);
                item.SetCount(0);
            }
        }
    }
}