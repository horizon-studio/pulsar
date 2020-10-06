namespace Pulsar.Inputs
{
    public interface Keyboard
    {
        public delegate void OnKeyHandler(int key, int scancode, int action, int mods);

        public event OnKeyHandler OnKeyEvent;

        public bool IsKeyPressed(int key);
        public bool IsKeyReleased(int key);
    }
}