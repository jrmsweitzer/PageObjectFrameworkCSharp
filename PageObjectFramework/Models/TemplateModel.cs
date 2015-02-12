using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class TemplateModel : PageObject
    {
        /// <summary>
        /// The Template PageObject Model.
        /// Edit this to jumpstart your template!
        /// </summary>
        public TemplateModel(IWebDriver driver) : base(driver)
        {
            _url = "http://the-internet.herokuapp.com/";
            _title = "The Internet";

            GoTo(_url, _title);
        }

        // Example Locator: 
        // private static readonly By Title = By.XPath("//title");
    }
}
