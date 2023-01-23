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
            while (false) // Debugging the addresses or functions
            {
                Thread.Sleep(300);
                Console.WriteLine(Memory.IsWM());
            }


            if (Memory.GetStoryProgress() < 17)
            route.BalambGarden();

            route.FireCavern();
        }
    }
}
