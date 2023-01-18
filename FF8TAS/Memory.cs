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
    class Memory
    {
        public Process ff8Process;
        static private MemoryHelper64 helper;

        private int textBox;
        public static int LastTextStatus;

        [DllImport("User32.dll")]

        static extern int SetForegroundWindow(IntPtr point);
        public Process FindProcess()
        {
            Process[] ps = Process.GetProcessesByName("FF8_EN");
            Console.WriteLine("Found " + ps.Length + " processes");

            ff8Process = ps.FirstOrDefault();
            return ff8Process;
        }

        public void MemoryInit()
        {
            helper = new MemoryHelper64(ff8Process);
        }

        public void SetFocus(Process process = null)
        {
            if (process == null)
                process = ff8Process;

            IntPtr h = process.MainWindowHandle;
            SetForegroundWindow(h);
        }

        public void StartRun(IRoute route)
        {
            //route.ValveCheck();
            route.BalambGarden();
        }

        static public bool IsTextBox()
        {
            ulong targetAddress = helper.GetBaseAddress(0x192B585);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        static public bool IsMenu()
        {
            ulong targetAddress = helper.GetBaseAddress(0x18E490B);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        static public int GetFieldID()
        {
            ulong targetAddress = helper.GetBaseAddress(0x18D2FC0);
            int value = helper.ReadMemory<int>(targetAddress);
            return value;
        }

        static public bool CanMove()
        {
            ulong targetAddress = helper.GetBaseAddress(0x199D018);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 0)
                return true;
            else
                return false;
        }

        static public int GetFieldX()
        {
            ulong targetAddress = helper.GetBaseAddress(0x1677238);
            int value = helper.ReadMemory<int>(targetAddress);
            return value;
        }
        static public int GetFieldY()
        {
            ulong targetAddress = helper.GetBaseAddress(0x167723C);
            int value = helper.ReadMemory<int>(targetAddress);
            return value;
        }
        static public byte GetBGDraw()
        {
            ulong targetAddress = helper.GetBaseAddress(0x18E4906);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public byte GetTextID()
        {
            ulong targetAddress = helper.GetBaseAddress(0x148C8C8);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        // 0 nothing
        // 1 printing
        // 13 choice
        // 7 clearable
        // 10 next page
        static public byte GetTextStatus(int id = 0)
        {
            ulong address = 0x192B354;
            if (id == 1)
                address = 0x192B390;
            ulong targetAddress = helper.GetBaseAddress(address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            LastTextStatus = value;
            return value;
        }
        static public byte GetSquallAnimID()
        {
            ulong targetAddress = helper.GetBaseAddress(0x156ED16);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public byte GetOptionChoice()
        {
            ulong targetAddress = helper.GetBaseAddress(0x192B35B);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public bool IsGFMenu()
        {
            ulong targetAddress = helper.GetBaseAddress(0x18E4A68);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }
        // 156ED16
        // 18E4906
        // 18E4A68
    }
}
