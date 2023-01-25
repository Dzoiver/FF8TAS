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

        public enum Controls
        {
            Up = WindowsInput.Native.VirtualKeyCode.VK_W,
            Left = WindowsInput.Native.VirtualKeyCode.VK_A,
            Down = WindowsInput.Native.VirtualKeyCode.VK_S,
            Right = WindowsInput.Native.VirtualKeyCode.VK_D,
            Triangle = WindowsInput.Native.VirtualKeyCode.VK_I,
            Square = WindowsInput.Native.VirtualKeyCode.VK_J,
            Circle = WindowsInput.Native.VirtualKeyCode.VK_L,
            X = WindowsInput.Native.VirtualKeyCode.VK_K,
            Start = WindowsInput.Native.VirtualKeyCode.VK_H,
            Select = WindowsInput.Native.VirtualKeyCode.VK_G,
            L1 = WindowsInput.Native.VirtualKeyCode.VK_Q,
            L2 = WindowsInput.Native.VirtualKeyCode.VK_U,
            R1 = WindowsInput.Native.VirtualKeyCode.VK_E,
            R2 = WindowsInput.Native.VirtualKeyCode.VK_O,
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

            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Down);
            Thread.Sleep(16);
        }
        static public void HoldLeft()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Left);
            Thread.Sleep(16);
        }
        static public void HoldUp()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Up);
            WaitOneFrame();
        }
        static public void HoldRight()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Right);
            WaitOneFrame();
        }

        static public void ReleaseDown()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Down);
            Thread.Sleep(16);
        }
        static public void ReleaseLeft()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Left);
            Thread.Sleep(16);
        }
        static public void ReleaseUp()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Up);
            Thread.Sleep(16);
        }
        static public void ReleaseRight(int wait = 0)
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Right);
            WaitOneFrame(wait);
        }

        static public void PressStart()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Start);
            WaitOneFrame();
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Start);
            WaitOneFrame();
        }

        static public void PressX(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.X);
            WaitOneFrame(wait);
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.X);
            WaitOneFrame(wait);
        }

        static public void PressRight()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Right);
            WaitOneFrame();
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Right);
            WaitOneFrame();
        }
        
        static public void PressLeft()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Left);
            WaitOneFrame();
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Left);
            WaitOneFrame();
        }

        static public void PressDown(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Down);
            WaitOneFrame(wait);
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Down);
            WaitOneFrame(wait);
        }

        static public void PressUp(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Up);
            WaitOneFrame(wait);
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Up);
            WaitOneFrame(wait);
        }

        static public void HoldX()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.X);
        }

        static public void ReleaseX()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.X);
        }

        static public void PressTriangle(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Triangle);
            WaitOneFrame(wait);
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Triangle);
            WaitOneFrame(wait);
        }

        static public void PressCircle()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Circle);
            WaitOneFrame();
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Circle);
            WaitOneFrame();
        }

        static public void PressR1()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.R1);
            WaitOneFrame();
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.R1);
            WaitOneFrame();
        }

        static public void HoldR1()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.R1);
        }

        static public void ReleaseR1()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.R1);
        }

        static public void HoldL1()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.L1);
        }

        static public void HoldR2()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.R2);
        }

        static public void ReleaseR2()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.R2);
        }

        static public void HoldL2()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.L2);
        }

        static public void ReleaseL2()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.L2);
        }

        static public void ReleaseL1()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.L1);
        }

        static public void HoldSquare()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Square);
        }

        static public void ReleaseSquare()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Square);
        }

        static public void HoldTriangle()
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Triangle);
        }

        static public void ReleaseTriangle()
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Triangle);
        }

        static public void WaitOneFrame(int wait = 0)
        {
            if (wait != 0)
            {
                Thread.Sleep(wait);
                return;
            }
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

        public static void MoveTo(int x, int y) // Orientation is different on each field so it doesn't really work properly
        {
            int currentX = Memory.GetFieldX(); // 0
            int currentY = Memory.GetFieldY(); // 0
            int fieldPollTime = 32;
            int distanceX = x - currentX; // 510
            int distanceY = y - currentY; // 1200
            double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY); // ~3200
            byte cameraByte = Memory.GetCameraAngle();

            float deltaCamera = cameraByte - 128;
            float deltaCameraDegrees = 360f / 256f * deltaCamera; // -16.8

            float someAngle = 45f + deltaCameraDegrees;

            Console.WriteLine("distanceX: " + distanceX);
            Console.WriteLine("distanceY: " + distanceY);

            int shortestCoordinate;
            if (Math.Abs(distanceX) < Math.Abs(distanceY)) // 1200x 2940y
            {
                shortestCoordinate = distanceX;
            }
            else
            {
                shortestCoordinate = distanceY;
            }

            // double firstTriangleKatet = Math.Sqrt(shortestCoordinate * shortestCoordinate + shortestCoordinate * shortestCoordinate);

            float wtfAngle = 180f - (90f + 45f + Math.Abs(deltaCameraDegrees));

            Console.WriteLine("deltaCameraDegrees: " + deltaCameraDegrees);
            Console.WriteLine("wtfAngle: " + wtfAngle);

            double firstTriangleKatet = (distance * Math.Sin((wtfAngle * Math.PI) / 180)) / Math.Sin((135 * Math.PI) / 180); // 2121

            double fixedCoord = (firstTriangleKatet * Math.Sin((deltaCameraDegrees * Math.PI) / 180)) / Math.Sin((90 * Math.PI) / 180); // 584

            if (fixedCoord != 0)
                x = (int)fixedCoord;

            Console.WriteLine("fixedCoord: " + fixedCoord);

            Console.WriteLine("someAngle: " + someAngle);

            if (currentX > x)
                HoldLeft();
            else if (currentX < x)
                HoldRight();


            if (currentY > y)
                HoldDown();
            else if (currentY < y)
                HoldUp();

            // Check which to release first. Shortest coordinate first

            if (Math.Abs(distanceX) < Math.Abs(distanceY)) // yes
            {
                WaitForX(currentX, x, fieldPollTime);
                WaitForY(currentY, y, fieldPollTime);
            }
            else
            {
                WaitForY(currentY, y, fieldPollTime);
                WaitForX(currentX, x, fieldPollTime);
            }
        }

        private static void WaitForX(int currentX, int x, int fieldPollTime)
        {
            if (currentX < x) // -500 < 0
            {
                while (Memory.GetFieldX() < x)
                {
                    Thread.Sleep(fieldPollTime);
                }
                ReleaseRight();
            }
            else
            {
                while (Memory.GetFieldX() > x)
                {
                    Thread.Sleep(fieldPollTime);
                }
                ReleaseLeft();
            }
        }

        private static void WaitForY(int currentY, int y, int fieldPollTime)
        {
            if (currentY < y)
            {
                while (Memory.GetFieldY() < y)
                {
                    Thread.Sleep(fieldPollTime);
                }
                ReleaseUp();
            }
            else
            {
                while (Memory.GetFieldY() > y) // -500 < 10 OR 500 > 10
                {
                    Thread.Sleep(fieldPollTime);
                }
                ReleaseDown();
            }
        }
    }
}
