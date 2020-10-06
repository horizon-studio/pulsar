namespace Pulsar.Graphics.OpenGL
{
    public class GLShader : GPUResource<uint>, GPUShader
    {
        private ShaderStages _stage;
        
        public GLShader(uint h, ShaderStages stage) : base(h)
        {
            _stage = stage;
        }

        public ShaderStages GetStage()
        {
            return _stage;
        }
    }
}