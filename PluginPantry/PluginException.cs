﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginPantry
{

	[Serializable]
	public class PluginException : Exception
	{
		public PluginException() { }
		public PluginException(string message) : base(message) { }
		public PluginException(string message, Exception inner) : base(message, inner) { }
		protected PluginException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
