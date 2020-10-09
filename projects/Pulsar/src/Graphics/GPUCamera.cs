using Pulsar.Maths;

namespace Pulsar.Graphics
{
    public interface GPUCamera
    {
        Matrix GetProjection();
        Matrix GetView();
    }
}