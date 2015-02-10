using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class Template : PageObjectModelBase
    {
        private IWebDriver Driver { get; set; }

        private string _url = "http://the-internet.herokuapp.com/";
        private string _title = "The Internet";

        public Template(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            GoTo(_url, _title);
        }
    }
}
