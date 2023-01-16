using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory.Utils;
using Memory.Win64;
using WindowsInput;

namespace FF8TAS
{
    class Player
    {
        public Process ff8Process;
        private MemoryHelper64 helper;

        private int textBox;

        [DllImport("User32.dll")]

        static extern int SetForegroundWindow(IntPtr point);
        public Process FindProcess()
        {
            Process[] ps = Process.GetProcessesByName("FF8_EN");
            Console.WriteLine(ps.Length);

            ff8Process = ps.FirstOrDefault();
            return ff8Process;
        }

        public void MemoryInit()
        {
            helper = new MemoryHelper64(ff8Process);
            // 192B585
            ulong targetAddress = helper.GetBaseAddress(0x1677238);
            // int[] offsets = {  };
            // ulong targetAddr = MemoryUtils.OffsetCalculator(helper, baseAddr, offsets);
            string output = helper.ReadMemory<int>(targetAddress).ToString();
            Console.WriteLine(output);
            targetAddress = helper.GetBaseAddress(0x192B585);

        }

        public void SetFocus(Process process = null)
        {
            if (process == null)
                process = ff8Process;

            IntPtr h = process.MainWindowHandle;
            SetForegroundWindow(h);
        }

        public void StartRun()
        {
            InputSimulator isim = new InputSimulator();
            Console.WriteLine("Starting game");
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_K);
            Thread.Sleep(30);
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_K);
            Console.WriteLine("FMV time");
            while (!IsTextBox())
            {
                Thread.Sleep(33);
            }
            Console.WriteLine("Mashing time");

            while (!IsMenu())
            {
                isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_K);
                Thread.Sleep(16);
                isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_K);
                Thread.Sleep(17);
            }
            Thread.Sleep(1000);

            for (int i = 0; i < 5; i++)
            {
                isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_I);
                Thread.Sleep(8);
                isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_I);
                Thread.Sleep(8);
            }

            // Select
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_H);
            Thread.Sleep(8);
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_H);
            Thread.Sleep(16);

            // X
            isim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_K);
            Thread.Sleep(16);
            isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_K);
            Thread.Sleep(16);

            // Stopwatch s = new Stopwatch();
            // s.Start();
            // while (s.Elapsed < TimeSpan.FromSeconds(8))
            // {

            // }


            // Thread.Sleep(3000);
            // while (s.Elapsed < TimeSpan.FromSeconds(5))
            // isim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_A);
            // s.Stop();
        }

        private bool IsTextBox()
        {
            ulong targetAddress = helper.GetBaseAddress(0x192B585);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        private bool IsMenu()
        {
            ulong targetAddress = helper.GetBaseAddress(0x18E490B);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }
    }
}
