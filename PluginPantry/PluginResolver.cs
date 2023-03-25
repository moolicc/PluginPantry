using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;

namespace PluginPantry
{
    public class PluginResolver
    {
        public bool AllowExternalMetadata { get; set; }
        public PluginMetadataSchema MetadataSchema { get; set; }

        public PluginResolver()
        {
            AllowExternalMetadata = true;
            MetadataSchema = PluginMetadataSchema.Default;
        }

        public IEnumerable<PluginMetadata> LoadPlugins(Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                foreach (var method in type.GetMethods())
                {
                    var attrib = method.GetCustomAttribute<EntryPointAttribute>();
                    if (attrib == null)
                    {
                        continue;
                    }

                    // Return this plugin's information.
                    yield return LoadFromEntryPoint(method);
                }
            }
        }


        public IEnumerable<PluginMetadata> LoadFromType(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                var attrib = method.GetCustomAttribute<EntryPointAttribute>();
                if (attrib == null)
                {
                    continue;
                }

                // Return this plugin's information.
                yield return LoadFromEntryPoint(method);
            }
        }

        public PluginMetadata LoadFromEntryPoint(MethodInfo method)
        {
            var attrib = method.GetCustomAttribute<EntryPointAttribute>();

            Dictionary<string, string> parameters;
            if (attrib == null || attrib.Parameters.Length == 0)
            {
                parameters = new Dictionary<string, string>();
            }
            else
            {
                parameters = new Dictionary<string, string>(attrib.Parameters);
            }

            // Is the metadata valid according to the schema?
            if (MetadataSchema.IsValidCallback != null)
            {
                if (!MetadataSchema.IsValidCallback(parameters))
                {
                    throw new InvalidOperationException("Plugin has invalid signature.");
                }
            }
            else
            {
                foreach (var reqArg in MetadataSchema.RequiredKeys)
                {
                    if (!parameters.ContainsKey(reqArg))
                    {
                        throw new InvalidOperationException("Plugin is missing required argument.");
                    }
                }
            }

            if (!method.IsStatic)
            {
                throw new InvalidOperationException("Plugin is entry point must be static.");
            }

            return new PluginMetadata(parameters, method);
        }
    }
}
