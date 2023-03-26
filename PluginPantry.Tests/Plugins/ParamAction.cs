using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantryTests.Plugins
{
    internal class ParamAction
    {
        public string Text { get; init; }


        public ParamAction()
        : this("Test text")
        {

        }

        public ParamAction(string text)
        {
            Text = text;
        }
    }
}
