using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WindowsInput;

namespace FF8TAS
{
    static class GameInput
    {
        static public InputSimulator isim;
        static private int fps = (int)State.Menu;
        static GameInput()
        {
            isim = new InputSimulator();
        }

        static public void ChangeFps(State state)
        {
            fps = (int)state;
        }
        
        public enum State
        {
            Battle = 15,
            Field = 59,
            Menu = 60
        }

        public enum Entity
        {
            TextBox,
            Menu,
        }

        public enum Directions
        {
            Up = WindowsInput.Native.VirtualKeyCode.VK_W,
            Left = WindowsInput.Native.VirtualKeyCode.VK_A,
            Down = WindowsInput.Native.VirtualKeyCode.VK_S,
            Right = WindowsInput.Native.VirtualKeyCode.VK_D
        }

        static public void WaitFor(Entity entity)
        {
            if (entity == Entity.TextBox)
            {
                while (Player.IsTextBox())
                    WaitOneFrame();
                return;
            }
            if (entity == Entity.Menu)
            {
                while (Player.IsMenu())
                    WaitOneFrame();
                return;
            }
        }

        static public void WaitOneFrame()
        {
            float time = 1 / (float)fps * 1000;
            Thread.Sleep((int)Math.Round(time));
        }

        static private void WaitHalfFrame()
        {
            float time = 1 / (float)fps * 500;
            Thread.Sleep((int)Math.Round(time));
        }

        static public void KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode key)
        {
            isim.Keyboard.KeyDown(key);
            WaitOneFrame();
            isim.Keyboard.KeyUp(key);
            WaitOneFrame();
        }

        static public void CustomPress(WindowsInput.Native.VirtualKeyCode key, int time)
        {
            isim.Keyboard.KeyDown(key);
            Thread.Sleep(time);
            isim.Keyboard.KeyUp(key);
            Thread.Sleep(time);
        }

        static public void MoveTo(int x, int y)
        {

        }
    }
}
