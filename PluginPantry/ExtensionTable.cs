using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    internal class ExtensionTable<TBase>
    {
        private static readonly Dictionary<PluginContext, ExtensionTable<TBase>> _instances;

        private Dictionary<Guid, List<object>> _extensionImplementations;

        static ExtensionTable()
        {
            _instances = new Dictionary<PluginContext, ExtensionTable<TBase>>();
        }

        internal static void ClearTable(PluginContext context)
        {
            _instances.Remove(context);
        }

        internal static ExtensionTable<TBase> ForPluginContext(PluginContext context)
        {
            if (!_instances.TryGetValue(context, out var table))
            {
                table = new ExtensionTable<TBase>();
                _instances.Add(context, table);
            }
            return table;
        }

        private ExtensionTable()
        {
            _extensionImplementations = new Dictionary<Guid, List<object>>();
        }

        internal void RemoveActions(Guid pluginId)
        {
            _extensionImplementations.Remove(pluginId);
        }

        internal void AddExtension<TSub>(Guid pluginId, TSub instance) where TSub : TBase
        {
            if (_extensionImplementations.TryGetValue(pluginId, out var table))
            {
                table.Add(instance!);
            }
            else
            {
                _extensionImplementations.Add(pluginId, new List<object> { instance });
            }
        }

        public IEnumerable<TBase> GetExtensions()
        {
            foreach (var item in _extensionImplementations)
            {
                foreach (var instance in item.Value)
                {
                    yield return (TBase)instance;
                }
            }
        }

        public IEnumerable<TBase> GetExtensions(Guid pluginGuid)
        {
            var items = _extensionImplementations[pluginGuid];
            foreach (var instance in items)
            {
                yield return (TBase)instance;
            }
        }
    }
}
