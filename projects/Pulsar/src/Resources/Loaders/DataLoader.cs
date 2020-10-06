namespace Pulsar.Resources
{
    public class DataLoader : IResourceLoader
    {
        public Resource Load(string path, byte[] data)
        {
            return new DataResource(path, data);
        }
    }
}