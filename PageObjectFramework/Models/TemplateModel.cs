using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class TemplateModel : PageObjectModelBase
    {
        private IWebDriver Driver { get; set; }

        private string _url = "http://the-internet.herokuapp.com/";
        private string _title = "The Internet";

        /// <summary>
        /// The Template PageObject Model.
        /// Edit this to jumpstart your template!
        /// </summary>
        public TemplateModel(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            GoTo(_url, _title);
        }

        // Example Locator: 
        // private static readonly By Title = By.XPath("//title");
    }
}
