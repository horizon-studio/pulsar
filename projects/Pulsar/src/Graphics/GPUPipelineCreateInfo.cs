namespace Pulsar.Graphics
{
    public struct GPUPipelineCreateInfo
    {
        public GPUShader VertexShader;
        public GPUShader FragmentShader;
        public GPUShader GeometryShader;
        public GPUPipelineFormat Format;
    }
}