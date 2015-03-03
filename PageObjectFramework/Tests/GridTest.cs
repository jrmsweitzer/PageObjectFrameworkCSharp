using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Threading;

namespace PageObjectFramework.Tests
{
    // https://seleniumautomation84.wordpress.com/2014/08/06/selenium-grid-fundamentals-implemented-in-c/
    [TestFixture]
    public class GridTest
    {
        IWebDriver driver;
        IWebDriver driver2;

        //[SetUp]
        public void SetUp()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities = DesiredCapabilities.Firefox();

            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));

            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);

            driver2 = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Firefox());

        }

        //[TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver2.Quit();
        }

        //[Test]
        public void Grid()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            driver2.Navigate().GoToUrl("http://www.yahoo.com");
            Assert.AreEqual("Google", driver.Title);
            Assert.AreEqual("Yahoo", driver2.Title);
            Thread.Sleep(3000);
        }

        [Test]
        public void RunGridHubBatchFile()
        {
            var currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            Console.WriteLine(currentPath);

            var filepath = "C:/Selenium/Grid/test.bat"; 
            //var filepath = "~..\\..\\Resources\\test.bat";

            Console.WriteLine(filepath);

            Process process = new Process();
            process.StartInfo.FileName = filepath;
            process.Start();
        }
    }
}
