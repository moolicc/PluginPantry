using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    public delegate bool PluginValidCallback(Dictionary<string, string> arguments);

    public class PluginMetadataSchema
    {
        public static readonly PluginMetadataSchema Default = new PluginMetadataSchema("version", "name");

        public List<string> RequiredKeys { get; private set; }
        public PluginValidCallback? IsValidCallback { get; set; }

        public PluginMetadataSchema(params string[] requiredKeys)
        {
            RequiredKeys = new List<string>(requiredKeys);
        }

        public PluginMetadataSchema(PluginValidCallback isValidCallback)
            : this()
        {
            IsValidCallback = isValidCallback;
        }
    }
}
