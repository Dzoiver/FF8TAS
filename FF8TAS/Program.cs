using System;

namespace FF8TAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FF8 TAS ready to start!");

            Memory player = new Memory();
            ECM ecm = new ECM();

            player.FindProcess();
            player.MemoryInit();
            player.SetFocus();

            player.StartRun(ecm);
            Console.ReadKey();
        }
    }
}
