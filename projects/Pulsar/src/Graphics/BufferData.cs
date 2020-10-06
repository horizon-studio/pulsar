using System;
using System.Runtime.InteropServices;

namespace Pulsar.Graphics
{
    public class BufferData : IDisposable
    {
        private uint _pos = 0;
        private byte[] _data;
        private GCHandle _gcHandle;
        
        public BufferData()
        {
            _data = new byte[1024];
            _gcHandle = GCHandle.Alloc(_data);
        }

        public BufferData(int size)
        {
            _data = new byte[size];
        }
        
        public void Clear()
        {
            _pos = 0;
        }

        public void Write(byte b)
        {
            _data[_pos] = b;
            _pos += 1;
        }

        public void Write(sbyte sb)
        {
            BitConverter.GetBytes(sb).CopyTo(_data, _pos);
            _pos += 1;
        }
        
        public void Write(int i)
        {
            BitConverter.GetBytes(i).CopyTo(_data, _pos);
            _pos += 4;
        }

        public void Write(uint ui)
        {
            BitConverter.GetBytes(ui).CopyTo(_data, _pos);
            _pos += 4;
        }
        
        public void Write(float f)
        {
            BitConverter.GetBytes(f).CopyTo(_data, _pos);
            _pos += 4;
        }

        public void Write(short s)
        {
            BitConverter.GetBytes(s).CopyTo(_data, _pos);
            _pos += 2;
        }

        public void Write(ushort us)
        {
            BitConverter.GetBytes(us).CopyTo(_data, _pos);
            _pos += 2;
        }

        public void Write(double d)
        {
            BitConverter.GetBytes(d).CopyTo(_data, _pos);
            _pos += 8;
        }

        public void Write(long l)
        {
            BitConverter.GetBytes(l).CopyTo(_data, _pos);
            _pos += 8;
        }

        public void Write(ulong ul)
        {
            BitConverter.GetBytes(ul).CopyTo(_data, _pos);
            _pos += 8;
        }

        public void Write(float x, float y, float z)
        {
            Write(x);
            Write(y);
            Write(z);
        }
        
        public byte[] GetData()
        {
            return _data;
        }

        public IntPtr GetPtr()
        {
            return _gcHandle.AddrOfPinnedObject();
        }

        public uint GetUsedSize()
        {
            return _pos;
        }

        public uint GetTotalSize()
        {
            return (uint) _data.Length;
        }

        public void Dispose()
        {
            _gcHandle.Free();
        }
    }
}