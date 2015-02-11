using OpenQA.Selenium;
using PageObjectFramework.Framework;
using PageObjectFramework.Models.Heroku;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class HerokuMain : PageObjectModelBase, IHerokuApp
    {
        private IWebDriver Driver { get; set; }
        private static readonly string _url = "http://the-internet.herokuapp.com/";

        public HerokuMain(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            GoTo(_url, "The Internet");
        }

        public const string Checkboxes = "Checkboxes";
        public const string Dropdown = "Dropdown";
        public const string FormAuthentication = "Form Authentication";

        /// <summary>
        /// Clicks link defined by the linkText
        /// <para> @param linkText - The text of the link to click.</para>
        /// <para> @returns a heroku page object, depending on the link clicked.</para>
        /// </summary>
        public IHerokuApp ClickLink(string linkText)
        {
            Click(By.LinkText(linkText));
            Thread.Sleep(1000);
            switch (linkText)
            {
                case Checkboxes:
                    return new HerokuCheckboxes(Driver);
                case Dropdown:
                    return new HerokuDropdown(Driver);
                case FormAuthentication:
                    return new HerokuLogin(Driver);
                default:
                    return this;
            }
        }

        /// <summary>
        /// Takes URL to the Heroku Main Page and brings control of the driver with it.
        /// </summary>
        /// <returns></returns>
        public HerokuMain ReturnToHeroku()
        {
            GoTo(_url, "The Internet");
            return new HerokuMain(Driver);
        }
    }
}
