using Launchy.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy.Commands
{
    public class OpenCommand : ICommand
    {
        private readonly OpenParser parser = new OpenParser();

        public OpenCommand()
        {
            this.parser.Setup();
        }

        public void Execute(string phrase)
        {            
            string match = this.parser.FuzzyMatch(phrase);
            string path;
            if(this.parser.items.TryGetValue(match, out path))
            {
                StartExplorerAtFolder.Start(path);
            }
        }

        public string[] GetPhrases()
        {
            return new string[]
            {
                "open",
                "explore",
                "o"
            };
        }
    }
}
