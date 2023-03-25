using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    public class PluginMetadata
    {
        public Guid Uid { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public MethodInfo EntryPoint { get; private set; }

        internal bool EntryPointContextFirst;

        private bool _executed;


        internal PluginMetadata(Dictionary<string, string> parameters, MethodInfo entryPoint)
            : this(Guid.NewGuid(), parameters, entryPoint)
        {
        }

        internal PluginMetadata(Guid uid, Dictionary<string, string> parameters, MethodInfo entryPoint)
        {
            Uid = uid;
            Parameters = parameters;
            EntryPoint = entryPoint;
        }

        public void ExecuteEntryPoint(params object[] args)
        {
            if(_executed)
            {
                throw new InvalidOperationException("Plugin entry point already invoked!");
            }
            _executed = true;
            EntryPoint?.Invoke(this, args);
        }
    }
}
