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

            //while (Memory.GetSquallAnimID() != 8)
            //{
            //    Thread.Sleep(200);
            //}

            Console.WriteLine("Hold UR");
            GameInput.HoldUp();
            GameInput.HoldRight();


            while (!Memory.CanMove())
            {
                Thread.Sleep(33);
            }

            while (Memory.GetFieldX() < 1245)
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

            while (Memory.GetFieldY() >= -2780)
            {
                Thread.Sleep(pollTime);
            }
            Console.WriteLine(Memory.GetFieldY() + ": stopped here");
            GameInput.ReleaseRight();
            GameInput.PressX(); // Talk to Quistis
            // Stopped here
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
            Thread.Sleep(1535); // Wait until screen is changed for sure
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

            GameInput.PressCircle();
            GameInput.ReleaseDown();
            while (Memory.GetMenuCursorStatus() == 0)
            {
                Thread.Sleep(pollTime);
            }

            

            GameInput.ChangeFps(GameInput.State.Menu);

            FirstMenu();

            GameInput.HoldDown();

            while (!Memory.IsWM())
            {
                Thread.Sleep(pollTime);
                Console.WriteLine(Memory.IsWM());
            }
            Console.WriteLine("Hello WM");
            GameInput.ReleaseDown();
        }

        private void FirstMenu()
        {
            if (Memory.GetMenuCursorStatus() == 0)
            {
                GameInput.PressCircle();
            }
            Console.WriteLine("Starting menu");
            Menu firstMenu = new Menu();

            firstMenu.OpenMainSection(Menu.MainSection.Junction);

            firstMenu.ChooseCharacter();

            firstMenu.Junction_Section(Menu.JunctionSection.Junction);

            while (Memory.GetJunctionScroll() != 16)
            {
                Thread.Sleep(pollTime); 
            }

            Console.WriteLine("GF - Magic");
            GameInput.PressX();
            // GF

            while (Memory.GetItemFade() != 16)
            {
                Thread.Sleep(pollTime);
            }

            Console.WriteLine("Selecting GF");
            GameInput.PressDown();
            GameInput.PressX();
            Console.WriteLine("Shiva");
            GameInput.PressTriangle();

            while (Memory.GetItemFade() != 0)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressTriangle();
            Console.WriteLine("Exit");
            while (Memory.GetItemFade() != 16)
            {
                Thread.Sleep(pollTime);
            }
            firstMenu.JunctionAbility(Menu.CharacterAbility.Magic);
            GameInput.PressDown(16);
            firstMenu.JunctionAbility(Menu.CharacterAbility.Draw);
            GameInput.PressDown(16);
            firstMenu.JunctionAbility(Menu.CharacterAbility.Item);
            GameInput.PressTriangle(9);

            while (Memory.GetJunctionScroll() != 0)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.PressTriangle(9);

            while (Memory.GetItemFade() != 0)
            {
                Thread.Sleep(pollTime);
            }

            // Go to Squall
            Thread.Sleep(16);
            Console.WriteLine("Go to Squall");

            GameInput.PressR1();
            Console.WriteLine("Pressed");
            while (Memory.GetCharacterScrollJ() != 0)
            {
                Thread.Sleep(pollTime);
            }
            // 
            Console.WriteLine("GF - Magic");
            GameInput.PressX();
            // GF

            while (Memory.GetJunctionScroll() != 16)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressX();

            while (Memory.GetItemFade() != 16)
            {
                Thread.Sleep(pollTime);
            }

            Console.WriteLine("Selecting GF");
            GameInput.PressX(16);
            Console.WriteLine("Quetz");

            GameInput.PressTriangle(16);
            while (Memory.GetItemFade() != 0)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressTriangle();
            Console.WriteLine("Exit");
            while (Memory.GetItemFade() != 16)
            {
                Thread.Sleep(pollTime);
            }
            firstMenu.JunctionAbility(Menu.CharacterAbility.Magic);
            GameInput.PressDown();
            firstMenu.JunctionAbility(Menu.CharacterAbility.Draw);
            GameInput.PressDown();
            firstMenu.JunctionAbility(Menu.CharacterAbility.Item);
            GameInput.PressTriangle();

            Console.WriteLine("Squall done");
            while (Memory.GetItemFade() != 0)
            {
                Thread.Sleep(pollTime);
            }
            Thread.Sleep(16);
            GameInput.PressTriangle();

            Console.WriteLine("Main menu");
            firstMenu.OpenMainSection(Menu.MainSection.Config);
            Console.WriteLine("Config menu");
            while (Memory.GetConfigFade() != 16)
            {
                Thread.Sleep(pollTime);
            }

            while (Memory.GetConfigCursorPos() != 1)
            {
                GameInput.PressDown(9);
            }

            GameInput.PressRight();

            while (Memory.GetConfigCursorPos() != 4)
            {
                GameInput.PressDown(9);
            }

            Console.WriteLine("Camera movement to 0");
            GameInput.PressLeft();
            GameInput.PressLeft();
            while (Memory.GetConfigCursorPos() != 5)
            {
                GameInput.PressDown(9);
            }
            GameInput.PressRight();
            GameInput.PressRight();
            while (Memory.GetConfigCursorPos() != 6)
            {
                GameInput.PressDown(9);
            }
            GameInput.PressRight();
            GameInput.PressRight();
            Console.WriteLine("Config Done");
            GameInput.PressTriangle();

            firstMenu.OpenMainSection(Menu.MainSection.GF);

            // Entering the GF menu

            while (Memory.GetGF_Fade() != 16)
            {
                Thread.Sleep(pollTime);
            }

            Console.WriteLine("Quezacotl ability selection");

            GameInput.PressX();

            while (Memory.GetGF_Fade() != 16)
            {
                Thread.Sleep(pollTime);
            }

            Console.WriteLine("Learn");
            firstMenu.GFLearnAbility(7, 0, false);

            GameInput.PressR1(); // Go to Shiva

            while (Memory.GetGF_SelectedScroll() != 0) // The variable is mb wrong
            {
                Thread.Sleep(pollTime);
            }
            Thread.Sleep(16); // Without it, it stops
            Console.WriteLine("Shiva ability selection");
            firstMenu.GFLearnAbility(0, 1, true);

            GameInput.PressTriangle();

            while (Memory.GetGF_Fade() != 16)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressTriangle();

            while (Memory.GetMainOuterFade() != 16)
            {
                Thread.Sleep(pollTime);
            }
            // Stopped here once
            Thread.Sleep(200);
            GameInput.PressTriangle();

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
            Infirmary();
            NameMenu();

            GameInput.ChangeFps(GameInput.State.Field);
            Infirmary2();
            clearTextThread1.Start();
            Corridor();
            ClassRoom();
            SelphieBonk();
            TakeCardsUseLift();
            ElevatorMainHall();

            GatesTakeGFs();
            Console.WriteLine("Balamb Finished!");
        }

        public void TravelToCavern()
        {
            Fight fight = new Fight();
            GameInput.HoldL1();
            GameInput.HoldRight();
            GameInput.HoldSquare();
            Console.WriteLine("WM started");

            while (Memory.GetCameraDirection() > 25)
            {
                if (Memory.IsBattle())
                {
                    GameInput.ReleaseL1();
                    GameInput.ReleaseSquare();
                    GameInput.ReleaseRight();
                    fight.RunFromRandomEncounter();
                    GameInput.HoldL1();
                    GameInput.HoldRight();
                    GameInput.HoldSquare();
                }
                Thread.Sleep(pollTime);
            }
            GameInput.ReleaseL1();
            Console.WriteLine("Released");
            while (Memory.GetFieldID() != 136)
            {
                if (Memory.IsBattle())
                {
                    GameInput.ReleaseSquare();
                    GameInput.ReleaseRight();
                    fight.RunFromRandomEncounter();
                    GameInput.HoldRight();
                    GameInput.HoldSquare();
                }
                Thread.Sleep(pollTime);
            }
            Console.WriteLine("At cavern");
            GameInput.ReleaseRight();
            GameInput.ReleaseSquare();
        }

        public void FireCavern()
        {
            GameInput.ChangeFps(GameInput.State.Field);
            TravelToCavern();
        }
    }
}
