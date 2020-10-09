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

        public void AddTask(GPURenderTask task)
        {
            _tasks.Add(task);
        }

        public void RemoveTask(GPURenderTask task)
        {
            _tasks.Remove(task);
        }

        public void ClearTasks()
        {
            _tasks.Clear();
        }
    }
}