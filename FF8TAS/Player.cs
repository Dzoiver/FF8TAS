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
        static private MemoryHelper64 helper;

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

        static public bool IsField(int fieldId)
        {
            ulong targetAddress = helper.GetBaseAddress(0x18D2FC0);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == fieldId)
                return true;
            else
                return false;
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
        static public int GetBGDraw()
        {
            ulong targetAddress = helper.GetBaseAddress(0x18E4906);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public int GetTextID()
        {
            ulong targetAddress = helper.GetBaseAddress(0x148C8C8);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public bool CanSkipText()
        {
            ulong targetAddress = helper.GetBaseAddress(0x148C8C8);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 7)
                return true;
            else
                return false;
        }
        // 18E4906
    }
}
