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
    static class Memory
    {
        static private Process ff8Process;
        static private MemoryHelper64 helper;
        public static int LastTextStatus;

        [DllImport("User32.dll")]

        static extern int SetForegroundWindow(IntPtr point);
        static public Process FindProcess()
        {
            Process[] ps = Process.GetProcessesByName("FF8_EN");
            Console.WriteLine("English version found");
            if (ps.Length > 0)
            {
                ff8Process = ps.FirstOrDefault();
                Language language = new Language("EN");
                return ff8Process;
            }

            ps = Process.GetProcessesByName("FF8_FR");
            if (ps.Length > 0)
            {
                Console.WriteLine("French version found");
                ff8Process = ps.FirstOrDefault();
                Language language = new Language("FR");
                return ff8Process;
            }

            return null;
        }

        static public void MemoryInit()
        {
            helper = new MemoryHelper64(ff8Process);
        }

        static public void SetFocus(Process process = null)
        {
            if (process == null)
                process = ff8Process;

            IntPtr h = process.MainWindowHandle;
            SetForegroundWindow(h);
        }

        static public void StartRun(IRoute route)
        {
            //route.ValveCheck();
            route.BalambGarden();
        }

        public static ulong FieldX_Address;
        public static ulong FieldY_Address;
        public static ulong BGDraw_Address;
        public static ulong TextStatus_Address;
        public static ulong SquallAnim_Address;
        public static ulong OptionChoice_Address;
        public static ulong IsGFMenu_Address;
        public static ulong TextBox_Address;
        public static ulong IsMenu_Address;
        public static ulong FieldID_Address;
        public static ulong CanMove_Address;
        public static ulong TextID_Address;
        public static ulong IsField_Address;
        public static ulong MenuCursorStatus_Address;

        static public byte GetMenuCursorStatus()
        {
            ulong targetAddress = helper.GetBaseAddress(MenuCursorStatus_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public bool IsTextBox()
        {
            ulong targetAddress = helper.GetBaseAddress(TextBox_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        static public bool IsMenu()
        {
            ulong targetAddress = helper.GetBaseAddress(IsMenu_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        static public int GetFieldID()
        {
            ulong targetAddress = helper.GetBaseAddress(FieldID_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            return value;
        }

        static public bool CanMove()
        {
            ulong targetAddress = helper.GetBaseAddress(CanMove_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 0)
                return true;
            else
                return false;
        }

        static public int GetFieldX()
        {
            ulong targetAddress = helper.GetBaseAddress(FieldX_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            return value;
        }
        static public int GetFieldY()
        {
            ulong targetAddress = helper.GetBaseAddress(FieldY_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            return value;
        }
        static public byte GetBGDraw()
        {
            ulong targetAddress = helper.GetBaseAddress(BGDraw_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public byte GetTextID()
        {
            ulong targetAddress = helper.GetBaseAddress(TextID_Address);
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
            ulong address = TextStatus_Address;
            if (id == 1)
                address = address + 0x3C * (ulong)id; // + 3C
            ulong targetAddress = helper.GetBaseAddress(address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            LastTextStatus = value;
            return value;
        }
        static public byte GetSquallAnimID()
        {
            ulong targetAddress = helper.GetBaseAddress(SquallAnim_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public byte GetOptionChoice()
        {
            ulong targetAddress = helper.GetBaseAddress(OptionChoice_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public bool IsGFMenu()
        {
            ulong targetAddress = helper.GetBaseAddress(IsGFMenu_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        static public bool IsField()
        {
            ulong targetAddress = helper.GetBaseAddress(IsField_Address);
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
