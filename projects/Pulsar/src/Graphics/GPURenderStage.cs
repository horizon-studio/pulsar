using System.Collections.Generic;

namespace Pulsar.Graphics
{
    public abstract class GPURenderStage
    {
        private List<GPURenderTask> _tasks = new List<GPURenderTask>();
        public abstract void OnRenderStart(GPUContext context);
        public abstract void OnRenderEnd(GPUContext context);

        public void OnRender(GPUContext context)
        {
            foreach (var gpuRenderTask in _tasks)
            {
                gpuRenderTask.OnRender(context, this);
            }
        }
    }
}