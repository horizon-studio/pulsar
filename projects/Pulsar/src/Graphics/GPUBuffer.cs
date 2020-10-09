using System.Collections.Generic;
using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public class GPUBuffer : GPUResource
    {
        
        protected uint _size;
        
        public GPUBuffer(uint h, uint size) : base(h)
        {
            _size = size;
        }

        public virtual void Upload(BufferData data)
        {
            Upload(data, 0);
        }

        public virtual void Upload(BufferData data, uint offset)
        {
            Gl.glNamedBufferSubData(_handle, offset, data.GetUsedSize(), data.GetPtr());
        }
    }
}