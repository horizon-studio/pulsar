using System;

namespace Pulsar.Graphics
{
    public class GPUPipelineFormat : GPUResource
    {
        public GPUPipelineFormat(uint h, GPUBufferFormat[] bufferFormats, GPUVertexFormat[] vertexFormats) : base(h)
        {
            BufferFormats = bufferFormats;
            VertexFormats = vertexFormats;
        }
        
        /// <summary>
        /// Define used buffers.
        /// </summary>
        private GPUBufferFormat[] BufferFormats;

        /// <summary>
        /// Define each vertex attributes.
        /// </summary>
        private GPUVertexFormat[] VertexFormats;

        public GPUBufferFormat[] GetBufferFormats()
        {
            return BufferFormats;
        }

        public GPUVertexFormat[] GetVertexFormats()
        {
            return VertexFormats;
        }
    }
}