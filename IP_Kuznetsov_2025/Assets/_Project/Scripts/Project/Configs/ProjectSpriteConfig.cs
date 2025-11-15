using System;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.Project.Configs
{
    public interface IProjectSpriteConfig
    {
        Sprite GetGameItemIcon(string iconId);
    }

    [CreateAssetMenu(fileName = "ProjectSpriteConfig", menuName = "Project/Configs/Project/Project Sprite Config")]
    public class ProjectSpriteConfig : ScriptableObject, IProjectSpriteConfig
    {
        [SerializeField] private SpriteConfigSpriteItem[] _gameItemIcons;

        public Sprite GetGameItemIcon(string iconId)
        {
            for (int i = 0; i < _gameItemIcons.Length; i++)
            {
                var iconSpriteItem = _gameItemIcons[i];

                if (iconSpriteItem.SpriteId == iconId)
                    return iconSpriteItem.Sprite;
            }

            LogUtils.Error(this, $"Game item icon with id [{iconId}] DOES NOT EXIST!");
            return null;
        }

        [Serializable]
        public class SpriteConfigSpriteItem
        {
            public string SpriteId => _spriteId;
            public Sprite Sprite => _sprite;

            [SerializeField] private string _spriteId;
            [SerializeField] private Sprite _sprite;
        }
    }
}