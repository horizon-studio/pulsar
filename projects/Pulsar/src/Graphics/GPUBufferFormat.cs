namespace Pulsar.Graphics
{
    public struct GPUBufferFormat
    {
        /// <summary>
        /// The index of the vertex buffer binding point to which to bind the buffer.
        /// </summary>
        public uint BufferIndex;
        
        /// <summary>
        /// The offset of the first element of the buffer.
        /// </summary>
        public uint Offset;
        
        /// <summary>
        /// The distance between elements within the buffer.
        /// </summary>
        public uint Stride;

        /// <summary>
        /// Buffer size.
        /// </summary>
        public uint Size;
    }
}