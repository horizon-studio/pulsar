using Pulsar.Contexts.GlfwSharp.Binding;

namespace Pulsar.Graphics
{
    public class Window : NativeWindow
    {
        public Window(uint width, uint height, string title) : base((int)width, (int)height, title)
        {
            
        }
    }
}