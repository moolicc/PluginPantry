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
    public class RunAction_Tests
    {
        [Test]
        public void RunAction_NoReturn_IgnoresParam_Executes()
        {
            Type entryPointType = typeof(ActionPlugins);
            MethodInfo entryPoint = entryPointType.GetMethod("MainEntryPoint")!;
            PluginResolver resolver = new PluginResolver();

            var plugin = resolver.LoadFromEntryPoint(entryPoint);
            var context = new PluginContext();

            IEnumerable<ActionResult> results = Array.Empty<ActionResult>();
            Assert.DoesNotThrow(() =>
            {
                context.RegisterPlugin(plugin);
                results = context.RunAction(new ParamAction());
            });

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.IsTrue(results.ElementAt(0).Success);
            Assert.IsNull(results.ElementAt(0).ReturnValue);
            Assert.IsNull(results.ElementAt(0).Exception);
        }

        [Test]
        public void RunAction_NoReturn_AcceptsParam_Executes()
        {
            Type entryPointType = typeof(ActionPlugins);
            MethodInfo entryPoint = entryPointType.GetMethod("MainEntryPoint2")!;
            PluginResolver resolver = new PluginResolver();

            var plugin = resolver.LoadFromEntryPoint(entryPoint);
            var context = new PluginContext();

            IEnumerable<ActionResult> results = Array.Empty<ActionResult>();
            Assert.DoesNotThrow(() =>
            {
                context.RegisterPlugin(plugin);
                results = context.RunAction(new ParamAction());
            });

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.IsTrue(results.ElementAt(0).Success);
            Assert.IsNull(results.ElementAt(0).ReturnValue);
            Assert.IsNull(results.ElementAt(0).Exception);
        }

        [Test]
        public void RunAction_NoReturn_AcceptsExplicitParam_Executes()
        {
            Type entryPointType = typeof(ActionPlugins);
            MethodInfo entryPoint = entryPointType.GetMethod("MainEntryPoint3")!;
            PluginResolver resolver = new PluginResolver();

            var plugin = resolver.LoadFromEntryPoint(entryPoint);
            var context = new PluginContext();

            IEnumerable<ActionResult> results = Array.Empty<ActionResult>();
            Assert.DoesNotThrow(() =>
            {
                context.RegisterPlugin(plugin);
                results = context.RunAction(new ParamAction());
            });

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.IsTrue(results.ElementAt(0).Success);
            Assert.IsNull(results.ElementAt(0).ReturnValue);
            Assert.IsNull(results.ElementAt(0).Exception);
        }

        [Test]
        public void RunAction_NoReturn_MissingParam_ReturnsFail()
        {
            Type entryPointType = typeof(ActionPlugins);
            MethodInfo entryPoint = entryPointType.GetMethod("MainEntryPoint4")!;
            PluginResolver resolver = new PluginResolver();

            var plugin = resolver.LoadFromEntryPoint(entryPoint);
            var context = new PluginContext();

            IEnumerable<ActionResult> results = Array.Empty<ActionResult>();
            Assert.DoesNotThrow(() =>
            {
                context.RegisterPlugin(plugin);
                results = context.RunAction(new ParamAction());
            });

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.IsFalse(results.ElementAt(0).Success);
            Assert.IsNull(results.ElementAt(0).ReturnValue);
            Assert.NotNull(results.ElementAt(0).Exception);
        }



        [Test]
        public void RunAction_IntReturn_IgnoresParam_Executes()
        {
            Type entryPointType = typeof(ActionPlugins);
            MethodInfo entryPoint = entryPointType.GetMethod("MainEntryPoint5")!;
            PluginResolver resolver = new PluginResolver();

            var plugin = resolver.LoadFromEntryPoint(entryPoint);
            var context = new PluginContext();

            IEnumerable<ActionResult> results = Array.Empty<ActionResult>();
            Assert.DoesNotThrow(() =>
            {
                context.RegisterPlugin(plugin);
                results = context.RunAction(new ParamAction());
            });

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.IsTrue(results.ElementAt(0).Success);
            Assert.IsNotNull(results.ElementAt(0).ReturnValue);
            Assert.That(results.ElementAt(0).ReturnValue, Is.TypeOf<int>());
            Assert.That(results.ElementAt(0).ReturnValue, Is.EqualTo(-1));
            Assert.IsNull(results.ElementAt(0).Exception);
        }









    }
}
