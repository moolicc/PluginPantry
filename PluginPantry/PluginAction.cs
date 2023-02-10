using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    internal record struct PluginAction
    {
        public object? Instance;
        public MethodInfo Method;
        public Guid PluginId;
    }
}
