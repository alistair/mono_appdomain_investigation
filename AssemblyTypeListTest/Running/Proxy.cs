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
		    string result = string.Empty;
		    try
		    {
				var files = Directory.GetFiles(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

		        result = string.Join(Environment.NewLine, files.ToArray());
		        result += Environment.NewLine + Environment.NewLine;

		        foreach (var file in files)
		        {
					if(file.EndsWith("AssemblyTypeListTest.exe.mdb")) continue;

		            var assembly = Assembly.LoadFrom(file);
		            cache.Add(assembly.GetName().Name, assembly);
		        }

		        var types = cache.Keys.SelectMany(x => cache[x].GetExportedTypes());

		        result += string.Format("Types Loaded: {0}\n", types.Count());

		        foreach (var type in types)
				{

					var isConcrete = type.IsConcrete();
					var isAssignableFrom = typeof (IApplicationSource).IsAssignableFrom(type);
					var emptyContructor = type.GetConstructor(new Type[0]) != null;

					if ( isConcrete && isAssignableFrom && emptyContructor)
		            	result += string.Format("Found: {0}\n", type.FullName);
		        }
		    }
		    catch (Exception e)
		    {
		        result += string.Format("\n\n\n{0}\n\n", e.ToString());
		    }
		    return result;
		}
	}
}

