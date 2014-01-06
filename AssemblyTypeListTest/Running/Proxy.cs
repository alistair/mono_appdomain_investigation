using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FubuCore;
using SharedLibrary;
using System.Configuration;

namespace AssemblyTypeListTest
{
	public class Proxy : MarshalByRefObject
	{

		public static Dictionary<string, Assembly> cache = new Dictionary<string, Assembly>();

		public Proxy ()
		{
		}


		public string Go ()
		{
			//But this one fails...
			var @object = ConfigurationManager.GetSection ("system.codedom");
		    var type = Type.GetType("testlibrary2.MyClassDoesNotExist, testlibrary2");

		    if (type == null)
		        return "No Type Found";
			return string.Empty;
		}
	}
}

