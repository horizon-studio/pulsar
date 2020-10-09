namespace Pulsar.Graphics
{
    public abstract class GPURenderTask
    {
        public abstract void Init(GPUEngine engine, GPURenderStage stage);
        public abstract void OnRender(GPUEngine engine, GPURenderStage stage, float delta);
    }
}