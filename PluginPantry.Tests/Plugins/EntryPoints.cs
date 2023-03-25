using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry.Tests.Plugins
{
    public class EntryPoints
    {
        public static bool Called = false;



        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainNoParams()
        {
            Called = true;
        }


        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void Main(string s1)
        {
            Called = true;
        }
    }
}
