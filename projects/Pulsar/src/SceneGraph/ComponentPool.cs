using System;
using System.Collections;

namespace Pulsar.SceneGraph
{
    public class ComponentPool<T> : IComponentPool
    {
        private T[] _pool;
        private uint?[] _owners;
        private uint _nextFree;
        private bool _canGrow = true;
        private float _growFactor = 1.5f;
        private uint _used;
        public ComponentPool(uint capacity, bool canGrow, float growFactor)
        {
            _pool = new T[capacity];
            _owners = new uint?[capacity];
            _nextFree = 1; // 0 reserved when there is no more space. In this case all new component are redirected to index 0. DefaultValue
            _canGrow = canGrow;
            _growFactor = growFactor;
            _pool[0] = default;
        }

        private void Resize(uint capacity)
        {
            Array.Resize(ref _pool, (int)capacity);
            Array.Resize(ref _owners, (int)capacity);
        }

        public uint New(uint localId)
        {
            uint result = _nextFree;
            _owners[result] = localId;
            _nextFree = FindNextFree();
            if (result != 0)
                _used++;
            _pool[result] = default;
            return result;
        }

        public ref T Get(uint i)
        {
            return ref _pool[i];
        }

        public void Set(uint i, T c)
        {
            _pool[i] = c;
        }
        
        public void Recycle(uint i)
        {
            _owners[i] = null;
            _pool[i] = default;
            if (i != 0)
                _used--;
        }
        
        private uint FindNextFree()
        {
            uint total = 0;
            uint i = _nextFree;
            while (total < _owners.Length)
            {
                if (_owners[i] == null && i != 0)
                    return i;

                i++;
                if (i == _owners.Length)
                    i = 0;
                total++;
            }

            if (_canGrow)
                Resize(Convert.ToUInt32(_owners.Length * _growFactor));
            else
                return 0;
            
            return FindNextFree();
        }

        public uint GetUsedCount()
        {
            return _used;
        }
    }
}