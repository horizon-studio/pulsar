using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public enum BufferMasks : uint
    {
        Color = Gl.GL_COLOR_BUFFER_BIT,
        Depth = Gl.GL_DEPTH_BUFFER_BIT,
        Stencil = Gl.GL_STENCIL_BUFFER_BIT,
        ColorDepth = Color | Depth,
        ColorStencil = Color | Stencil,
        DepthStencil = Depth | Stencil
    }
}