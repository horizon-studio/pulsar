namespace Pulsar.Graphics.OpenGL
{
    public class GLBuffer : GPUResource<uint>, GPUBuffer
    {
        public GLBuffer(uint h) : base(h)
        {
        }

        public void Upload(BufferData data, UsageStorage usage)
        {
            Gl.glNamedBufferData(_handle, data.GetUsedSize(), data.GetPtr(), (int)usage);
        }
    }
}