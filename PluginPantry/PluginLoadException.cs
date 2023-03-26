using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{

	[Serializable]
	public class PluginLoadException : PluginException
	{
		public PluginLoadException() { }
		public PluginLoadException(string message) : base(message) { }
		public PluginLoadException(string message, Exception inner) : base(message, inner) { }
		protected PluginLoadException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
