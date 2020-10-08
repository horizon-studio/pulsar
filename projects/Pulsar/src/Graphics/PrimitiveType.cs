using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public enum PrimitiveType
    {
        Triangles = Gl.GL_TRIANGLES,
        TrianglesStrip,
        TriangleFan,
        Quads,
        QuadsStrip,
        Patches,
        Lines,
        LineStrip,
        LineLoop,
        Points
    }
}