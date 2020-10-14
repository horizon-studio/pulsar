using System;
using Pulsar.Contexts.GlfwSharp.Binding;
using Pulsar.Contexts.OpenGL;
using Pulsar.Graphics;

namespace Pulsar.Application
{
    public class ApplicationHost : IDisposable
    {
        private Application _application;
        private long _now;
        private long _lastUpdate;
        private int _updatesCount;
        private int _targetUpdate;
        private int _targetRender;
        private long _lastRender;
        private int _rendersCount;
        private int _updateInterval;
        private int _renderInterval;
        private long _lastSecond;

        private int _ups;
        private int _fps;

        private float _lastDelta;

        private Window _window;
        public ApplicationHost(Application app)
        {
            SetApp(app);
        }
        
        public void Start()
        {
            //TODO: use logger , erro handling
            Glfw.Init();
            Glfw.WindowHint(Hint.Visible, false);
            _window = new Window(800, 600, "");
            _window.MakeCurrent();
            Gl.Init(Glfw.GetProcAddress);
            _application.SetWindow(_window);
            _application.SetGPUEngine(new GPUEngine());
            _application.Init();
            _window.Visible = true;
            
            while (_application.IsRunning)
            {
                _now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                _lastDelta = _now - _lastUpdate;
                if ((_targetUpdate > 0 && _now - _lastUpdate > _updateInterval) || _targetUpdate == -1)
                {
                    _application.Update(_lastDelta);
                    _lastUpdate = _now;
                    _updatesCount++;
                } else if ((_targetRender > 0 && _now - _lastRender > _renderInterval) || _targetRender == -1) {
                    _application.Render(_lastDelta);
                    _application.GetEngine().Render(_lastDelta);
                    _window.SwapBuffers();
                    Glfw.PollEvents();
                    if (_window.IsClosing)
                        _application.IsRunning = false;
                    _lastRender = _now;
                    _rendersCount++;
                }
                
                if (_now - _lastSecond >= 1000)
                {
                    _ups = _updatesCount;
                    _fps = _rendersCount;
                    _updatesCount = 0;
                    _rendersCount = 0;
                    _lastSecond = _now;
                }
            }
            _application.Dispose();
        }

        public void Stop()
        {
            _application.Stop();
        }

        public void SetApp(Application app)
        {
            _application?.Stop();
            _application?.Dispose();
            _application = app;
            app.SetHost(this);
        }
        
        public void SetTargetUps(int ups)
        {
            _targetUpdate = ups;
            if (_targetUpdate > 0)
                _updateInterval = 1000 / _targetUpdate;
        }

        public void SetTargetFps(int fps)
        {
            _targetRender = fps;
            if (_targetRender > 0)
                _renderInterval = 1000 / _targetRender;
        }

        public int GetFps()
        {
            return _fps;
        }

        public int GetUps()
        {
            return _ups;
        }

        public float GetDelta()
        {
            return _lastDelta;
        }

        public void Dispose()
        {
            _application?.Dispose();
        }
    }
}