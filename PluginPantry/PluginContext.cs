using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var entryAssembly = Assembly.GetEntryAssembly();
            if(entryAssembly == null)
            {
                throw new PluginException("Failed to find entry assembly for default plugin.");
            }
            if(entryAssembly.EntryPoint == null)
            {
                throw new PluginException("Failed to find program entry point for default plugin.");
            }

            _plugins = new List<PluginMetadata>
            {
                new PluginMetadata(Guid.Empty, new Dictionary<string, string>(), entryAssembly.EntryPoint)
            };
        }

        public IEnumerable<PluginMetadata> GetPlugins()
            => _plugins.ToArray();

        public void RegisterPlugin(PluginMetadata metadata, params object[] args)
        {
            try
            {
                var p = new object[args.Length + 2];

                if(metadata.EntryPointContextFirst)
                {
                    p[0] = this;
                    p[1] = metadata.Uid;
                }
                else
                {
                    p[0] = metadata.Uid;
                    p[1] = this;
                }
                Array.Copy(args, 0, p, 2, args.Length);
                
                metadata.ExecuteEntryPoint(p);
            }
            catch (Exception ex)
            {
                throw new PluginException("Failed to invoke plugin entry point.", ex);
            }
            _plugins.Add(metadata);
        }

        public void RegisterAction<TAction, TOwner>(Guid pluginId, string functionName, TOwner? owningInstance = default)
            => RegisterAction<TAction>(pluginId, functionName, typeof(TOwner), owningInstance);

        public void RegisterAction<TAction>(Guid pluginId, string functionName, Type owningType, object? owningInstance = null)
        {
            MethodInfo? method = owningType.GetMethod(functionName);

            if(method == null)
            {
                throw new PluginException("Failed to bind action. Method not found.");
            }

            PluginAction action = new PluginAction()
            {
                Instance = owningInstance,
                Method = method,
                PluginId = pluginId
            };
            RegisterAction<TAction>(action);
        }

        private void RegisterAction<TAction>(PluginAction action)
        {
            ActionTable<TAction>.ForPluginContext(this).AddAction(action);
        }


        public void RegisterExtension<TBase, TImpl>(Guid pluginId) where TImpl : TBase
        {
            RegisterExtension<TBase, TImpl>(pluginId, Activator.CreateInstance<TImpl>());
        }

        public void RegisterExtension<TBase, TImpl>(Guid pluginId, TImpl implementation) where TImpl : TBase
        {
            ExtensionTable<TBase>.ForPluginContext(this).AddExtension(pluginId, implementation);
        }



        public IEnumerable<ActionResult> RunAction<TAction>(TAction model)
        {
            foreach (var action in ActionTable<TAction>.ForPluginContext(this).GetActions())
            {
                ActionResult result;
                try
                {
                    var returnVal = Util.TryInvokeMatchingMethod(action.Method, action.Instance, model);
                    if (returnVal.Status != MethodInvocationResults.Succeeded)
                    {
                        throw new PluginException($"Failed to execute action for plugin {action.PluginId}.");
                    }

                    result = new ActionResult
                    {
                        Exception = null,
                        ReturnValue = returnVal.ReturnVal,
                    };
                }
                catch (Exception ex)
                {
                    result = new ActionResult
                    { Exception = ex };
                }
                yield return result;
            }
        }

        public void RunAction<TAction>(TAction model, Action<ActionResult> onActionComplete)
        {
            foreach (var action in ActionTable<TAction>.ForPluginContext(this).GetActions())
            {
                ActionResult result;
                try
                {
                    var returnVal = Util.TryInvokeMatchingMethod(action.Method, action.Instance, model);
                    if (returnVal.Status != MethodInvocationResults.Succeeded)
                    {
                        throw new PluginException($"Failed to execute action for plugin {action.PluginId}.");
                    }

                    result = new ActionResult
                    {
                        Exception = null,
                        ReturnValue = returnVal.ReturnVal
                    };
                }
                catch (Exception ex)
                {
                    result = new ActionResult
                    { Exception = ex };
                }
                onActionComplete(result);
            }
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
                        throw new PluginException($"Failed to execute action for plugin {action.PluginId}.");
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
                        throw new PluginException($"Failed to execute action for plugin {action.PluginId}.");
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
