namespace Pulsar.Resources
{
    public interface IResourceLoader
    {
        public Resource Load(string path, byte[] data);
    }
}