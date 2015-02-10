using NUnit.Framework;
using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System;

namespace PageObjectFramework.Models
{
    public class Email : PageObjectModelBase
    {
        private IWebDriver Driver { get; set; }
        private string _url = "https://mail.catalystitservices.com/";
        private WindowHandler Handler { get; set; }

        public Email(IWebDriver driver) : base(driver)
        {
            Driver = driver;
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

        public Email SignInWithCredentials(string username, string password)
        {
            SendKeys(UsernameInput, username);
            SendKeys(PasswordInput, password);
            Click(SignInButton);

            return this;
        }

        public Email ComposeNewEmail(string toEmail, string subject, string message)
        {
            Click(NewEmailButton);

            Handler.AddNewWindowHandle("Compose Email");
            Handler.SwitchToHandle("Compose Email");

            SendKeys(ToInput, toEmail);
            SendKeys(SubjectInput, subject);

            Driver.SwitchTo().Frame(Find(MessageFrame));
            SendKeys(MessageBody, message);

            return this;
        }
    }
}
