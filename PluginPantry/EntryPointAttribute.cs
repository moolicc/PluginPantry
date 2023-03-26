using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class EntryPointAttribute : Attribute
    {
        public KeyValuePair<string, string>[] Parameters { get; }

        public EntryPointAttribute()
        {
            Parameters = new KeyValuePair<string, string>[0];
        }

        public EntryPointAttribute(params string[] args)
        {
            if(args.Length % 2 != 0)
            {
                throw new PluginException("Invalid metadata. Metadata must be provided in pairs of strings representing keys and their corresponding values.");
            }

            Parameters = new KeyValuePair<string, string>[args.Length / 2];
            for (int i = 0; i < args.Length; i += 2)
            {
                Parameters[i / 2] = new KeyValuePair<string, string>(args[i], args[i + 1]);
            }
        }
    }
}
