using System;
using System.Collections.Generic;
using System.IO;

namespace Pulsar.Resources
{
    public class ResourceManager
    {
        /// <summary>
        /// Store all loaded resources, do not remove/move element in this list, resource store their position to have a fast access.  
        /// </summary>
        private readonly List<Resource> _resources = new List<Resource>();

        private Dictionary<Type, IResourceLoader> _loaders = new Dictionary<Type, IResourceLoader>(); 
        public T GetResource<T>(string path) where T : Resource
        {
            return (T)_resources.Find((resource => { if (resource.GetPath() == path) return true; return false; }));
        }

        public T Load<T>(string path) where T : Resource
        {
            return Load<T>(path, File.ReadAllBytes(path));
        }
        
        public T Load<T>(string path, byte[] data) where T : Resource
        {
            if (!_loaders.ContainsKey(typeof(T)))
            {
                //TODO: logger
                return null;
            }
            return (T) AddResource(_loaders[typeof(T)].Load(path, data));
        }

        private Resource AddResource(Resource r)
        {
            lock (_resources)
            {
                _resources.Add(r);
                return r;
            }
        }

        public bool AddLoader(Type t, IResourceLoader loader)
        {
            if (_loaders.ContainsKey(t))
                return false;

            _loaders.Add(t, loader);
            return true;
        }
    }
}