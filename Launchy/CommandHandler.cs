using Launchy.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy
{
    public class CommandHandler
    {
        private readonly CommandParser parser;
        private readonly Dictionary<string, ICommand> phraseToCommand = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase);

        private static readonly ICommand[] commands = new ICommand[]
        {
            new SwitchToChromeCommand(),
            new OpenCommand(),
            new ExitCommand(),
        };

        public CommandHandler()
        {
            this.parser = new CommandParser(CommandHandler.commands);
            this.parser.Setup();

            foreach (ICommand command in CommandHandler.commands)
            {
                foreach (string s in command.GetPhrases())
                {
                    string lowerOfString = s.ToLower();
                    if (phraseToCommand.ContainsKey(lowerOfString))
                    {
                        Trace.TraceError("We cannot add same phrase twice!! Investigate.");
                        continue;
                    }

                    this.phraseToCommand.Add(lowerOfString, command);
                }
            }
        }

        public string FuzzyMatch(string text)
        {
            int index = text.IndexOf(' ');
            if (index == -1)
            {
                return string.Empty;
            }

            string commandText = text.Substring(0, index);
            return parser.FuzzyMatch(commandText);
        }
        
        public void Parse(string text)
        {
            int index = text.IndexOf(' ');
            if (index == -1)
            {
                return;
            }

            string commandText = text.Substring(0, index);
            string commandArguments = text.Substring(1 + index);

            string result = parser.FuzzyMatch(commandText);
            ICommand command;
            if(!this.phraseToCommand.TryGetValue(result, out command))
            {
                return;
            }

            command.Execute(commandArguments);
        } 
    }
}
