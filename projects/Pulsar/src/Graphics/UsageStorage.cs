using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public enum UsageStorage : int
    {
        StreamDraw = Gl.GL_STREAM_DRAW, 
        StreamRead = Gl.GL_STREAM_READ, 
        StreamCopy = Gl.GL_STREAM_COPY, 
        StaticDraw = Gl.GL_STATIC_DRAW, 
        StaticRead = Gl.GL_STATIC_READ, 
        StaticCopy = Gl.GL_STATIC_COPY, 
        DynamicDraw = Gl.GL_DYNAMIC_DRAW, 
        DynamicRead = Gl.GL_DYNAMIC_READ,
        DynamicCopy = Gl.GL_DYNAMIC_COPY
    }
}