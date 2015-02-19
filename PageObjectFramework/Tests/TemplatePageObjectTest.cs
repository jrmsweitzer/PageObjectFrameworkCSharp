using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;
using PageObjectFramework.Framework;
using PageObjectFramework.Models;

namespace PageObjectFramework.Tests
{
    //Place all of the browsers you want to test here:
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    //[TestFixture(typeof(InternetExplorerDriver))]
    //[TestFixture(typeof(SafariDriver))]
    //[TestFixture(typeof(PhantomJSDriver))]
    // Just replace "TemplatePageObjectTest" with the name of your test. 
    // Everything else here stays the same.
    class TemplatePageObjectTest<TWebDriver> : PageObjectTest<TWebDriver> 
        where TWebDriver : IWebDriver, new()
    {
        // A test using the Page Objects.
        [Test]
        public void TestA()
        {
            Google google = new Google(Driver);
            google.EnterSearchText("Test Search")
                .Search();
        }

        // A test using the Page Objects.
        [Test]
        public void TestB()
        {
            Google google = new Google(Driver);
            google.EnterSearchText("Test Search")
                .Search();
        }

        // A test using the Page Objects.
        [Test]
        public void TestC()
        {
            Assert.IsTrue(false);
        }

        // A test using the Page Objects.
        [Test]
        public void TestD()
        {
            Assert.Fail("This is a custom Failure message!");
        }
    }
}
