using Ubersharp.Client.Rendering;

namespace Pulsar.Graphics
{
    public struct GPUPipelineFormat
    {
        /// <summary>
        /// Define used buffers.
        /// </summary>
        public GPUBufferFormat[] BufferFormats;

        /// <summary>
        /// Define each vertex attributes.
        /// </summary>
        public GPUVertexFormat[] VertexFormats;

        /// <summary>
        /// TRUE if the pipeline will use indices buffer.
        /// FALSE otherwise.
        /// </summary>
        public bool UseIndices;

        /// <summary>
        /// The size of the indices buffer.
        /// </summary>
        public uint IndicesSize;

        /// <summary>
        /// Flags for the indices buffer creation.
        /// </summary>
        public uint IndicesFlags;
    }
}