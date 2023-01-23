using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FF8TAS
{
    class Menu
    {
        private int pollTime = 16;
        public void OpenMainSection(MainSection section = MainSection.Junction)
        {
            while (Memory.GetMenuCursorStatus() != 3)
            {
                Thread.Sleep(pollTime);
            }

            // Ready to navigate
            Console.WriteLine("Cursor ready");

            while (Memory.GetMainCursorPos() != (int)section)
            {
                GameInput.PressUp(9);
            }

            GameInput.PressX();
        }

        public void ChooseCharacter(int position = 0)
        {
            while (Memory.GetMenuCursorStatus() != 10)
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressX();
        }

        public void Junction_Section(JunctionSection section = JunctionSection.Junction)
        {
            // Scroll left or right until find Junction
            while (Memory.GetJunctionFade() != 16)
            {
                Thread.Sleep(pollTime);
            }
            GameInput.PressX();
        }

        public void JunctionAbility(CharacterAbility ability = CharacterAbility.Draw)
        {
            GameInput.PressX();
            while (Memory.GetAbilityPosition() != (int)ability)
            {
                GameInput.PressDown(9);
            }
            GameInput.PressX();
        }

        public void GFLearnAbility(int index = 0, int page = 0, bool moveDown = true)
        {
            GameInput.PressX();

            while (Memory.GetGF_LearnScroll() != 16)
            {
                Thread.Sleep(pollTime);
            }

            if (page == 0)
            {
                while (Memory.GetGF_AbilityPos1() != index)
                {
                    if (moveDown)
                        GameInput.PressDown(9);
                    else
                        GameInput.PressUp(9);
                }
            }
            else if (page == 1)
            {
                GameInput.PressRight();
                while (Memory.GetGF_AbilityPos2() != index)
                {
                    if (moveDown)
                        GameInput.PressDown(9);
                    else
                        GameInput.PressUp(9);
                }
            }

            GameInput.PressX();
            GameInput.PressTriangle();
            while (Memory.GetGF_LearnScroll() != 0)
            {
                Thread.Sleep(pollTime);
            }
        }

        public static void SkipTutorial()
        {
            Console.WriteLine("Waiting for tutorial");

            while (!Memory.IsGFMenu())
            {
                Thread.Sleep(16);
            }

            GameInput.HoldTriangle();

            while (Memory.IsGFMenu())
            {
                Thread.Sleep(16);
            }
            Console.WriteLine("Triangle released");
            GameInput.ReleaseTriangle();
        }

        public enum CharacterAbility
        {
            Magic,
            GF,
            Draw,
            Item
        }

        public enum JunctionSection
        {
            Junction,
            Off,
            Auto,
            Ability
        }



        public enum MainSection
        {
            Junction,
            Item,
            Magic,
            Status,
            GF,
            Ability,
            Switch,
            Card,
            Config,
            Tutorial,
            Save
        }
    }
}
