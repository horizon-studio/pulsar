using System;
using System.Collections.Generic;

namespace Pulsar.SceneGraph
{
    public struct ActorData
    {
        public Guid NetworkID;
        public Dictionary<Type, uint> Components;
        public List<uint> Children;
        public uint Parent;
        
        public void Reset(Guid networkId)
        {
            NetworkID = networkId;
            if (Components == null)
                Components = new Dictionary<Type, uint>();
            else
                Components.Clear();
            if (Children == null)
                Children = new List<uint>();
            else 
                Children.Clear();
            Parent = 0;
        }
    }
}