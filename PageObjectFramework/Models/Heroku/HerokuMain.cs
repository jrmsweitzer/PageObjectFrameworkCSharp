using OpenQA.Selenium;
using PageObjectFramework.Framework;
using PageObjectFramework.Models.Heroku;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class HerokuMain : PageObject, IHerokuApp
    {
        public HerokuMain(IWebDriver driver) : base(driver)
        {
            _url = "http://the-internet.herokuapp.com/";
            GoTo(_url, "The Internet");
        }

        public const string Checkboxes = "Checkboxes";
        public const string Dropdown = "Dropdown";
        public const string FormAuthentication = "Form Authentication";

        private static readonly ByFormatter _linkByText = ByFormatter.LinkText("{0}");

        /// <summary>
        /// Clicks link defined by the linkText
        /// <para> @param linkText - The text of the link to click.</para>
        /// <para> @returns a heroku page object, depending on the link clicked.</para>
        /// </summary>
        public IHerokuApp ClickLink(string linkText)
        {
            Click(_linkByText.Format(linkText));
            Sleep(1000);
            switch (linkText)
            {
                case Checkboxes:
                    return new HerokuCheckboxes(_driver);
                case Dropdown:
                    return new HerokuDropdown(_driver);
                case FormAuthentication:
                    return new HerokuLogin(_driver);
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
            return new HerokuMain(_driver);
        }
    }
}
