using System;

namespace Pulsar.Graphics
{
    public class GPUShader : GPUResource
    {
        private ShaderStages _stage;
        
        public GPUShader(uint h, ShaderStages stage) : base(h)
        {
            _stage = stage;
        }

        public ShaderStages GetStage()
        {
            return _stage;
        }
    }
}