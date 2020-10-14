using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pulsar.SceneGraph
{
    public class Actor : IDisposable
    {
        private Scene _scene;
        public readonly uint LocalId;
        
        public Actor(Scene scene, uint localId)
        {
            _scene = scene;
            LocalId = localId;
        }
        
        public ref T AddComponent<T>()
        {
            return ref _scene.AddComponent<T>(LocalId);
        }

        public void SetComponent<T>(T c)
        {
            _scene.SetComponent<T>(LocalId, c);
        }
        
        public ref T GetComponent<T>()
        {
            return ref _scene.GetComponent<T>(LocalId);
        }
        
        public void RemoveComponent<T>()
        {
            _scene.RemoveComponent<T>(LocalId);
        }

        public void Dispose()
        {
            _scene.RemoveActor(LocalId);
        }
    }
}