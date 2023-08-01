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
        public void RunFromRandomEncounter()
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

            while (!AnyATB_Ready())
            {
                Thread.Sleep(pollTime);
            }

            Attack();

            while (!AnyATB_Ready())
            {
                Thread.Sleep(pollTime);
            }


            // Choose draw
            ChooseCommand(2);
            // Choose target
            GameInput.PressX();

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

        private bool AnyATB_Ready()
        {
            return (Memory.GetAlly1CurrentATB() == maxATB ||
                    Memory.GetAlly2CurrentATB() == maxATB ||
                    Memory.GetAlly3CurrentATB() == maxATB);
        }

        private void ChooseCommand(int desiredIndex = 0)
        {
            // find out the direction
            bool isDown = true;
            int currentIndex = Memory.GetBattleCommand();
            int difference = Math.Abs(currentIndex - desiredIndex);
            if (difference > 2)
            {
                isDown = false;
            }
            while (Memory.GetBattleCommand() != desiredIndex)
            {
                if (isDown)
                    GameInput.PressDown();
                else
                    GameInput.PressUp();
            }
            // go that direction until needed index
            GameInput.PressX();
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
            GameInput.PressX();
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
