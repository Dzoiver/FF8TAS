using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FF8TAS
{
    class Fight
    {
        private bool isATBHeld = false;
        private int pollTime = 66;
        private short maxATB = 12000;
        private const int maxCommands = 3;
        private bool targetWindowOpened = false;

        public enum Command
        {
            Attack = 87,
            Magic = 118,
            Draw = 202,
            Item = 156
        }

        public enum Magic
        {
            Fire = 143,
            Thunder = 144,
            Blizzard = 206,
            Unknown
        }

        public enum DrawType
        {
            Stock = 11,
            Cast = 245
        }


        public void RunFromEncounter()
        {
            Console.WriteLine("Battle started");
            GameInput.ChangeFps(GameInput.State.Battle);
            GameInput.HoldR2();
            GameInput.HoldL2();

            while (Memory.GetBattleResult() == 0)
            {
                if (AnyATB_Ready())
                    HoldATB();
                GameInput.WaitOneFrame();
            }

            GameInput.ReleaseR2();
            GameInput.ReleaseL2();

            BattleResult();
        }

        public void Bats()
        {
            GameInput.ChangeFps(GameInput.State.Battle);
            Console.WriteLine("Bats started");

            if (Memory.GetSelectedAlly() == 0) // If Quistis first, do attack then draw
            {
                Attack();

                ChooseCommand(Command.Draw);
                SelectTarget(16); // Bat
                SelectDrawMagic(Magic.Unknown, 0);
                Draw(DrawType.Stock);
            }
            else // If Squall first, do draw first
            {
                ChooseCommand(Command.Draw);
                SelectTarget(); // Bat
                SelectDrawMagic(Magic.Unknown, 0);
                Draw(DrawType.Stock);

                Attack();
            }

            // Run when draw is happening or finished
            while (Memory.GetSquall_ItemAmount(0) == 0 && Memory.GetEnemyHealth(0) != 0)
            {
                // Attack();
                Thread.Sleep(pollTime);
            }
            RunFromEncounter();
        }

        private void Draw(DrawType drawType)
        {
            while (Memory.GetChoiceStockCastScale() != 16)
            {
                Thread.Sleep(pollTime);
            }

            while (Memory.GetCommandSelectedType() != (int)drawType)
            {
                GameInput.PressDown();
            }
            GameInput.PressX();
            Console.WriteLine("Draw Stock or Cast");
        }

        private void SelectDrawMagic(Magic magic = Magic.Unknown, int desiredIndex = 0)
        {
            while (Memory.GetChoiceMagicScale() != 16)
            {
                Thread.Sleep(pollTime);
            }
            if (magic == Magic.Unknown)
            {
                while (Memory.GetMagicChoiceIndex() != desiredIndex)
                {
                    GameInput.PressDown();
                }
            }
            GameInput.PressX();
            Console.WriteLine("Magic selected");
        }

        private bool AnyATB_Ready()
        {
            return (Memory.GetAlly1CurrentATB() == maxATB ||
                    Memory.GetAlly2CurrentATB() == maxATB ||
                    Memory.GetAlly3CurrentATB() == maxATB);
        }

        private void SelectTarget(int targetID = 0)
        {
            bool horizontal = true;
            while (Memory.GetBattleTarget() != targetID)
            {
                if (horizontal)
                {
                    GameInput.PressDown();
                }
            }
            Thread.Sleep(pollTime);
            GameInput.PressX();
            
            Console.WriteLine("Target selected");
        }

        private void ChooseCommand(Command command = Command.Attack, bool isDown = true)
        {
            while (!AnyATB_Ready())
            {
                Thread.Sleep(pollTime);
            }
            // find out the direction
            while (Memory.GetCommandSelectedType() != (int)command)
            {
                if (isDown)
                    GameInput.PressDown();
                else
                    GameInput.PressUp();
            }
            // go that direction until needed index
            GameInput.PressX();
            Console.WriteLine("Draw selected");
        }
        /*
         * wait for any ATB
         * attack bat
         * wait for any ATB
         * draw first spell
         * flee
        */

        private void Attack()
        {
            while (Memory.GetCommandWindowScale() != 16)
            {
                Thread.Sleep(pollTime);
            }
            Console.WriteLine("Attacking");
            GameInput.PressX();
            if (!targetWindowOpened)
            {
                GameInput.PressL1();
            }
            else
            {
                Thread.Sleep(pollTime);
            }

            GameInput.PressX();
        }

        private void HoldATB()
        {
            if (isATBHeld)
                return;

            // Wait until get control

            if (Memory.GetATBstatus() == 112)
            {
                isATBHeld = true;
            }
            else
            {
                GameInput.PressUp();
                GameInput.PressX();
            }
        }

        private void BattleResult()
        {
            GameInput.ChangeFps(GameInput.State.Menu);
            Console.WriteLine("Result screen");
            while (Memory.GetBattleResult() == 1)
            {
                GameInput.PressX();
            }
            Console.WriteLine("Battle end");
        }
    }
}
