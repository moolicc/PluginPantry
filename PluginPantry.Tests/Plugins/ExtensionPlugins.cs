using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantryTests.Plugins
{
    internal class ExtensionPlugins
    {
        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainEntryPoint(PluginContext context, Guid pluginId)
        {
            context.RegisterExtension<ImplBase, Impl>(pluginId, new Impl());
        }

    }
}
