namespace Pulsar.Resources
{
    public class DataResource : Resource
    {
        private byte[] _data;
        
        public DataResource(string path, byte[] data) : base(path)
        {
            _data = data;
        }

        public byte[] GetData()
        {
            return _data;
        }
    }
}