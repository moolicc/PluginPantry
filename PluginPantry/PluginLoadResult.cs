using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    public readonly record struct PluginLoadResult
    {
        public PluginMetadata? LoadedData { get; init; }
        public Exception? Exception { get; init; }
        public bool Success => Exception == null;
    }
}
