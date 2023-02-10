using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    internal class ActionTable<TAction>
    {
        private static readonly Dictionary<PluginContext, ActionTable<TAction>> _instances;

        private Dictionary<Guid, List<PluginAction>> _extensionActions;

        static ActionTable()
        {
            _instances = new Dictionary<PluginContext, ActionTable<TAction>>();
        }

        internal static void ClearTable(PluginContext context)
        {
            _instances.Remove(context);
        }

        internal static ActionTable<TAction> ForPluginContext(PluginContext context)
        {
            if (!_instances.TryGetValue(context, out var table))
            {
                table = new ActionTable<TAction>();
                _instances.Add(context, table);
            }
            return table;
        }

        private ActionTable()
        {
            _extensionActions = new Dictionary<Guid, List<PluginAction>>();
        }

        internal void RemoveActions(Guid pluginId)
        {
            _extensionActions.Remove(pluginId);
        }

        internal void AddAction(PluginAction action)
        {
            if (_extensionActions.TryGetValue(action.PluginId, out var table))
            {
                table.Add(action);
            }
            else
            {
                _extensionActions.Add(action.PluginId, new List<PluginAction> { action });
            }
        }

        public IEnumerable<PluginAction> GetActions()
        {
            foreach (var item in _extensionActions)
            {
                foreach (var instance in item.Value)
                {
                    yield return instance;
                }
            }
        }

        public IEnumerable<PluginAction> GetExtensions(Guid pluginGuid)
        {
            var items = _extensionActions[pluginGuid];
            foreach (var instance in items)
            {
                yield return instance;
            }
        }
    }
}
