namespace Pulsar.Graphics
{
    public class GPUResource
    {
        protected uint _handle;

        public GPUResource(uint h)
        {
            _handle = h;
        }

        public uint GetHandle()
        {
            return _handle;
        }
        
        public static implicit operator uint(GPUResource res) => res._handle;
    }
}