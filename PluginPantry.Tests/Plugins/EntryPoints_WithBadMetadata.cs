using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry.Tests.Plugins
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


        [EntryPoint]
        public static void MainWithEmptyAttribute()
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
    }
}
