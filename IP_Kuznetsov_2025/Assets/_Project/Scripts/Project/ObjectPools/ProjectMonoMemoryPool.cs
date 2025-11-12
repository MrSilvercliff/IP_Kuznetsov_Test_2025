using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.ObjectPool;
using IPoolable = ZerglingUnityPlugins.Tools.Scripts.ObjectPool.IPoolable;

namespace _Project.Scripts.Project.ObjectPools
{
    public class ProjectMonoMemoryPool<TProjectPoolable> : MonoMemoryPool<TProjectPoolable> where TProjectPoolable : Component, IPoolable
    {
        protected override void OnCreated(TProjectPoolable item)
        {
            base.OnCreated(item);
            item.OnCreated();
        }

        protected override void OnSpawned(TProjectPoolable item)
        {
            item.OnSpawned();
        }

        protected override void OnDespawned(TProjectPoolable item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}