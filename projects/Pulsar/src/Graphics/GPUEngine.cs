using System.Collections.Generic;

namespace Pulsar.Graphics
{
    public class GPUEngine
    {
        private GPUContext _context;
        private List<GPURenderStage> _stages = new List<GPURenderStage>();

        public GPUEngine()
        {
            _context = new GPUContext();    
        }
        
        public void Render()
        {
            foreach (var gpuRenderStage in _stages)
            {
                gpuRenderStage.OnRenderStart(_context);
                gpuRenderStage.OnRender(_context);
            }
        }

        public void AddStage(GPURenderStage stage)
        {
            _stages.Add(stage);
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