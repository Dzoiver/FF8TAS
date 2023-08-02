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
        static private int lastX = 0;
        static private int lastY = 0;
        static private int lastZ = 0;

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
            Console.WriteLine("Game is focused");
        }
        // 18fe9b8
        public static ulong FieldX_Address;
        public static ulong FieldY_Address = 0x28777C; // FF8_EN.exe+167723C
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
        public static ulong IsBattleOrField_Address;

        public static ulong MenuCursorStatus_Address;
        public static ulong JunctionScroll_Address;
        public static ulong ItemFade_Address;
        public static ulong JunctionFade_Address;

        public static ulong AbilityPosition_Address;
        public static ulong CharacterScrollJ_Address;

        public static ulong MainCursorPos_Address;
        public static ulong ConfigCursorPos_Address;
        public static ulong MainOuterFade_Address;
        public static ulong ConfigFade_Address;

        public static ulong GF_Fade_Address;
        public static ulong GF_CursorPosition_Address;
        public static ulong GF_AbilityScroll_Address;
        public static ulong GF_SelectedScroll_Address;
        public static ulong GF_LearnScroll_Address;
        public static ulong GF_AbilityPos1_Address;
        public static ulong GF_AbilityPos2_Address;

        private static ulong WM_MoveStatus = 0xB619F; // 1848819

        private static ulong WMCameraTilt_Address = 0x34034A; // 0x18fe9b8 - 1C3ED02 = 34034A 18FEA0C
        // here will be the relative address + memory base
        private static ulong IsWM_Address = 0x1B71AD; //  0x174780B
        private static ulong CameraDirection_Address = 0x34034A; // 0x1C3ED02 +
        private static ulong BattleResult_Address = 0x285D14; // 1678CA4 
        private static ulong IsBattle_Address = 0x2BAD8; // 18D2EE0 -
        private static ulong StoryProgress_Address = 0x100; // 18FEAB8 +
        private static ulong ATBStatus = 0x285982; // 1679036 -
        private static ulong commandPosition = 0x77E8B; // 1976843 +

        private static ulong Ally1CurrentATB = 0x2916C; // 1927B24 + D0 difference    // FF8_EN.exe+1927B24
        private static ulong Ally2CurrentATB = 0x2923C; // 1927BF4 +
        private static ulong Ally3CurrentATB = 0x2930C; // 1927CC4 +

        private static ulong CameraAngleAdress = 0x1A0B0; // 18E4908 

        public static byte GetCameraAngle()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - CameraAngleAdress);
            return helper.ReadMemory<byte>(targetAddress);
        }
        public static short GetAlly3CurrentATB()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + Ally3CurrentATB);
            return helper.ReadMemory<short>(targetAddress);
        }
        public static short GetAlly2CurrentATB()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + Ally2CurrentATB);
            return helper.ReadMemory<short>(targetAddress);
        }
        public static short GetAlly1CurrentATB() 
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + Ally1CurrentATB);
            return helper.ReadMemory<short>(targetAddress);
        }
        static public byte GetCommandPosition()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - commandPosition);
            return helper.ReadMemory<byte>(targetAddress);
        }
        static public byte GetATBstatus()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - ATBStatus);
            return helper.ReadMemory<byte>(targetAddress);
        }
        static public int GetStoryProgress()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + StoryProgress_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            Console.WriteLine(value);
            return value;
        }
        static public bool IsBattle()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - IsBattle_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value == 1;
        }
        static public byte GetBattleResult()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - BattleResult_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public short GetCameraDirection()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + CameraDirection_Address);
            short value = helper.ReadMemory<short>(targetAddress);
            return value;
        }
        static public bool IsWM()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - IsWM_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value == 2;
        }
        static public byte GetWMCameraTilt()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + WMCameraTilt_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        /// <summary>
        /// Equals 0 if can walk on a WM, 2 if opening a menu
        /// </summary>
        /// <returns></returns>
        static public byte GetWMStatus()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - WM_MoveStatus);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public byte GetGF_AbilityPos2()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_AbilityPos2_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetGF_AbilityPos1()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_AbilityPos1_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetGF_LearnScroll()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_LearnScroll_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetGF_SelectedScroll()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_SelectedScroll_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetGF_AbilityScroll()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_AbilityScroll_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetGF_CursorPosition()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_CursorPosition_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetGF_Fade()
        {
            ulong targetAddress = helper.GetBaseAddress(GF_Fade_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetConfigFade()
        {
            ulong targetAddress = helper.GetBaseAddress(ConfigFade_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetMainOuterFade()
        {
            ulong targetAddress = helper.GetBaseAddress(MainOuterFade_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetConfigCursorPos()
        {
            ulong targetAddress = helper.GetBaseAddress(ConfigCursorPos_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        static public byte GetMainCursorPos()
        {
            ulong targetAddress = helper.GetBaseAddress(MainCursorPos_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public short GetCharacterScrollJ()
        {
            ulong targetAddress = helper.GetBaseAddress(CharacterScrollJ_Address);
            short value = helper.ReadMemory<short>(targetAddress);
            return value;
        }
        static public byte GetAbilityPosition()
        {
            ulong targetAddress = helper.GetBaseAddress(AbilityPosition_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetJunctionFade()
        {
            ulong targetAddress = helper.GetBaseAddress(JunctionFade_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        static public byte GetItemFade()
        {
            ulong targetAddress = helper.GetBaseAddress(ItemFade_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        /// <summary>
        /// Equals 16 if the scroll was finished
        /// </summary>
        /// <returns></returns>
        static public byte GetJunctionScroll()
        {
            ulong targetAddress = helper.GetBaseAddress(JunctionScroll_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        /// <summary>
        /// Equals 0 if not in menu. Equals 3 in menu and ready to move
        /// </summary>
        /// <returns></returns>
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
            return value == 1;
        }

        static public bool IsMenu()
        {
            ulong targetAddress = helper.GetBaseAddress(IsMenu_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
            // return value == 1;
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
            return value == 0;
        }

        static public int GetFieldX()
        {
            ulong targetAddress = helper.GetBaseAddress(FieldX_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            if (value < 32000 && value > -32000)
            {
                lastX = value;
            }
            return lastX;
        }
        static public int GetFieldY()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - FieldY_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            if (value < 32000 && value > -32000)
            {
                lastY = value;
            }
            return lastY;
            //byte[] bytes = helper.ReadMemoryBytes(targetAddress, 4);
            //bytes[3] = 0;
            /*
            foreach (byte b in bytes)
            {
                Console.WriteLine(b);
            }
            
            Console.WriteLine(value);
            */
            //int value = BitConverter.ToInt32(bytes, 0);

            //if (!BitConverter.IsLittleEndian)
            //Array.Reverse(bytes);

            //int value = BitConverter.ToInt32(bytes, 0);
            //Console.WriteLine(value);
            //return value;
        }
        public static ulong FieldZ_Address = 0x287778; // 1677240 18fe9b8

        static public int GetFieldZ()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - FieldZ_Address);
            int value = helper.ReadMemory<int>(targetAddress);
            if (value > short.MaxValue || value < short.MinValue)
            {
                return lastZ;
            }
            lastZ = value;
            return lastZ;

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
            ulong address = TextStatus_Address + 0x3C * (ulong)id;
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

        static public byte GetOptionChoice(int chanID = 0)
        {
            ulong targetAddress = helper.GetBaseAddress(OptionChoice_Address + 0x3C * (uint)chanID);
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

        static public bool IsBattleOrField()
        {
            ulong targetAddress = helper.GetBaseAddress(IsBattleOrField_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            if (value == 1)
                return true;
            else
                return false;
        }

        private static ulong PreviousMapID_Address = 0x54; // FF8_EN.exe+18FEA0C
        static public short GetPreviousMapID()
        {
            ulong targetAddress = helper.GetBaseAddress(PreviousMapID_Address + Language.BaseAddress);
            short value = helper.ReadMemory<short>(targetAddress);
            return value;
        }

        private static ulong BattleTarget_Address = 0x77ECC; // FF8_EN.exe+1976884 -

        static public byte GetBattleTarget()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + BattleTarget_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        private static ulong BattleCommand_Address = 0x77E8B; // FF8_EN.exe+1976843 -

        static public byte GetBattleCommand()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + BattleCommand_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }
        // 18fe9b8

        private static ulong MagicChoiceIndex_Address = 0x77F20; // FF8_EN.exe+19768D8 -
        static public byte GetMagicChoiceIndex()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + MagicChoiceIndex_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        private static ulong ChoiceMagicScale_Address = 0x77F39; // FF8_EN.exe+19768F1 - 

        /// <summary>
        /// Equals 16 when finished scaling
        /// </summary>
        /// <returns></returns>
        static public byte GetChoiceMagicScale()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + ChoiceMagicScale_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        private static ulong ChoiceStockCastScale_Address = 0x77F3B; // FF8_EN.exe+19768F3 -

        /// <summary>
        /// Equals 16 when finished scaling
        /// </summary>
        /// <returns></returns>
        static public byte GetChoiceStockCastScale()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + ChoiceStockCastScale_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        private static ulong CommandSelectedType_Address = 0x77EA8; // FF8_EN.exe+1976860 - 18fe9b8

        static public byte GetCommandSelectedType()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + CommandSelectedType_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value;
        }

        private static ulong CommandWindowScale_Address = 0x77E89; // FF8_EN.exe+1976841

        static public byte GetCommandWindowScale()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + CommandWindowScale_Address);
            return helper.ReadMemory<byte>(targetAddress);
        }

        

        private static ulong Targetting_Address = 0x285946; // FF8_EN.exe+1679072 18fe9b8

        static public bool IsTargetting()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress - Targetting_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);
            return value == 1;
        }

        private static ulong SquallItems_Address = 0x89B; // FF8_EN.exe+18FF253 18fe9b8

        static public byte GetSquall_MagicAmount(int slot = 0)
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + SquallItems_Address + (uint)slot * 0x5);
            byte value = helper.ReadMemory<byte>(targetAddress);
            Console.WriteLine("Magic amount: " + value);
            return value;
        }

        private static ulong BattleSelectedCharacter_Address = 0x77E8C; // FF8_EN.exe+1976844

        static public byte GetSelectedAlly()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + BattleSelectedCharacter_Address);
            return helper.ReadMemory<byte>(targetAddress);
        }

        private static ulong EnemyHealth_Address = 0x293E0; // FF8_EN.exe+1927D98

        static public short GetEnemyHealth(int enemyID = 0)
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + EnemyHealth_Address + (uint)enemyID * 0x0D);
            byte value = helper.ReadMemory<byte>(targetAddress);
            Console.WriteLine("EnemyHealth: " + value);
            return value;
        }

        private static ulong DialogueSize_Address = 0x2C995; // FF8_EN.exe+192B34D 18fe9b8

        static public byte GetDialogueSize(int chanID = 0)
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + DialogueSize_Address + (uint)chanID * 0x3C);
            return helper.ReadMemory<byte>(targetAddress);
        }

        private static ulong ATB_Status_Address = 0x2A433; // FF8_EN.exe+1928DEB 18fe9b8
        static public bool IsATB_Active()
        {
            ulong targetAddress = helper.GetBaseAddress(Language.BaseAddress + ATB_Status_Address);
            byte value = helper.ReadMemory<byte>(targetAddress);

            return value == 1;
        }
        // FF8_EN.exe+1928DEB

        // FF8_EN.exe+1927D98  FF8_EN.exe+1927E68 　FF8_EN.exe+1927F38
        // next slot = 0x5
    }
}
