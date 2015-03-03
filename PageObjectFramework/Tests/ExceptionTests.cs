using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PageObjectFramework.Framework;
using PageObjectFramework.Framework.Exceptions;
using PageObjectFramework.Models;
using PageObjectFramework.Models.Heroku;

namespace PageObjectFramework.Tests
{
    [TestFixture(typeof(FirefoxDriver))]
    public class ErrorsTest<TWebDriver> : PageObjectTest<TWebDriver>
        where TWebDriver : IWebDriver, new()
    {
        [Test]
        public void ExceptionTestWrongPageTitle()
        {
            Assert.Throws<WrongPageException>(() =>
                new HerokuMain(Driver, "Incorrect Title"));
        }

        [Test]
        public void ExceptionTestInvalidByLocator()
        {
            var heroku = new HerokuMain(Driver);
            var invalidSelector = By.XPath("This is an invalid selector.");

            Assert.Throws<InvalidSelectorException>(() =>
                heroku.Click(invalidSelector));
        }

        [Test]
        public void ExceptionTestNonExistantLocator()
        {
            var heroku = new HerokuMain(Driver);
            var nonexistantLocator = By.XPath("//button[.='This button does not exist.']");

            Assert.Throws<NoSuchElementException>(() =>
                heroku.Click(nonexistantLocator));
        }

        [Test]
        public void ExceptionTestInvalidSelectOption()
        {
            var heroku = new HerokuMain(Driver);

            Assert.Throws<InvalidSelectOptionException>(() =>
            ((HerokuDropdown)heroku
                .ClickLink(HerokuMain.Dropdown))
                .SelectOption("This is not an option."));
        }
    }
}
