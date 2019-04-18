using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class Input
    {
        public static Vector2 cameraOffset;
        public static bool IsKeyDown(Keys key)
        {
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyDown(key);
        }
        public static bool IsKeyUp(Keys key)
        {
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyUp(key);
        }
        public static bool IsMouseKeyDown(int btn)
        {
            MouseState state = Mouse.GetState();
            switch (btn)
            {
                case 0:
                    return state.LeftButton == ButtonState.Pressed;
                case 1:
                    return state.MiddleButton == ButtonState.Pressed;
                case 2:
                    return state.RightButton == ButtonState.Pressed;
            }
            return false;
        }
        public static bool IsMouseKeyUp(int btn)
        {
            MouseState state = Mouse.GetState();
            switch (btn)
            {
                case 0:
                    return state.LeftButton == ButtonState.Released;
                case 1:
                    return state.MiddleButton == ButtonState.Released;
                case 2:
                    return state.RightButton == ButtonState.Released;
            }
            return false;
        }
        public static Vector2 GetMousePosition()
        {
            MouseState mouse = Mouse.GetState();
            return mouse.Position.ToVector2();
        }
        public static Vector2 ToWorldPosition(Vector2 pos)
        {
            return pos - cameraOffset;
        }
    }
}
