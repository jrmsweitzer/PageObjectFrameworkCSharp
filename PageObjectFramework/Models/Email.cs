using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class Email : PageObject
    {
        /// <summary>
        /// The Class for the Outlook WebApp
        /// </summary>
        public Email(IWebDriver driver) : base(driver)
        {
            _url = "https://mail.catalystitservices.com/";
            GoTo(_url);
        }

        private static readonly By _inputUsername = By.Id("username");
        private static readonly By _inputPassword = By.Id("password");
        private static readonly By _btnSignIn = By.XPath("//input[@type='submit']");

        private static readonly By _btnNewEmail = By.XPath("//a[.='New']");
        private static readonly By _inputTo = By.Id("divTo");
        private static readonly By _inputSubject = By.Id("txtSubj");
        private static readonly By _inputMessage = By.Id("txtBdy");
        private static readonly By _ifrMessage = By.Id("ifBdy");
        private static readonly By _bodyMessage = By.XPath("//style[@id='owaTempEditStyle']/../../body");
        private static readonly By _btnSend = By.Id("send");

        /// <summary>
        /// Signs in to the email account with the given credentials
        /// <para> @param username - the username to sign in with</para>
        /// <para> @param password - the password to sign in with</para>
        /// </summary>
        public Email LogInWithCredentials(string username, string password)
        {
            SendKeys(_inputUsername, username);
            SendKeys(_inputPassword, password);
            Click(_btnSignIn);

            return this;
        }

        /// <summary>
        /// Send a new email
        /// <para> @param to - the email address to send it to</para>
        /// <para> @param subject - the subject of the email</para>
        /// <para> @param message - the body of the email</para>
        /// </summary>
        public Email ComposeNewEmail(string to, string subject, string message)
        {
            Click(_btnNewEmail);

            _windowHandler.AddNewWindowHandle("Compose Email");
            _windowHandler.SwitchToHandle("Compose Email");

            SendKeys(_inputTo, to);
            Thread.Sleep(1000);
            SendKeys(_inputSubject, subject);
            Thread.Sleep(1000);

            _driver.SwitchTo().Frame(Find(_ifrMessage));
            SendKeys(_bodyMessage, message);

            _windowHandler.SwitchToHandle("Compose Email");
            Click(_btnSend);

            _windowHandler.SwitchToHandle(WindowHandler.MainWindowHandle);

            return this;
        }
    }
}
