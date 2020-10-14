using System;
using System.Collections.Generic;

namespace Pulsar.SceneGraph
{
    public class Scene
    {
        /// <summary>
        /// Actor cache.
        /// </summary>
        private readonly Dictionary<Type, IComponentPool> _compPools = new Dictionary<Type, IComponentPool>();

        private ActorDataPool _actorDataPool;

        public Scene()
        {
            _actorDataPool = new ActorDataPool(500, true, 1.1f);
        }

        public void RegisterComponent<T>(uint capacity, bool canGrow, float growFactor)
        {
            if (_compPools.ContainsKey(typeof(T)))
                return;
            
            _compPools.Add(typeof(T), new ComponentPool<T>(capacity, canGrow, growFactor));
        }
        
        public ref T AddComponent<T>(uint localId)
        {
            var pool = (ComponentPool<T>) _compPools[typeof(T)];
            uint i = pool.New(localId);
            ActorData d = _actorDataPool.Get(localId);
            d.Components.Add(typeof(T), i);
            return ref pool.Get(i);
        }

        public void SetComponent<T>(uint localId, T c)
        {
            var pool = (ComponentPool<T>) _compPools[typeof(T)];
            pool.Set(_actorDataPool.Get(localId).Components[typeof(T)], c);
        }
        
        public ref T GetComponent<T>(uint localId)
        {
            var pool = (ComponentPool<T>) _compPools[typeof(T)];
            return ref pool.Get(_actorDataPool.Get(localId).Components[typeof(T)]);
        }
        
        public void RemoveComponent<T>(uint localId)
        {
            var pool = (ComponentPool<T>) _compPools[typeof(T)];
            pool.Recycle(_actorDataPool.Get(localId).Components[typeof(T)]);
            _actorDataPool.Get(localId).Components.Remove(typeof(T));
        }

        public void RemoveComponent<T>(Actor a)
        {
            RemoveComponent<T>(a.LocalId);
        }

        public Actor CreateActor()
        {
            return CreateActor(Guid.Empty);
        }

        public Actor CreateActor(Guid networkId)
        {
            uint localId = _actorDataPool.New();
            _actorDataPool.Get(localId).Reset(networkId);
            Actor a = new Actor(this, localId);
            return a;
        }

        public void RemoveActor(uint localId)
        {
            foreach (var keyValuePair in _actorDataPool.Get(localId).Components)
            {
                _compPools[keyValuePair.Key].Recycle(keyValuePair.Value);
            }
            _actorDataPool.Get(localId).Components.Clear();
            
            foreach (var child in _actorDataPool.Get(localId).Children)
            {
                RemoveActor(child);
            }
            _actorDataPool.Recycle(localId);
        }
        
        public ref ActorData GetActorData(uint localId)
        {
            return ref _actorDataPool.Get(localId);
        }

        public ref ActorData GetActorData(Actor a)
        {
            return ref GetActorData(a.LocalId);
        }

        public void AddChild(uint parentLocalId, uint childLocalId)
        {
            ActorData child = _actorDataPool.Get(childLocalId);
            ActorData parent = _actorDataPool.Get(parentLocalId);
            if (child.Parent != 0)
            {
                ActorData cparent = _actorDataPool.Get(child.Parent);
                cparent.Children.Remove(childLocalId);
            }
            
            child.Parent = parentLocalId;
            parent.Children.Add(childLocalId);
        }

        public void RemoveChild(uint parentLocalId, uint childLocalId)
        {
            _actorDataPool.Get(childLocalId).Parent = 0;
            _actorDataPool.Get(parentLocalId).Children.Remove(childLocalId);
        }
    }
}