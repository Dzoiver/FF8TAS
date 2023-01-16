using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF8TAS
{
    class Player
    {
        public Process ff8Process;
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
            SendKeys.SendWait("{k}");
        }


    }
}
