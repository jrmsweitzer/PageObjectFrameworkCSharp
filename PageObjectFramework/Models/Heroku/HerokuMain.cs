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
        public const string WysiwygEditor = "WYSIWYG Editor";
        public const string FormAuthentication = "Form Authentication";

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

        public HerokuMain ReturnToHeroku()
        {
            GoTo(_url, "The Internet");
            return new HerokuMain(Driver);
        }
    }
}
