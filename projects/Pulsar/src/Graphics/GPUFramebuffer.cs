namespace Pulsar.Graphics
{
    public interface GPUFramebuffer
    {
        public void AttachTexture(GPUTexture texture, int slot);
    }
}