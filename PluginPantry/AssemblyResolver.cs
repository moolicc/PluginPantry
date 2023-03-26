using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    internal class AssemblyResolver
    {
        public static Assembly LoadAssembly(string baseDirectory, string assemblyFile)
        {
            var context = new PluginLoadContext(baseDirectory);
            return context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFile)));
        }
    }
}
