
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


            Type entryPointNoParamsType = typeof(Plugins.EntryPoints);
            PluginResolver resolver = new PluginResolver();
            _loadedPlugins = resolver.LoadFromType(entryPointNoParamsType).ToArray();

        }

        [Test]
        public void RegisterPlugin_CalledWithoutParams_EntryPointWithoutParams_Invoked()
        {
            Plugins.EntryPoints.Called = false;

            Assert.DoesNotThrow(() =>
            {
                _context.RegisterPlugin(_loadedPlugins[0]);
            });

            Assert.IsTrue(Plugins.EntryPoints.Called);
        }

        [Test]
        public void RegisterPlugin_CalledWithoutParams_EntryPointWithParams_Fails()
        {
            Plugins.EntryPoints.Called = false;

            Assert.Throws<InvalidOperationException>(() =>
            {
                _context.RegisterPlugin(_loadedPlugins[1]);
            });

            Assert.IsFalse(Plugins.EntryPoints.Called);
        }


        [Test]
        public void RegisterPlugin_CalledWithParams_EntryPointWithoutParams_Fails()
        {
            Plugins.EntryPoints.Called = false;

            Assert.Throws<InvalidOperationException>(() =>
            {
                _context.RegisterPlugin(_loadedPlugins[0], "True");
            });

            Assert.IsFalse(Plugins.EntryPoints.Called);
        }

        [Test]
        public void RegisterPlugin_CalledWithParams_EntryPointWithParams_Invoked()
        {
            Plugins.EntryPoints.Called = false;

            Assert.DoesNotThrow(() =>
            {
                _context.RegisterPlugin(_loadedPlugins[1], "Test");
            });

            Assert.IsTrue(Plugins.EntryPoints.Called);
        }

        [Test]
        public void RegisterPlugin_CalledWithParams_EntryPointWithParamsButBadType_Fails()
        {
            Plugins.EntryPoints.Called = false;

            Assert.Throws<InvalidOperationException>(() =>
            {
                _context.RegisterPlugin(_loadedPlugins[1], 0);
            });

            Assert.IsFalse(Plugins.EntryPoints.Called);
        }
    }
}
