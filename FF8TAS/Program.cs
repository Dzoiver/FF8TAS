using System;
using System.Threading;

namespace FF8TAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FF8 TAS ready to start!");

            if (Memory.FindProcess() == null)
            {
                Console.WriteLine("Game isn't launched");
                return;
            }

            Memory.MemoryInit();
            Memory.SetFocus();

            ECM ecm = new ECM(); // Game route

            StartRun(ecm);
            Console.ReadKey();
        }

        static private void StartRun(IRoute route)
        {
            while (true) // Debugging the addresses or functions
            {
                GameInput.MoveTo(-1200, -22000); // 0; -19000
                //GameInput.MoveTo(0, 0);
                //GameInput.MoveTo(-500, 0);
                Thread.Sleep(5300);
                //Console.WriteLine(Memory.IsWM());
            }

            Thread clearTextThread0 = new Thread(() => route.HandleTextboxes(0));
            clearTextThread0.Start();
            Thread clearTextThread1 = new Thread(() => route.HandleTextboxes(1));
            clearTextThread1.Start();
            Thread clearTextThread4 = new Thread(() => route.HandleTextboxes(4));
            clearTextThread4.Start();

            if (Memory.GetStoryProgress() < 17)
            route.BalambGarden();

            route.FireCavern();
        }
    }
}
