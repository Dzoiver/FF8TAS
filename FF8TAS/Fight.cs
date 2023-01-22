using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FF8TAS
{
    class Fight
    {
        public void RunFromRandomEncounter()
        {
            Console.WriteLine("Battle started");
            GameInput.ChangeFps(GameInput.State.Battle);
            GameInput.HoldR2();
            GameInput.HoldL2();

            GameInput.ChangeFps(GameInput.State.Menu);
            while (Memory.GetBattleResult() == 0)
            {
                GameInput.WaitOneFrame();
            }

            Console.WriteLine("Result screen");
            GameInput.ReleaseR2();
            GameInput.ReleaseL2();

            while (Memory.GetBattleResult() == 1)
            {
                GameInput.PressX();
            }
            Console.WriteLine("Battle end");
        }
    }
}
