namespace Pulsar.Graphics
{
    public struct GPUVertexFormat
    {
        /// <summary>
        /// The binding index to use.
        /// </summary>
        public uint BufferIndex;
        
        /// <summary>
        /// The number of values per vertex that are stored in the array.
        /// </summary>
        public uint Size;
        
        /// <summary>
        /// The type of the data stored in the array.
        /// </summary>
        public int Type;
        
        /// <summary>
        /// TRUE if the parameter represents a normalized integer (type must be an integer type).
        /// FALSE otherwise.
        /// </summary>
        public bool Normalized;
        
        /// <summary>
        /// The offset, measured in basic machine units of the first element relative
        /// to the start of the vertex buffer binding this attribute fetches from.
        /// </summary>
        public uint RelativeOffset;

        public uint Location;
    }
}