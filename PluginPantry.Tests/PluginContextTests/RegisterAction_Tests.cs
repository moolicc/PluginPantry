using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PluginPantryTests.PluginContextTests
{
    [TestFixture]
    public class RegisterAction_Tests
    {
        private PluginContext _context;

        private PluginMetadata[] _loadedPlugins;



        [SetUp]
        protected void Setup()
        {
            _context = new PluginContext();


            Type entryPointNoParamsType = typeof(Plugins.EntryPoints);
            PluginResolver resolver = new PluginResolver();
            _loadedPlugins = resolver.LoadFromType(entryPointNoParamsType).ToArray();

        }
    }
}
