using PluginPantryTests.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace PluginPantryTests.PluginContextTests
{
    [TestFixture]
    public class GetExtensions_Tests
    {
        [Test]
        public void GetExtensions()
        {
            Type entryPointType = typeof(ExtensionPlugins);
            MethodInfo entryPoint = entryPointType.GetMethod("MainEntryPoint")!;
            PluginResolver resolver = new PluginResolver();

            var plugin = resolver.LoadFromEntryPoint(entryPoint);
            var context = new PluginContext();


            IEnumerable<ImplBase> results = Array.Empty<ImplBase>();
            Assert.DoesNotThrow(() =>
            {
                context.RegisterPlugin(plugin);
                results = context.GetExtensions<ImplBase>();
            });

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.ElementAt(0), Is.TypeOf<Impl>());
        }

    }
}
