using System.Collections.Generic;

namespace Pulsar.Graphics
{
    public class GPUEngine
    {
        private GPUContext _context;
        private List<GPURenderTask> _tasks = new List<GPURenderTask>();
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
    }
}