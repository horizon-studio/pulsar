using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics.DrawCommands
{
    public class DrawArraysCommand : GPUDrawCommand
    {
        public PrimitiveType Type;
        public uint VertexCount;
        public uint FirstVertex;
        
        public void Draw()
        {
            Gl.glDrawArrays((int)Type, (int)FirstVertex, (int)VertexCount);
        }
    }
}