using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using FubuCore;

namespace AssemblyTypeListTest
{
	class MainClass
	{
		public static Dictionary<string, Assembly> cache = new Dictionary<string, Assembly>();

		public static void Main (string[] args)
		{
			if (!Directory.Exists ("fubu"))
				Directory.CreateDirectory ("fubu");

			File.Copy ("AssemblyTypeListTest.exe", Path.Combine ("fubu", "AssemblyTypeListTest.exe"), true);
			File.Copy ("AssemblyTypeListTest.exe.mdb", Path.Combine ("fubu", "AssemblyTypeListTest.exe.mdb"), true);
		    File.Copy(Path.Combine("..", "..", "..", "testlibrary", "bin", "Debug", "testlibrary.dll"),
		              Path.Combine("fubu", "testlibrary.dll"), true);
			File.Copy(Path.Combine("..", "..", "..", "SharedLibrary", "bin", "Debug", "SharedLibrary.dll"),
			          Path.Combine("fubu", "SharedLibrary.dll"), true);
			File.Copy(Path.Combine("FubuCore.dll"),
			          Path.Combine("fubu", "FubuCore.dll"), true);


			var setup = new AppDomainSetup {
				ApplicationName = "Bottle-Services-AppDomain",
				ShadowCopyFiles = "true",
				ConfigurationFile = "BottleServiceRunner.exe.config",
				ApplicationBase = Path.Combine(".".ToFullPath(), "fubu")
			};
			var domain = AppDomain.CreateDomain(setup.ApplicationName, null, setup);

			Type proxyType = typeof (Proxy);
			Proxy proxy = 
				(Proxy) domain.CreateInstanceAndUnwrap(
					proxyType.Assembly.FullName, 
					proxyType.FullName
					);

			var result = proxy.Go ();
			Console.WriteLine (result);

		    Console.WriteLine("Press enter to close");
		    Console.ReadLine();
		}
	}
}
