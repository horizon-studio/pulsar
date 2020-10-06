namespace Pulsar.Graphics
{
    public class GPUResource<T>
    {
        protected T _handle;

        public GPUResource(T h)
        {
            _handle = h;
        }

        public T GetHandle()
        {
            return _handle;
        }
    }
}