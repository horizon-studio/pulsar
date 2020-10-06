using Ubersharp.Client.Rendering.OpenGL;

namespace Pulsar.Graphics.OpenGL
{
    public class GLFramebuffer : GPUResource<uint>, GPUFramebuffer
    {
        public GLFramebuffer(uint h) : base(h)
        {
        }

        public void AttachTexture(GPUTexture texture, int slot)
        {
            Gl.glNamedFramebufferTexture(_handle, slot, ((GLTexture)texture).GetHandle(), 0);
        }
    }
}