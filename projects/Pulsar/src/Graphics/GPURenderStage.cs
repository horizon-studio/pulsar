using System.Collections.Generic;

namespace Pulsar.Graphics
{
    public abstract class GPURenderStage
    {
        private List<GPURenderTask> _tasks = new List<GPURenderTask>();
        public abstract void OnRenderStart(GPUEngine engine);
        public abstract void OnRenderEnd(GPUEngine engine);

        public void OnRender(GPUEngine engine)
        {
            foreach (var gpuRenderTask in _tasks)
            {
                gpuRenderTask.OnRender(engine, this);
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