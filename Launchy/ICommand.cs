using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy
{
    public interface ICommand
    {
        string[] GetPhrases();

        void Execute(string phrase);
    }
}
