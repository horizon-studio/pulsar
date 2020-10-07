using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public enum ShaderStages : int
    {
        Vertex = Gl.GL_VERTEX_SHADER,
        Fragment = Gl.GL_FRAGMENT_SHADER,
        Geometry = Gl.GL_GEOMETRY_SHADER,
        Compute = Gl.GL_COMPUTE_SHADER,
        TessellationControl = Gl.GL_TESS_CONTROL_SHADER,
        TessellationEvaluation = Gl.GL_TESS_EVALUATION_SHADER
    }
}