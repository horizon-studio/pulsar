using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public class GPUFramebuffer : GPUResource
    {
        public enum Attachments : int
        {
            Color0 = Gl.GL_COLOR_ATTACHMENT0,
            Color1 = Gl.GL_COLOR_ATTACHMENT1,
            Color2 = Gl.GL_COLOR_ATTACHMENT2,
            Color3 = Gl.GL_COLOR_ATTACHMENT3,
            Color4 = Gl.GL_COLOR_ATTACHMENT4,
            Color5 = Gl.GL_COLOR_ATTACHMENT5,
            Color6 = Gl.GL_COLOR_ATTACHMENT6,
            Color7 = Gl.GL_COLOR_ATTACHMENT7,
            Color8 = Gl.GL_COLOR_ATTACHMENT8,
            Color9 = Gl.GL_COLOR_ATTACHMENT9,
            Color10 = Gl.GL_COLOR_ATTACHMENT10,
            Color11 = Gl.GL_COLOR_ATTACHMENT11,
            Depth = Gl.GL_DEPTH_ATTACHMENT,
            Stencil = Gl.GL_STENCIL_ATTACHMENT,
            DepthStencil = Gl.GL_DEPTH_STENCIL_ATTACHMENT
        }

        public enum Masks : uint
        {
            Color = Gl.GL_COLOR_BUFFER_BIT,
            Depth = Gl.GL_DEPTH_BUFFER_BIT,
            Stencil = Gl.GL_STENCIL_BUFFER_BIT,
            ColorDepth = Color | Depth,
            ColorStencil = Color | Stencil,
            DepthStencil = Depth | Stencil
        }

        public enum Filters : int
        {
            Nearest = Gl.GL_NEAREST,
            Linear = Gl.GL_LINEAR
        }
        
        public GPUFramebuffer(uint h) : base(h)
        {
        }
        
        public void AttachTexture(GPUTexture texture, Attachments att, int level)
        {
            Gl.glNamedFramebufferTexture(_handle, (int)att, texture, level);
        }

        public void AttachTexture(GPUTexture texture, Attachments att)
        {
            AttachTexture(texture, att, 0);
        }
        public void CopyTo(GPUFramebuffer dst, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0,
            int dstX1, int dstY1, Masks mask, Filters filter)
        {
            Gl.glBlitNamedFramebuffer(_handle, dst, srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, (uint)mask, (int)filter);
        }

        public void Bind()
        {
            Gl.glBindFramebuffer(Gl.GL_FRAMEBUFFER, _handle);
        }
    }
}