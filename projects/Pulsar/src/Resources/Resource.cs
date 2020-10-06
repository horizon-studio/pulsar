namespace Pulsar.Resources
{
    public abstract class Resource
    {
        private string _path;

        protected Resource(string path)
        {
            _path = path;
        }

        public string GetPath()
        {
            return _path;
        }
    }
}