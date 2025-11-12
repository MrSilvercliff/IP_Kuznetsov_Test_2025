using System.Collections.Generic;
using UnityEngine;
using Zenject;
using IPoolable = ZerglingUnityPlugins.Tools.Scripts.ObjectPool.IPoolable;

namespace ZerglingUnityPlugins.UI.Scripts.Pools
{
    public interface IZenjectFactoryMonoPool<T> where T : Component
    {
        T Spawn();
        void Despawn(T obj);
        void Flush();
    }

    public class ZenjectFactoryMonoPool<T> : IZenjectFactoryMonoPool<T> where T : Component, IPoolable
    {
        private T _sourceObject;
        private readonly Stack<T> _stack = new Stack<T>();
        private Transform _parent;
        private readonly IFactory<T, T> _factory;

        public ZenjectFactoryMonoPool(T source, IFactory<T, T> factory, Transform parent = null, int initCount = 0)
        {
            _sourceObject = source;
            _parent = parent == null ? _sourceObject.transform.parent : parent;
            _factory = factory;

            if (initCount > 0) 
                WarmingUp(initCount);
        }

        public T Spawn()
        {
            if (_stack.Count == 0)
                Instantiate();

            T obj = _stack.Pop();
            obj.OnSpawned();
            return obj;
        }

        public void Despawn(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent);
            obj.OnDespawned();
            _stack.Push(obj);
        }

        private void WarmingUp(int count)
        {
            for (int i = 0; i < count; i++)
                Instantiate();
        }

        private T Instantiate()
        {
            T obj = _factory.Create(_sourceObject);
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent);
            _stack.Push(obj);
            obj.OnCreated();
            return obj;
        }

        public void Flush()
        {
            while (_stack.Count > 0)
            { 
                var obj = _stack.Pop();
                GameObject.Destroy(obj.gameObject);
            }
        }
    }
}

