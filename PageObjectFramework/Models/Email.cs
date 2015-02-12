using NUnit.Framework;
using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class Email : PageObjectModelBase
    {
        private string _url = "https://mail.catalystitservices.com/";
        private WindowHandler Handler { get; set; }

        /// <summary>
        /// The Class for the Outlook WebApp
        /// </summary>
        public Email(IWebDriver driver) : base(driver)
        {
            GoTo(_url);
            Handler = new WindowHandler(driver);
        }

        private static readonly By UsernameInput = By.Id("username");
        private static readonly By PasswordInput = By.Id("password");
        private static readonly By SignInButton = By.XPath("//input[@type='submit']");

        private static readonly By NewEmailButton = By.XPath("//a[.='New']");
        private static readonly By ToInput = By.Id("divTo");
        private static readonly By SubjectInput = By.Id("txtSubj");
        private static readonly By MessageInput = By.Id("txtBdy");
        private static readonly By MessageFrame = By.Id("ifBdy");
        private static readonly By MessageBody = By.XPath("//style[@id='owaTempEditStyle']/../../body");
        private static readonly By SendButton = By.Id("send");

        /// <summary>
        /// Signs in to the email account with the given credentials
        /// <para> @param username - the username to sign in with</para>
        /// <para> @param password - the password to sign in with</para>
        /// </summary>
        public Email LogInWithCredentials(string username, string password)
        {
            SendKeys(UsernameInput, username);
            SendKeys(PasswordInput, password);
            Click(SignInButton);

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
            Click(NewEmailButton);

            Handler.AddNewWindowHandle("Compose Email");
            Handler.SwitchToHandle("Compose Email");

            SendKeys(ToInput, to);
            Thread.Sleep(1000);
            SendKeys(SubjectInput, subject);
            Thread.Sleep(1000);

            Driver.SwitchTo().Frame(Find(MessageFrame));
            SendKeys(MessageBody, message);

            Handler.SwitchToHandle("Compose Email");
            Click(SendButton);

            Handler.SwitchToHandle(WindowHandler.MainWindowHandle);

            return this;
        }
    }
}
