using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy.Commands
{
    public class ExitCommand : ICommand
    {
        public void Execute(string phrase)
        {
            Environment.Exit(0);
        }

        public string[] GetPhrases()
        {
            return new string[]
            {
                "exit",
                "quit"
            };
        }
    }
}
