using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantryTests.Plugins
{
    internal class ActionPlugins
    {

        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainEntryPoint(PluginContext context, Guid pluginId)
        {
            context.RegisterAction<ParamAction, ActionPlugins>(pluginId, nameof(StaticTestNoParamNoReturn));
        }


        [EntryPoint("name", "myplugin2", "version", "1.0")]
        public static void MainEntryPoint2(PluginContext context, Guid pluginId)
        {
            context.RegisterAction<ParamAction, ActionPlugins>(pluginId, nameof(StaticTestImplicitParamNoReturn));
        }

        [EntryPoint("name", "myplugin3", "version", "1.0")]
        public static void MainEntryPoint3(PluginContext context, Guid pluginId)
        {
            context.RegisterAction<ParamAction, ActionPlugins>(pluginId, nameof(StaticTestExplicitParamNoReturn));
        }

        [EntryPoint("name", "myplugin4", "version", "1.0")]
        public static void MainEntryPoint4(PluginContext context, Guid pluginId)
        {
            context.RegisterAction<ParamAction, ActionPlugins>(pluginId, nameof(StaticTestMissingParamNoReturn));
        }

        [EntryPoint("name", "myplugin5", "version", "1.0")]
        public static void MainEntryPoint5(PluginContext context, Guid pluginId)
        {
            context.RegisterAction<ParamAction, ActionPlugins>(pluginId, nameof(StaticTestNoParamWithReturn));
        }

        public static void StaticTestNoParamNoReturn()
        {

        }

        public static int StaticTestNoParamWithReturn()
        {
            return -1;
        }

        public static void StaticTestImplicitParamNoReturn(string text)
        {

        }

        public static void StaticTestExplicitParamNoReturn(ParamAction model)
        {

        }

        public static void StaticTestMissingParamNoReturn(int i)
        {

        }
    }
}
