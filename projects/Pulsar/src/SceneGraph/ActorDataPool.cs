using System;

namespace Pulsar.SceneGraph
{
    public class ActorDataPool
    {
        private ActorData[] _pool;
        private bool[] _ownedTable;
        private uint _nextFree;
        private bool _canGrow = true;
        private float _growFactor = 1.5f;
        private uint _used;
        public ActorDataPool(uint capacity, bool canGrow, float growFactor)
        {
            _pool = new ActorData[capacity];
            _ownedTable = new bool[capacity];
            _nextFree = 1; // 0 = null actor
            _canGrow = canGrow;
            _growFactor = growFactor;
            _pool[0] = default;
        }

        private void Resize(uint capacity)
        {
            Array.Resize(ref _pool, (int)capacity);
            Array.Resize(ref _ownedTable, (int)capacity);
        }

        public uint New()
        {
            uint result = _nextFree;
            _ownedTable[result] = true;
            _nextFree = FindNextFree();
            if (result != 0)
                _used++;
            _pool[result] = default;
            return result;
        }

        public ref ActorData Get(uint i)
        {
            return ref _pool[i];
        }

        public void Recycle(uint i)
        {
            _ownedTable[i] = false;
            if (i != 0)
                _used--;
        }
        
        private uint FindNextFree()
        {
            uint total = 0;
            uint i = _nextFree;
            while (total < _ownedTable.Length)
            {
                if (_ownedTable[i] == false && i != 0)
                    return i;

                i++;
                if (i == _ownedTable.Length)
                    i = 0;
                total++;
            }

            if (_canGrow)
                Resize(Convert.ToUInt32(_ownedTable.Length * _growFactor));
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