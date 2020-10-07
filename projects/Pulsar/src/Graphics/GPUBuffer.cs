using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public class GPUBuffer : GPUResource
    {
        
        public GPUBuffer(uint h) : base(h)
        {
        }

        public void Upload(BufferData data, UsageStorage usage)
        {
            Gl.glNamedBufferData(_handle, data.GetUsedSize(), data.GetPtr(), (int)usage);
        }
    }
}