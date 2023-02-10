
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry.Tests.PluginContextTests
{
    [TestFixture]
    public class RegisterPlugin_Tests
    {
        private PluginContext _context;

        private PluginMetadata[] _loadedPlugins;


        [SetUp]
        protected void Setup()
        {
            _context = new PluginContext();


            Type entryPointNoParamsType = typeof(Plugins.EntryPointNoParams);
            PluginResolver resolver = new PluginResolver();
            _loadedPlugins = resolver.LoadFromType(entryPointNoParamsType).ToArray();
        }

        [Test]
        public void RegisterPlugin_CalledWithoutParams_EntryPointWithoutParamsInvoked()
        {
        }

        [Test]
        public void RegisterPlugin_CalledWithoutParams_EntryPointWithParamsFails()
        {
        }


        [Test]
        public void RegisterPlugin_CalledWithParams_EntryPointWithoutParamsFails()
        {
        }

        [Test]
        public void RegisterPlugin_CalledWithParams_EntryPointWithParamsInvoked()
        {
        }
    }
}
