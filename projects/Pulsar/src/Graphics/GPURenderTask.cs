namespace Pulsar.Graphics
{
    public abstract class GPURenderTask
    {
        public abstract void Init(GPUContext context, GPURenderStage stage);
        public abstract void OnRender(GPUContext context, GPURenderStage stage);
    }
}