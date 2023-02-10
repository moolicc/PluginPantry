using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{
    public class PluginContext
    {
        private List<PluginMetadata> _plugins;

        public PluginContext()
        {
            _plugins = new List<PluginMetadata>
            {
                // TODO: This could be an error. The explicit !s.
                new PluginMetadata(Guid.Empty, new Dictionary<string, string>(), Assembly.GetEntryAssembly()!.EntryPoint!)
            };
        }

        public IEnumerable<PluginMetadata> GetPlugins()
            => _plugins.ToArray();

        public void RegisterPlugin(PluginMetadata metadata, params object[] args)
        {
            _plugins.Add(metadata);
            metadata.EntryPoint.Invoke(null, args);
        }


        public void RegisterAction<TAction, TOwner>(Guid pluginId, TOwner? instance, string functionName)
        {
            Type type = typeof(TOwner);
            MethodInfo? method = type.GetMethod(functionName);

            if(method == null)
            {
                throw new InvalidOperationException("Failed to bind action. Method not found.");
            }

            PluginAction action = new PluginAction()
            {
                Instance = instance,
                Method = method,
                PluginId = pluginId
            };
            RegisterAction<TAction>(action);
        }

        private void RegisterAction<TAction>(PluginAction action)
        {
            ActionTable<TAction>.ForPluginContext(this).AddAction(action);
        }

        public void RegisterExtension<TBase, TImpl>(Guid pluginId, TImpl implementation) where TImpl : TBase
        {
            ExtensionTable<TBase>.ForPluginContext(this).AddExtension(pluginId, implementation);
        }




        public IEnumerable<ActionResult<TActionReturn>> RunAction<TAction, TActionReturn>(TAction model)
        {
            foreach (var action in ActionTable<TAction>.ForPluginContext(this).GetActions())
            {
                ActionResult<TActionReturn> result;
                try
                {
                    var returnVal = Util.TryInvokeMatchingMethod(action.Method, action.Instance, model);
                    if (returnVal.Status != MethodInvocationResults.Succeeded)
                    {
                        throw new InvalidOperationException($"Failed to execute action for plugin {action.PluginId}.");
                    }

                    result = new ActionResult<TActionReturn>
                    {
                        Exception = null,
                        ReturnValue = (TActionReturn?)returnVal.ReturnVal
                    };
                }
                catch (Exception ex)
                {
                    result = new ActionResult<TActionReturn>
                    { Exception = ex };
                }
                yield return result;
            }
        }

        public void RunAction<TAction, TActionReturn>(TAction model, Action<ActionResult<TActionReturn>> onActionComplete)
        {
            foreach (var action in ActionTable<TAction>.ForPluginContext(this).GetActions())
            {
                ActionResult<TActionReturn> result;
                try
                {
                    var returnVal = Util.TryInvokeMatchingMethod(action.Method, action.Instance, model);
                    if (returnVal.Status != MethodInvocationResults.Succeeded)
                    {
                        throw new InvalidOperationException($"Failed to execute action for plugin {action.PluginId}.");
                    }

                    result = new ActionResult<TActionReturn>
                    {
                        Exception = null,
                        ReturnValue = (TActionReturn?)returnVal.ReturnVal
                    };
                }
                catch (Exception ex)
                {
                    result = new ActionResult<TActionReturn>
                    { Exception = ex };
                }
                onActionComplete(result);
            }
        }





        public IEnumerable<TBase> GetExtensions<TBase>()
        {
            return ExtensionTable<TBase>.ForPluginContext(this).GetExtensions();
        }

        public void WithExtensions<TBase>(Action<TBase> action)
        {
            foreach(var item in ExtensionTable<TBase>.ForPluginContext(this).GetExtensions())
            {
                action(item);
            }
        }
    }
}
