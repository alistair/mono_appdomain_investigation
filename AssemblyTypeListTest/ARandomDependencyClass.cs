using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AssemblyTypeListTest
{
	public class ARandomDependencyClass
	{
		public IWebDriver BuildBrowser()
		{
			return new FirefoxDriver();
		}
	}
}

