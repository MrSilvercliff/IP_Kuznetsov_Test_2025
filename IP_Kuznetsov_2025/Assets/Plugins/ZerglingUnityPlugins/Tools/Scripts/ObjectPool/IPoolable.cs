using UnityEngine;
using Zenject;

namespace ZerglingUnityPlugins.Tools.Scripts.ObjectPool
{
    public interface IPoolable : Zenject.IPoolable
    {
        void OnCreated();
    }
}