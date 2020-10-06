using Pulsar.Maths;

namespace Pulsar.Graphics
{
    public interface GPUPipeline
    {
        void SetUniform(string name, int value);
        void SetUniform(string name, float value);
        void SetUniform(string name, Vector2 value);
        void UpdateIndices(uint destOffset, byte[] data, uint length);
        void UpdateData(uint indexBuffer, uint destOffset, byte[] data, uint length);
        void Draw(GPUDrawCommand command);
        void SetFramebuffer(GPUFramebuffer framebuffer);
    }
}