using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy
{
    public class StartExplorerAtFolder
    {
        public static void Start(string folderName)
        {
            Process.Start("explorer.exe", folderName);
        }
    }
}
