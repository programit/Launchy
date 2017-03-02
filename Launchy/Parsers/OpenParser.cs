using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy.Parsers
{
    public class OpenParser : BaseParser
    {
        private readonly FileSystemWatcher watcher;

        public readonly Dictionary<string, string> items = new Dictionary<string, string>();

        private const string FileName = "open.txt";

        public OpenParser()
        {
            this.watcher = new FileSystemWatcher(".\\", "open.txt")
            {
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite
            };

            this.watcher.Changed += Watcher_Changed;
            this.watcher.EnableRaisingEvents = true;
        }

        public override void Setup()
        {
            if(!File.Exists(OpenParser.FileName))
            {
                using (File.Create(OpenParser.FileName)) { }
            }

            string[] lines = File.ReadAllLines(OpenParser.FileName);
            this.items.Clear();
            this.phrases.Clear();

            foreach(string line in lines)
            {
                string[] parts = line.Split(',');
                if(parts.Length != 2)
                {
                    Trace.TraceError("Failed to parse line from open command: " + line);
                    continue;
                }

                this.phrases.Add(parts[0]);
                this.items.Add(parts[0], parts[1]);
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.Setup();
        }
    }
}
