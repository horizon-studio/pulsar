namespace Pulsar.Inputs.Glfw
{
    public class GLFWKeyboard : Keyboard
    {
        public event Keyboard.OnKeyHandler OnKeyEvent;

        public bool IsKeyPressed(int key)
        {
            return true;
        }

        public bool IsKeyReleased(int key)
        {
            return true;
        }
    }
}