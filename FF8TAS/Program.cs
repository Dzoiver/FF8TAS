﻿using System;
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
                
            }
            Thread clearTextThread0 = new Thread(() => route.HandleTextboxes(0));
            clearTextThread0.Start();
            Thread clearTextThread1 = new Thread(() => route.HandleTextboxes(1));
            clearTextThread1.Start();
            Thread clearTextThread4 = new Thread(() => route.HandleTextboxes(4));
            clearTextThread4.Start();
            



            if (Memory.GetStoryProgress() < 17)
            route.BalambGarden();

            if (Memory.GetStoryProgress() < 290)
            route.FireCavern();

            if (Memory.GetStoryProgress() == 290)
            {
                route.LD2Skip();
            }
        }
    }
}
