using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace PageObjectFramework.Tests
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(OpenQA.Selenium.IE.InternetExplorerDriver))]
    [TestFixture(typeof(OpenQA.Selenium.PhantomJS.PhantomJSDriver))]
    [TestFixture(typeof(OpenQA.Selenium.Safari.SafariDriver))]
    public class MultipleBrowserTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver driver;
        private bool runInFireFox = true;
        private bool runInChrome = false;

        [SetUp]
        public void CreateDriver()
        {
            var path = @"../../Drivers";
            if (typeof (TWebDriver).Equals(typeof(FirefoxDriver)))
            {
                if (runInFireFox)
                {
                    driver = new TWebDriver();
                }
            }
            else
            {
                driver = Activator.CreateInstance(typeof(TWebDriver), new object[] { path }) as IWebDriver;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void GoogleTest()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Bread" + Keys.Enter);

            Thread.Sleep(20000);

            Assert.AreEqual("Bread - Google Search", driver.Title);
            driver.Quit();
        }
    }
}
