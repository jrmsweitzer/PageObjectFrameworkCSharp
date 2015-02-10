using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models.Heroku
{
    public class HerokuLogin : PageObjectModelBase, IHerokuApp
    {
        private IWebDriver Driver { get; set; }

        private string _url = "http://the-internet.herokuapp.com/login";

        public HerokuLogin(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            GoTo(_url);
        }

        private static readonly By Username = By.Id("username");
        private static readonly By Password = By.Id("password");
        private static readonly By LoginButton = By.XPath("//button");

        public HerokuLogin AppendToUsernameTextBox(string text)
        {
            SendKeys(Username, text);
            return this;
        }

        public HerokuLogin ClearUsernameTextBox()
        {
            Clear(Username);
            return this;
        }

        public HerokuLogin ReturnToLogin()
        {
            GoTo(_url);
            return new HerokuLogin(Driver);
        }

        public HerokuLogin WriteInUsernameTextBox(string text)
        {
            Clear(Username);
            SendKeys(Username, text);
            return this;
        }
    }
}
