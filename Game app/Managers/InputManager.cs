using EZInput;

namespace Game_app.Managers
{
    internal class InputManager
    {
        public bool MoveLeft()
        {
            return Keyboard.IsKeyPressed(Key.LeftArrow);
        }

        public bool MoveRight()
        {
            return Keyboard.IsKeyPressed(Key.RightArrow);
        }

        public bool Jump()
        {
            return Keyboard.IsKeyPressed(Key.UpArrow);
        }
        public bool Fire()
        {
            return Keyboard.IsKeyPressed(Key.Space);
        }
    }
}

