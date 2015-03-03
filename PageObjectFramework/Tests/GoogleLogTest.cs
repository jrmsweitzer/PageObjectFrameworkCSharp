using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using PageObjectFramework.Models;
using PageObjectFramework.Framework;
using OpenQA.Selenium;

namespace PageObjectFramework.Tests
{
    [TestFixture(typeof(ChromeDriver))]
    public class GoogleLogTest<TWebDriver> : PageObjectTest<TWebDriver>
        where TWebDriver : IWebDriver, new()
    {
        [Test]
        public void GoogleShouldMakeLogForEveryMethod()
        {
            Google google = new Google(Driver);
            google.EnterSearchText("Example Search Text.");
            google.AppendSearchText(" Adding new text.");
            google.EnterSearchText("Clearing field for this text.");
            google.Search();
        }
    }
}
