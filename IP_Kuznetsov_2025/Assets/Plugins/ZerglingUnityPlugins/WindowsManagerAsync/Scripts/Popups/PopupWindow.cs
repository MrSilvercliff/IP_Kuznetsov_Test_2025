using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Basics;
using IPoolable = ZerglingUnityPlugins.Tools.Scripts.ObjectPool.IPoolable;

namespace ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Popups
{
    public interface IPopupWindow : IWindow, IPoolable
    {
    }

    public abstract class PopupWindow : Window, IPopupWindow
    {
        public void OnCreated()
        {
        }

        public void OnSpawned()
        {
        }

        public void OnDespawned()
        {
        }

        public class Factory : PlaceholderFactory<PopupWindow, PopupWindow> { }
    }
}


