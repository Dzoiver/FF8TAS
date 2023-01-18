using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WindowsInput;

namespace FF8TAS
{
    class ECM : IRoute
    {
        private int pollTime = 16;
        private int textBoxCount = 0;
        public void ValveCheck()
        {
            GameInput.ChangeFps(GameInput.State.Field);
            float frameTime = 1000f / 60f;
            float time = 0f;
            int lastTime = 0;
            int fakeTime;
            int delta;
            for (int i = 0; i < 100; i++) // Class room
            {
                time += frameTime;
                fakeTime = (int)time;
                delta = fakeTime - lastTime; // 3 turn, 50 - 0 = 50
                lastTime = fakeTime; // 50
                // GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_J);
                GameInput.CustomPress(WindowsInput.Native.VirtualKeyCode.VK_J, delta);
            }
        }
        // 0 -- 1 -- 13 -- 7 -- 1
        private void ClearText(int id = 0)
        {
            while (true)
            {
                try
                {
                    while (Memory.GetTextStatus(id) != 1) // Not Printing
                        Thread.Sleep(pollTime);
                    while (Memory.GetTextStatus(id) < 2) // Not Finished Printing
                    {
                        Thread.Sleep(pollTime);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Stopped clearing text. Channel: " + id);
                }

                if (Memory.LastTextStatus == 9)
                {
                    while (Memory.GetTextStatus(id) != 10) // Not Finished Printing
                    {
                        Thread.Sleep(pollTime);
                    }
                }
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
                textBoxCount++;
                // Console.WriteLine("Cleared " + textBoxCount + " text");
            }
        }
        private void Infirmary()
        {
            while (textBoxCount < 5)
            {
                Thread.Sleep(pollTime);
            }
        }

        private void NameMenu()
        {
            Console.WriteLine("Wait for menu");
            while (!Memory.IsMenu())
            {
                Thread.Sleep(pollTime);
            }
            Console.WriteLine("Name menu");
            GameInput.ChangeFps(GameInput.State.Menu);
            Thread.Sleep(1000); // Need to change it to check for value instead of delay

            for (int i = 0; i < 5; i++)
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_I);
            }

            GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_H); // Start
            GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K); // X
        }

        private void Infirmary2()
        {
            while (Memory.GetFieldID() != 229)
            {
                Thread.Sleep(500);
            }
        }

        private void Corridor()
        {
            Console.WriteLine("Corridor started");
            GameInput.HoldDown();
            while (Memory.GetFieldID() != 232)
            {
                Thread.Sleep(33);
            }
            GameInput.ReleaseDown();
        }

        private void ClassRoom()
        {
            Console.WriteLine("Class room field");

            //while (Memory.GetSquallAnimID() != 8)
            //{
            //    Thread.Sleep(200);
            //}

            Console.WriteLine("Hold UR");
            GameInput.HoldUp();
            GameInput.HoldRight();

            while (Memory.GetFieldX() < 1242)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.ReleaseUp();

            while (Memory.GetBGDraw() == 0) // Front of the Class
            {
                Thread.Sleep(500);
            }
            GameInput.HoldDown();
            Console.WriteLine("Camera changed");
            while (Memory.GetFieldX() >= 987)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.ReleaseDown();

            while (Memory.GetFieldY() >= -2809)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.ReleaseRight();
            GameInput.PressX(); // Talk to Quistis

            while (!Memory.CanMove())
            {
                Thread.Sleep(33);
            }
            Console.WriteLine("Leaving the room");
            GameInput.HoldUp();
            GameInput.HoldRight();

            while (Memory.GetFieldX() < 1341)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseUp();

            while (Memory.GetFieldID() != 139)
            {
                Thread.Sleep(33);
            }

            GameInput.ReleaseRight();
        }
        public void BalambGarden()
        {
            Console.WriteLine("Starting game");
            // GameInput.PressX();
            Console.WriteLine("FMV time");
            GameInput.ChangeFps(GameInput.State.Field);
            Thread clearTextThread0 = new Thread(() => ClearText(0));
            Thread clearTextThread1 = new Thread(() => ClearText(1));
            clearTextThread0.Start();
            // Infirmary();
            // NameMenu();

            // GameInput.ChangeFps(GameInput.State.Field);
            // Infirmary2();
            clearTextThread1.Start();
            // Corridor();
            ClassRoom();
        }
        enum Buttons
        {
            Up = WindowsInput.Native.VirtualKeyCode.VK_W
        }

        public void FireCavern()
        {

        }
    }
}
