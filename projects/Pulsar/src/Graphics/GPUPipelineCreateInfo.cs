namespace Pulsar.Graphics
{
    public struct GPUPipelineCreateInfo
    {
        public GPUShaderProgram ShaderProgram;
        public GPUPipelineFormat Format;
        public bool UseIndices;
        public uint IndicesSize;
        public uint IndicesFlags;
    }
}