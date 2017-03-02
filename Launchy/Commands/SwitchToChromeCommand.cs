using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Launchy.Commands
{
    public class SwitchToChromeCommand : ICommand
    {
        private const string ProcessName = "chrome";

        public void Execute(string phrase)
        {            
            SwitchToWindow.Switch(SwitchToChromeCommand.ProcessName);
        }
                
        public string[] GetPhrases()
        {
            return new string[]
            {
                "chrome"
            };
        }
    }
}
