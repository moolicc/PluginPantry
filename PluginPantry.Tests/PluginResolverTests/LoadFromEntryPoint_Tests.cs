using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry.Tests.PluginResolverTests
{
    [TestFixture]
    public class LoadFromEntryPoint_Tests
    {
        [Test]
        public void LoadFromEntryPoint_WithoutAttribute_NonStatic_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("NonStatic")!;
            PluginResolver resolver = new PluginResolver();


            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithoutAttribute_NoScheme_Loads()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithNoAttribute")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema();


            var loaded = resolver.LoadFromEntryPoint(entryPoint);


            Assert.Multiple(() =>
            {
                Assert.IsNotNull(loaded);
                Assert.IsEmpty(loaded.Parameters);
                Assert.That(entryPoint, Is.EqualTo(loaded.EntryPoint));
                Assert.That(loaded.Uid, Is.Not.EqualTo(Guid.Empty));
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithoutAttribute_ConcreteScheme_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithNoAttribute")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema("testkey");

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithoutAttribute_DynamicScheme_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithNoAttribute")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema(new PluginValidCallback(_ => false));

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }





        [Test]
        public void LoadFromEntryPoint_WithEmptyAttribute_NonStatic_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("NonStaticWithEmptyAttrib")!;
            PluginResolver resolver = new PluginResolver();


            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithEmptyAttribute_NoScheme_Loads()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithEmptyAttribute")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema();


            var loaded = resolver.LoadFromEntryPoint(entryPoint);


            Assert.Multiple(() =>
            {
                Assert.IsNotNull(loaded);
                Assert.IsEmpty(loaded.Parameters);
                Assert.That(entryPoint, Is.EqualTo(loaded.EntryPoint));
                Assert.That(loaded.Uid, Is.Not.EqualTo(Guid.Empty));
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithEmptyAttribute_ConcreteScheme_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithEmptyAttribute")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema("testkey");

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithEmptyAttribute_DynamicScheme_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithEmptyAttribute")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema(new PluginValidCallback(_ => false));

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }





        [Test]
        public void LoadFromEntryPoint_WithAttribute_InvalidMetadata_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithBadMetada")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema();

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithAttribute_NoScheme_Loads()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithGoodMetadata")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema();


            var loaded = resolver.LoadFromEntryPoint(entryPoint);


            Assert.Multiple(() =>
            {
                Assert.IsNotNull(loaded);
                Assert.That(entryPoint, Is.EqualTo(loaded.EntryPoint));
                Assert.That(loaded.Uid, Is.Not.EqualTo(Guid.Empty));
                Assert.That(loaded.Parameters, Contains.Key("name"));
                Assert.That(loaded.Parameters, Contains.Key("version"));
                Assert.That(loaded.Parameters, Contains.Value("myplugin"));
                Assert.That(loaded.Parameters, Contains.Value("1.0"));
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithAttribute_InvalidConcreteScheme_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithGoodMetadata")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema("testkey");

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithAttribute_InvalidDynamicScheme_Fails()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithGoodMetadata")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema(new PluginValidCallback(_ => false));

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.LoadFromEntryPoint(entryPoint);
            });
        }



        [Test]
        public void LoadFromEntryPoint_WithAttribute_ValidConcreteScheme_Loads()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithGoodMetadata")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema("name", "version");

            var loaded = resolver.LoadFromEntryPoint(entryPoint);


            Assert.Multiple(() =>
            {
                Assert.IsNotNull(loaded);
                Assert.That(entryPoint, Is.EqualTo(loaded.EntryPoint));
                Assert.That(loaded.Uid, Is.Not.EqualTo(Guid.Empty));
                Assert.That(loaded.Parameters, Contains.Key("name"));
                Assert.That(loaded.Parameters, Contains.Key("version"));
                Assert.That(loaded.Parameters, Contains.Value("myplugin"));
                Assert.That(loaded.Parameters, Contains.Value("1.0"));
            });
        }

        [Test]
        public void LoadFromEntryPoint_WithAttribute_ValidDynamicScheme_Loads()
        {
            Type entryPointNoParamsType = typeof(Plugins.EntryPoints_WithBadMetadata);
            MethodInfo entryPoint = entryPointNoParamsType.GetMethod("MainWithGoodMetadata")!;
            PluginResolver resolver = new PluginResolver();
            resolver.MetadataSchema = new PluginMetadataSchema(new PluginValidCallback(d => d.ContainsKey("name")));

            var loaded = resolver.LoadFromEntryPoint(entryPoint);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(loaded);
                Assert.That(entryPoint, Is.EqualTo(loaded.EntryPoint));
                Assert.That(loaded.Uid, Is.Not.EqualTo(Guid.Empty));
                Assert.That(loaded.Parameters, Contains.Key("name"));
                Assert.That(loaded.Parameters, Contains.Key("version"));
                Assert.That(loaded.Parameters, Contains.Value("myplugin"));
                Assert.That(loaded.Parameters, Contains.Value("1.0"));
            });
        }

    }
}
