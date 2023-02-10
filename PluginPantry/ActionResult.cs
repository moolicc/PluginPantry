using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    public readonly record struct ActionResult<TReturn>
    {
        public TReturn? ReturnValue { get; init; }
        public Exception? Exception { get; init; }
        public bool Success => Exception == null;
    }

    public readonly record struct ActionResult
    {
        public object? ReturnValue { get; init; }
        public Exception? Exception { get; init; }
        public bool Success => Exception == null;
    }
}
