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
            Field = 30,
            Menu = 59
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
                while (Memory.IsTextBox())
                    WaitOneFrame();
                return;
            }
            if (entity == Entity.Menu)
            {
                while (Memory.IsMenu())
                    WaitOneFrame();
                return;
            }
        }

        static public void HoldDown()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
            Thread.Sleep(16);
        }
        static public void HoldLeft()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_A);
            Thread.Sleep(16);
        }
        static public void HoldUp()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
            Thread.Sleep(16);
        }
        static public void HoldRight()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_D);
            Thread.Sleep(16);
        }

        static public void ReleaseDown()
        {
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);
            Thread.Sleep(16);
        }
        static public void ReleaseLeft()
        {
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_A);
            Thread.Sleep(16);
        }
        static public void ReleaseUp()
        {
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W);
            Thread.Sleep(16);
        }
        static public void ReleaseRight()
        {
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_D);
            Thread.Sleep(16);
        }

        static public void PressStart()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_H);
            WaitOneFrame();
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_H);
            WaitOneFrame();
        }

        static public void PressX()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_K);
            WaitOneFrame();
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_K);
            WaitOneFrame();
        }

        static public void PressDown()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
            WaitOneFrame();
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);
            WaitOneFrame();
        }

        static public void PressUp()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
            WaitOneFrame();
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W);
            WaitOneFrame();
        }

        static public void HoldX()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_K);
        }

        static public void ReleaseX()
        {
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_K);
        }

        static public void PressTriangle()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_I);
            WaitOneFrame();
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_I);
            WaitOneFrame();
        }

        static public void PressCircle()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_L);
            WaitOneFrame();
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_L);
            WaitOneFrame();
        }

        static public void HoldTriangle()
        {
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_I);
        }

        static public void ReleaseTriangle()
        {
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_I);
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
