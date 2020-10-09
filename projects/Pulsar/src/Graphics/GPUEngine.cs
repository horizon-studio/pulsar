using System.Collections.Generic;

namespace Pulsar.Graphics
{
    public partial class GPUEngine
    {
        private List<GPURenderStage> _stages = new List<GPURenderStage>();

        public GPUEngine()
        {
               
        }
        
        public void Render(float delta)
        {
            foreach (var gpuRenderStage in _stages)
            {
                gpuRenderStage.OnRenderStart(this);
                gpuRenderStage.OnRender(this, delta);
                gpuRenderStage.OnRenderEnd(this);
            }
        }

        public void AddStage(GPURenderStage stage)
        {
            _stages.Add(stage);
            stage.OnInit(this);
        }

        public void RemoveStage(GPURenderStage stage)
        {
            _stages.Remove(stage);
        }

        public void ClearStages()
        {
            _stages.Clear();
        }
    }
}