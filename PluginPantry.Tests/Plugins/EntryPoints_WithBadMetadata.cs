using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginPantry;


namespace PluginPantryTests.Plugins
{
    public class EntryPoints_WithBadMetadata
    {
        public static bool Called = false;



        public void NonStatic()
        {
            Called = true;
        }


        [EntryPoint]
        public void NonStaticWithEmptyAttrib()
        {

        }




        public static void MainWithNoAttribute()
        {
            Called = true;
        }

        public static void MainWithNoAttributeAndParams(PluginContext context, Guid guid)
        {
            Called = true;
        }

        [EntryPoint]
        public static void MainWithEmptyAttribute()
        {
            Called = true;
        }

        [EntryPoint]
        public static void MainWithEmptyAttributeAndParams(Guid id, PluginContext context)
        {
            Called = true;
        }


        [EntryPoint("name", "myplugin", "version")]
        public static void MainWithBadMetada()
        {
            Called = true;
        }


        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainWithGoodMetadata()
        {
            Called = true;
        }

        

        [EntryPoint("name", "myplugin", "version", "1.0")]
        public static void MainWithGoodMetadataAndParams(Guid id, PluginContext context)
        {
            Called = true;
        }
    }
}
