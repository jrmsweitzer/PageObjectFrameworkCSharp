using OpenQA.Selenium;
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

        private static readonly By Username = By.Id("username");
        private static readonly By Password = By.Id("password");
        private static readonly By LoginButton = By.XPath("//button");

        /// <summary>
        /// Appends text to the text already in the username textbox
        /// <para> @param text - the text to append.</para>
        /// </summary>
        public HerokuLogin AppendToUsernameTextBox(string text)
        {
            SendKeys(Username, text);
            return this;
        }

        /// <summary>
        /// Sends a backspace key to the username box.
        /// </summary>
        public HerokuLogin Backspace()
        {
            Find(Username).SendKeys(Keys.Backspace);
            return this;
        }

        /// <summary>
        /// Clears the username textbox
        /// </summary>
        public HerokuLogin ClearUsernameTextBox()
        {
            Clear(Username);
            return this;
        }

        /// <summary>
        /// Logs in using Heroku's Login credentials
        /// </summary>
        public HerokuLogin Login()
        {
            WriteInUsernameTextBox("tomsmith");
            SendKeys(Password, "SuperSecretPassword!");
            Click(LoginButton);
            return this;
        }

        /// <summary>
        /// Takes URL to the Heroku Login Page and brings control of the driver with it.
        /// </summary>
        /// <returns></returns>
        public HerokuLogin ReturnToLogin()
        {
            GoTo(_url);
            return new HerokuLogin(Driver);
        }

        /// <summary>
        /// Clears the username textbox and writes in the given text
        /// <para> @param text - the text to write in the username textbox. </para>
        /// </summary>
        public HerokuLogin WriteInUsernameTextBox(string text)
        {
            Clear(Username);
            SendKeys(Username, text);
            return this;
        }
    }
}
