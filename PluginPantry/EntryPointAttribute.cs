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

        public EntryPointAttribute(params KeyValuePair<string, string>[] args)
        {
            Parameters = args;
        }


        public EntryPointAttribute(params (string Key, string Value)[] args)
        {
            Parameters = args.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)).ToArray();
        }
    }
}
