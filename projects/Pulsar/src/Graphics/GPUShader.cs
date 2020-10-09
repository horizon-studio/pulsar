using System;
using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public class GPUShader : GPUResource, IDisposable
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

        public void Dispose()
        {
            Gl.glDeleteShader(this);
        }
    }
}