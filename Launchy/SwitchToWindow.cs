using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Launchy
{
    public class SwitchToWindow
    {
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);

        public static void Switch(string processName)
        {
            Process[] procs = Process.GetProcessesByName(processName);
            List<Process> ps = procs.Where(v => v.MainWindowHandle.ToInt32() != 0).ToList();
            if (ps.Count > 0)
            {
                SwitchToThisWindow(ps.First().MainWindowHandle, false);
            }
        }

        public static void Switch(string processName, string tabName)
        {
            throw new NotImplementedException();
        }
    }
}
