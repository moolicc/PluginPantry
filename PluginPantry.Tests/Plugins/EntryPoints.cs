using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantryTests.Plugins
{
    public class EntryPoints
    {
        public static bool Called = false;



        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainNoParams(PluginContext context, Guid id)
        {
            Called = true;
        }


        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainWithParams(PluginContext context, Guid id, string s1)
        {
            Called = true;
        }
    }
}
