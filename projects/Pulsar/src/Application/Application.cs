using System;

namespace Pulsar.Application
{
    public abstract class Application : IDisposable
    {
        protected ApplicationHost AppHost;
        public bool IsRunning { get; protected set; } = true;

        public abstract void Init();

        public abstract void Update(float delta);

        public abstract void Render(float delta);

        public abstract void Stop();

        public abstract void Dispose();
        
        public void SetHost(ApplicationHost host)
        {
            AppHost = host;
        }
    }
}