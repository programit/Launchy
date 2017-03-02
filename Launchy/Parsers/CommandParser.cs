using FuzzyString;
using Launchy.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy
{
    public class CommandParser : BaseParser
    {
        private readonly ICommand[] commands;

        public CommandParser(ICommand[] commands)
        {
            this.commands = commands;
        }                

        public override void Setup()
        {
            foreach (ICommand command in this.commands)
            {
                foreach(string s in command.GetPhrases())
                {
                    string lowerOfString = s.ToLower();
                    this.phrases.Add(lowerOfString);                    
                }                
            }
        }
    }
}