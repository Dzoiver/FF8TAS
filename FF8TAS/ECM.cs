using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WindowsInput;

namespace FF8TAS
{
    class ECM : IRoute
    {
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
        public void BalambGarden()
        {
            Console.WriteLine("Starting game");
            GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            Console.WriteLine("FMV time");
            GameInput.ChangeFps(GameInput.State.Field);
            GameInput.WaitFor(GameInput.Entity.TextBox);

            Console.WriteLine("Mashing time");

            while (!Player.IsMenu())
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            }
            Console.WriteLine("Name menu");
            GameInput.ChangeFps(GameInput.State.Menu);
            Thread.Sleep(1000);

            for (int i = 0; i < 5; i++)
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_I);
            }
            GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_H); // Start
            GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K); // X
            Console.WriteLine("Mashing again");
            GameInput.ChangeFps(GameInput.State.Field);
            Console.WriteLine("Waiting for class room");
            GameInput.isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
            while (!Player.IsField(232)) // Class room
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            }
            Console.WriteLine("Class room field");
            GameInput.isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);
            GameInput.WaitOneFrame();
            GameInput.isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
            GameInput.isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_D);
            Thread.Sleep(500);
            while(Player.GetTextID() != 10)
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            }

            while (!Player.CanSkipText())
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            }
            GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            Console.WriteLine("Squall stands up");
            // After you can move
            GameInput.isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);

            while (Player.GetFieldX() < 1242)
            {
                GameInput.WaitOneFrame();
            }
            Console.WriteLine("Change direction");
            GameInput.isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W);

            while  (Player.GetBGDraw() == 0)
            {
                GameInput.WaitOneFrame();
            }
            Console.WriteLine("Camera change");
            GameInput.isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
            while (Player.GetFieldX() <= 987)
            {
                GameInput.WaitOneFrame();
            }
            GameInput.isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);
            while (Player.CanMove())
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            }
            Console.WriteLine("Talking to Quistis");
            while (!Player.CanMove())
            {
                GameInput.KeyDownUpFrame(WindowsInput.Native.VirtualKeyCode.VK_K);
            }
            Console.WriteLine("Leaving the room");
            GameInput.isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
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
