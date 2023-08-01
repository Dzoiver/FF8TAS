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
        static private int cameraShifts;
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
            Menu = 59,
            WM = 30
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

        static public void HoldDown(int wait = 0)
        {

            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Down);
            WaitOneFrame(wait);
        }
        static public void HoldLeft(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Left);
            WaitOneFrame(wait);
        }
        static public void HoldUp(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Up);
        }
        static public void HoldRight(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Right);
            WaitOneFrame(wait);
        }

        static public void ReleaseDown(int wait = 0)
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Down);
            WaitOneFrame(wait);
        }
        static public void ReleaseLeft(int wait = 0)
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Left);
            WaitOneFrame(wait);
        }
        static public void ReleaseUp(int wait = 0)
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Up);
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
        }

        static public void PressUp(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Up);
            WaitOneFrame(wait);
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Up);
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
            WaitOneFrame();
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Triangle);
            //WaitOneFrame();
        }

        static public void PressCircle(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Circle);
            WaitOneFrame(wait);
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Circle);
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

        static public void HoldCircle(int wait = 0)
        {
            isim.Keyboard.KeyDown((WindowsInput.Native.VirtualKeyCode)Controls.Circle);
            // WaitOneFrame(wait);
        }

        static public void ReleaseCircle(int wait = 0)
        {
            isim.Keyboard.KeyUp((WindowsInput.Native.VirtualKeyCode)Controls.Circle);
            WaitOneFrame(wait);
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
            int distanceX = x - currentX; // 1200
            int distanceY = y - currentY; // 3000
            double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY); // ~3200
            byte cameraByte = Memory.GetCameraAngle();

            byte defaultCamera = 128;

            float deltaCamera = cameraByte - 128; // 93 (35) = 50 gradusov

            cameraShifts = (int)deltaCamera / 32;
            Console.WriteLine("cameraShifts: " + cameraShifts);

            int fakeX = x;
            int fakeY = y;

            if (cameraShifts == -1) // 96 --- ok = 3
            {
                fakeX = currentX + distanceX;
                fakeY = currentY - distanceY;

                //x: -1046
                //y: -23000
            }
            else if (cameraShifts == -2) // 64
            {
                fakeX = currentX - distanceY;
                fakeY = currentY + distanceX;
            }
            else if (cameraShifts == -3) // 32 --- ok
            {
                fakeX = currentX + distanceY;
                fakeY = currentY + distanceX;
            }
            else if (cameraShifts == -4 || cameraShifts == 4) // 0
            {
                fakeX = currentX - distanceX;
                fakeY = currentY - distanceY;
            }
            else if (cameraShifts == 3) // 224 --- ok
            {
                fakeX = currentX - distanceX;
                fakeY = currentY + distanceY;
            }
            else if (cameraShifts == 2) // 192 --- ok
            {
                fakeX = currentX + distanceY;
                fakeY = currentY - distanceX;
            }
            else if (cameraShifts == 1) // 160 --- ok
            {
                fakeX = currentX - distanceY;
                fakeY = currentY - distanceX;
            }

            Console.WriteLine("x: " + x);
            Console.WriteLine("y: " + y);
            Console.WriteLine("fakeX: " + fakeX);
            Console.WriteLine("fakeY: " + fakeY);

            //while (true)
            //{
            //    if (Math.Abs(deltaCamera) > 32)
            //    {
            //        cameraByte - 32;
            //    }
            //}

            float deltaCameraDegrees = 360f / 256f * deltaCamera; // -16.8

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




            // DIMA
            //float wtfAngle = 180f - (90f + 45f + Math.Abs(deltaCameraDegrees));

            //Console.WriteLine("deltaCameraDegrees: " + deltaCameraDegrees);
            //Console.WriteLine("wtfAngle: " + wtfAngle);

            //double firstTriangleKatet = (distance * Math.Sin((wtfAngle * Math.PI) / 180)) / Math.Sin((135 * Math.PI) / 180); // 2121  // 28

            //double fixedCoord = (firstTriangleKatet * Math.Sin((deltaCameraDegrees * Math.PI) / 180)) / Math.Sin((90 * Math.PI) / 180); // 584    // 16

            //if (fixedCoord != 0)
            //    x = (int)fixedCoord;
            //

            // Seifer


            //float wtfAngle = 180f - (135f + Math.Abs(deltaCameraDegrees));
            //double firstTriangleKatet = (distance * Math.Sin((wtfAngle * Math.PI) / 180)) / Math.Sin((135 * Math.PI) / 180); // 2121

            //double fixedCoord = (firstTriangleKatet * Math.Sin((wtfAngle * Math.PI) / 180)) / Math.Sin((90 * Math.PI) / 180); // 584
            //x = (int)fixedCoord;

            //

            double AP = Math.Sqrt(distanceX * distanceX * 2);
            Console.WriteLine("x: " + x);

            double DP = Math.Abs(distanceY - distanceX);

            double DAP = Math.Acos((distance * distance + AP * AP - DP * DP) / (2 * distance * AP)) * 180 / Math.PI; // 22
            //double DAP = (distance * distance + AP * AP - DP * DP) / (2 * distance * AP);

            double DAM = DAP - deltaCameraDegrees; // 5

            Console.WriteLine("distance: " + distance);
            Console.WriteLine("distanceX: " + distanceX);
            Console.WriteLine("distanceY: " + distanceY);
            Console.WriteLine("deltaCameraDegrees: " + deltaCameraDegrees);
            Console.WriteLine("AP: " + AP);
            Console.WriteLine("DP: " + DP);
            Console.WriteLine("DAP: " + DAP);
            Console.WriteLine("DAM: " + DAM);
            // Console.WriteLine("deltaCameraDegrees: " + deltaCameraDegrees);


            double ADC = 180f - (135f + DAM);
            Console.WriteLine("ADC: " + ADC);
            double AC = (distance * Math.Sin((ADC * Math.PI) / 180)) / Math.Sin((135 * Math.PI) / 180); // 2121
            Console.WriteLine("AC: " + AC);
            double ACB = 90f - 45f - deltaCameraDegrees;
            Console.WriteLine("ACB: " + ACB);
            double AB = (AC * Math.Sin((ACB * Math.PI) / 180)) / Math.Sin((90 * Math.PI) / 180); // 584

            //if (distanceX < distanceY)
            //    distanceX = (int)AB;

            //if (distanceY < distanceX)
            //    distanceY = (int)AB;
            Console.WriteLine("AB: " + AB);


            StartMovement();


            // 64  WD -> W

            if (currentX > fakeX && cameraShifts != -3) // 98 > -274
            {
                HoldLeft();
                Console.WriteLine("ya derzhu vlevo chel");
            }
            else if (currentX < fakeX && (cameraShifts != 1 || cameraShifts != 3)) // 64
            {
                HoldRight();
                Console.WriteLine("ya derzhu pravo chel");
            }

            
            if (currentY > fakeY) 
                HoldDown();
            else if (currentY < fakeY && cameraShifts != -1) // 64
                HoldUp();

            // Check which to release first. Shortest coordinate first

            if (Math.Abs(distanceX) < Math.Abs(distanceY)) // yes
            {
                Console.WriteLine("x then y");
                WaitForX(currentX, x, fieldPollTime);
                WaitForY(currentY, y, fieldPollTime);
            }
            else
            {
                Console.WriteLine("y then x");
                WaitForY(currentY, y, fieldPollTime);
                WaitForX(currentX, x, fieldPollTime);
            }

            Console.WriteLine("movement complete");
        }

        private static void StartMovement()
        {
            // move x and move y
            // if x complete, stop x; if y complete, stop y

            MoveX();
            MoveY();
            CheckDestination();
        }

        private static void MoveX()
        {
            // x = cos(a * pi/2)
            // y = sin(a * pi/2)
            // 160
            // 1 Right = x++, y++
            // 2 DR = x++
            // 3 Down = x++, y--
            // 4 DL = y--
            // 5 Left = x--, y++
            // 6 UL = x--
            // 7 Up = x--, y++
            // 8 UR = y++

            // 128
            // 1 Right = x++
            // 2 DR = x++, y--
            // 3 Down = y--
            // 4 DL = x--, y++
            // 5 Left = x--
            // 6 UL = x--, y++
            // 7 Up = y++
            // 8 UR = x++, y++

            // 96
            // 1 Right = x++ & y--
            // 2 DR = y--
            // 3 Down = x--, y--
            // 4 DL = x--
            // 5 Left = x-- & y++
            // 6 UL = y++
            // 7 Up = x++ & y++, 
            // 8 UR = x++
        }

        private static void MoveY()
        {

        }

        private static void CheckDestination()
        {

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
                ReleaseLeft();
                if (cameraShifts == 1)
                {
                    ReleaseDown();
                }
                if (cameraShifts == -1)
                {
                    ReleaseUp();
                }
            }
            else
            {
                while (Memory.GetFieldX() > x)
                {
                    Thread.Sleep(fieldPollTime);
                }
                ReleaseRight();
                ReleaseLeft();
            }
        }

        private static void WaitForY(int currentY, int y, int fieldPollTime)
        {
            if (currentY < y)
            {
                Console.WriteLine("current is less");
                while (Memory.GetFieldY() < y)
                {
                    Thread.Sleep(fieldPollTime);
                }
                ReleaseUp();
                ReleaseDown();
                Console.WriteLine("reached y");
            }
            else
            {
                Console.WriteLine("current is greater");
                while (Memory.GetFieldY() > y) // -500 < 10 OR 500 > 10
                {
                    Thread.Sleep(fieldPollTime);
                }

                if (cameraShifts == -2)
                {
                    ReleaseRight();
                }

                if (cameraShifts == 0)
                {
                    ReleaseUp();
                }
                if (cameraShifts == -3)
                {
                    HoldLeft();
                }

                if (cameraShifts == 2)
                {
                    ReleaseLeft();
                }

                if (cameraShifts == 0)
                {
                    ReleaseDown();
                }
                if (cameraShifts == 1)
                {
                    HoldRight();
                }

                if (cameraShifts == -1)
                {
                    HoldUp();
                    Console.WriteLine("hold up");
                }

                Console.WriteLine("reached y");
            }
        }
    }
}
