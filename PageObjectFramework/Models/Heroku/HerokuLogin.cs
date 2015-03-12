using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models.Heroku
{
    public class HerokuLogin : PageObject, IHerokuApp
    {
        public HerokuLogin(IWebDriver driver) : base(driver)
        {
            _url = "http://the-internet.herokuapp.com/login";
            GoTo(_url);
        }

        public HerokuLogin() : base(new ChromeDriver(SeleniumSettings.DriverDirectory))
        {
            _url = "http://the-internet.herokuapp.com/login";
            GoTo(_url);
        }

        private static readonly By _inputUsername = By.Id("username");
        private static readonly By _inputPassword = By.Id("password");
        private static readonly By _btnLogin = By.XPath("//button");

        /// <summary>
        /// Appends text to the text already in the username textbox
        /// <para> @param text - the text to append.</para>
        /// </summary>
        public HerokuLogin AppendToUsernameTextBox(string text)
        {
            SendKeys(_inputUsername, text);
            return this;
        }

        /// <summary>
        /// Sends a backspace key to the username box.
        /// </summary>
        public HerokuLogin Backspace()
        {
            Find(_inputUsername).SendKeys(Keys.Backspace);
            return this;
        }

        /// <summary>
        /// Clears the username textbox
        /// </summary>
        public HerokuLogin ClearUsernameTextBox()
        {
            Clear(_inputUsername);
            return this;
        }

        /// <summary>
        /// Logs in using Heroku's Login credentials
        /// </summary>
        public HerokuLogin Login()
        {
            Click(_btnLogin);
            return this;
        }

        /// <summary>
        /// Takes URL to the Heroku Login Page and brings control of the driver with it.
        /// </summary>
        /// <returns></returns>
        public HerokuLogin ReturnToLogin()
        {
            GoTo(_url);
            return new HerokuLogin(_driver);
        }

        /// <summary>
        /// Clears the username textbox and writes in the given text
        /// <para> @param text - the text to write in the username textbox. </para>
        /// </summary>
        public HerokuLogin WriteInUsernameTextBox(string text)
        {
            Clear(_inputUsername);
            SendKeys(_inputUsername, text);
            return this;
        }

        public HerokuLogin WriteInPasswordTextBox(string text)
        {
            Clear(_inputPassword);
            SendKeys(_inputPassword, text);
            return this;
        }
    }
}
