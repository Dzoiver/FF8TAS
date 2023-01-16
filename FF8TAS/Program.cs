using System;

namespace FF8TAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FF8 TAS ready to start!");

            Player player = new Player();

            player.FindProcess();
            player.MemoryInit();
            player.SetFocus();
            player.StartRun();
            Console.ReadKey();

        }
    }
}
