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
        private void HandleTextboxes(int id = 0)
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
                    while (Memory.GetTextStatus(id) != 10)
                    {
                        Thread.Sleep(pollTime);
                    }
                }

                if (Memory.LastTextStatus == 13)
                {
                    Choices.Choice desiredChoice = Choices.GetNextChoice();
                    while (Memory.GetOptionChoice() != desiredChoice.desiredID && desiredChoice.name != "") // Not Finished Printing
                    {
                        if (desiredChoice.isCursorGoDown)
                            GameInput.PressDown();
                        else
                            GameInput.PressUp();
                    }
                    Console.WriteLine("Desired option reached");
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
                GameInput.PressTriangle();
            }

            GameInput.PressStart();
            GameInput.PressX();
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

            while (Memory.GetSquallAnimID() != 8)
            {
                Thread.Sleep(200);
            }

            Console.WriteLine("Hold UR");
            GameInput.HoldUp();
            GameInput.HoldRight();


            while (!Memory.CanMove())
            {
                Thread.Sleep(33);
            }

            while (Memory.GetFieldX() < 1242)
            {
                Thread.Sleep(pollTime);
            }
            Console.WriteLine("Release Up");
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

        private void SelphieBonk()
        {
            Console.WriteLine("2F Field");
            Choices.AddQueue(Choices.SelphieGarden1);
            Choices.AddQueue(Choices.SelphieGarden2);
            GameInput.HoldDown();
            Thread.Sleep(1533); // Wait until screen is changed for sure
            // -1277
            while (Memory.GetFieldX() < -1277)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.HoldLeft();
            Console.WriteLine("Change direction");
            while (Memory.CanMove())
            {
                Thread.Sleep(33);
            }

            GameInput.ReleaseDown();
            GameInput.ReleaseLeft();
            Console.WriteLine("Entered the trigger");
            // 2 Options

            while (!Memory.CanMove())
            {
                Thread.Sleep(33);
            }
            Console.WriteLine("Leaving");

            GameInput.HoldDown();
            GameInput.HoldRight();
            

            while (Memory.GetFieldX() < 280)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseDown();

            while (Memory.GetFieldID() != 137)
            {
                Thread.Sleep(33);
            }

            GameInput.ReleaseRight();
        }
        private void TakeCardsUseLift()
        {
            GameInput.HoldRight();
            GameInput.HoldDown();
            Thread.Sleep(1500); // Bad
            Console.WriteLine("Taking Cards");
            while (Memory.GetFieldX() <= 150)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseDown();

            while (Memory.GetFieldY() <= -3677)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseRight();
            GameInput.PressX();

            while (!Memory.CanMove())
            {
                Thread.Sleep(33);
            }
            GameInput.HoldRight();


            while (Memory.CanMove())
            {
                Thread.Sleep(33);
            }
            GameInput.ReleaseRight();
        }

        private void ElevatorMainHall()
        {
            Console.WriteLine("Going down");
            GameInput.HoldDown();

            while (Memory.GetFieldID() != 165)
            {
                Thread.Sleep(33);
            }

            GameInput.HoldLeft();

            while (Memory.GetFieldX() >= -437)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseLeft();

            while (Memory.GetFieldY() >= -7404)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseDown();
            GameInput.HoldRight();

            Console.WriteLine("Hold Right");
            while (Memory.GetFieldX() <= -92)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.ReleaseRight();
            GameInput.HoldUp();
            GameInput.PressX();
            GameInput.ReleaseUp();
            Console.WriteLine("Board activated");
            GameInput.HoldUp();
            GameInput.HoldX();

            while (Memory.GetFieldID() != 159)
            {
                Thread.Sleep(33);
            }
            Console.WriteLine("Teleported to Gates");

            GameInput.ReleaseX();
            GameInput.ReleaseUp();
        }

        private void GatesTakeGFs()
        {
            GameInput.HoldDown();
            Thread.Sleep(1500);
            while (Memory.CanMove())
            {
                Thread.Sleep(33);
            }

            Console.WriteLine("Entered the trigger");

            while (!Memory.IsGFMenu())
            {
                Thread.Sleep(pollTime);
            }
            GameInput.ReleaseDown();
            Console.WriteLine("Quetzacotl");
            GameInput.ChangeFps(GameInput.State.Menu);
            Thread.Sleep(380); // Need to change it to check for value instead of delay

            GameInput.PressStart();
            GameInput.PressX();

            while (Memory.IsGFMenu())
            {
                Thread.Sleep(pollTime);
            }

            while (!Memory.IsGFMenu())
            {
                Thread.Sleep(pollTime);
            }

            Console.WriteLine("Shiva");
            Thread.Sleep(380); // Need to change it to check for value instead of delay
            GameInput.PressStart();
            GameInput.PressX();
            GameInput.ChangeFps(GameInput.State.Field);
            while (Memory.IsGFMenu())
            {
                Thread.Sleep(pollTime);
            }

            Console.WriteLine("Waiting for tutorial");

            while (!Memory.IsGFMenu())
            {
                Thread.Sleep(pollTime);
            }

            GameInput.HoldTriangle();

            while (Memory.IsGFMenu())
            {
                Thread.Sleep(pollTime);
            }
            Console.WriteLine("Triangle released");
            GameInput.ReleaseTriangle();

            GameInput.HoldDown();

            while (!Memory.CanMove())
            {
                Thread.Sleep(33);
            }

            FirstMenu();

            GameInput.HoldDown();

            while (Memory.IsField())
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseDown();
        }

        private void FirstMenu()
        {
            Console.WriteLine("Starting menu");

            GameInput.PressCircle();

            while (Memory.GetMenuCursorStatus() == 0)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.ReleaseDown();

            GameInput.ChangeFps(GameInput.State.Menu);

            while (Memory.GetMenuCursorStatus() != 3)
            {
                Thread.Sleep(pollTime);
            }

            // Ready to navigate
            Console.WriteLine("Cursor ready");
            GameInput.PressX();

            while (Memory.GetMenuCursorStatus() != 10)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressX();
            Console.WriteLine("Press X");

            while (Memory.GetMenuCursorStatus() != 7)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressX();

            GameInput.ChangeFps(GameInput.State.Field);
            Console.WriteLine("Finished menu");
            // Left the menu
        }
        public void BalambGarden()
        {
            Console.WriteLine("Starting game");
            GameInput.PressX();
            Console.WriteLine("FMV time");
            GameInput.ChangeFps(GameInput.State.Field);
            Thread clearTextThread0 = new Thread(() => HandleTextboxes(0));
            Thread clearTextThread1 = new Thread(() => HandleTextboxes(1));
            clearTextThread0.Start();
            //Infirmary();
            //NameMenu();

            //GameInput.ChangeFps(GameInput.State.Field);
            //Infirmary2();
            //clearTextThread1.Start();
            //Corridor();
            //ClassRoom();
            //SelphieBonk();
            //TakeCardsUseLift();
            //ElevatorMainHall();
            GatesTakeGFs();
        }

        public void TravelToCavern()
        {

        }

        public void FireCavern()
        {
            TravelToCavern();
        }
    }
}
