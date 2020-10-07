using System;
using Pulsar.Contexts.GlfwSharp.Binding;
using Pulsar.Graphics;

namespace Pulsar.Application
{
    public abstract class Application : IDisposable
    {
        protected ApplicationHost AppHost;
        protected Window Window;
        protected GPUContext _gpuContext;
        private uint _swapInterval = 0;
        public uint SwapInterval
        {
            get
            {
                return _swapInterval;
            }
            set
            {
                _swapInterval = value;
                Glfw.SwapInterval((int)_swapInterval);
            }
        }
        
        public bool IsRunning { get; set; } = true;

        public abstract void Init();

        public abstract void Update(float delta);

        public abstract void Render(float delta);

        public abstract void Stop();

        public abstract void Dispose();
        
        public void SetHost(ApplicationHost host)
        {
            AppHost = host;
        }

        public void SetWindow(Window w)
        {
            Window = w;
        }

        public void SetGPUContext(GPUContext c)
        {
            _gpuContext = c;
        }
    }
}