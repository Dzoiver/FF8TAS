using System;

namespace FF8TAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FF8 TAS ready to start!");

            Memory mem = new Memory();
            ECM ecm = new ECM(); // Game route

            mem.FindProcess();
            mem.MemoryInit();
            mem.SetFocus();

            mem.StartRun(ecm);
            Console.ReadKey();
        }
    }
}
