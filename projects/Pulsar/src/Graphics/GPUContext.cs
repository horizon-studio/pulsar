namespace Pulsar.Graphics
{
    public interface GPUContext
    {
        public void Initialize();
        public GPUShader CreateShader(string code, ShaderStages stage);
        public void DeleteShader(GPUShader shader);
        public GPUPipeline CreatePipeline(GPUPipelineCreateInfo info);
        public void DeletePipeline(GPUPipeline pipe);
        public GPUBuffer CreateBuffer();
        public void DeleteBuffer(GPUBuffer buffer);
        public GPUTexture CreateTexture();
        public void DeleteTexture(GPUTexture texture);
        public GPUFramebuffer CreateFramebuffer();
        public void DeleteFramebuffer(GPUFramebuffer framebuffer);
        public GPUFramebuffer GetDefaultFramebuffer();
    }
}