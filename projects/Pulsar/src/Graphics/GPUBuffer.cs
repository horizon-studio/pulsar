namespace Pulsar.Graphics
{
    public interface GPUBuffer
    {
        public void Upload(BufferData data, UsageStorage usage);
    }
}