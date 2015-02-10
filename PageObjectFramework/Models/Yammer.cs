using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class Yammer : PageObjectModelBase
    {
        private IWebDriver Driver { get; set; }
        private static readonly string _url = "http://www.yammer.com";

        public Yammer(IWebDriver driver)
            : base(driver)
        {
            Driver = driver;
            GoTo(_url, "Yammer");
        }

        private static readonly By LogInLink = By.LinkText("Log In");
        private static readonly By Email = By.Id("login");
        private static readonly By Password = By.Id("password");
        private static readonly By LogInButton = By.XPath("//span[.='Log In']/..");
        private static readonly By WhatAreYouWorkingOn = By.LinkText("What are you working on?");
        private static readonly By Status = By.XPath("(//textarea[contains(@id,'yamjs')])[1]");
        private static readonly By PostButton = By.XPath("(//button[@data-qaid='post_button'])[1]");

        public Yammer LogInWithCredentials(string email, string password)
        {
            Click(LogInLink);
            SendKeys(Email, email);
            SendKeys(Password, password);
            Click(LogInButton);
            Thread.Sleep(5000);
            return this;
        }
        
        public Yammer PostNewMessage(string message)
        {
            Click(WhatAreYouWorkingOn);
            ClearAndSendKeys(Status, message);
            //Click(PostButton);
            return this;
        }

        public Yammer FillInMessageBoxWith(string message)
        {
            Click(WhatAreYouWorkingOn);
            ClearAndSendKeys(Status, message);
            return this;
        }
    }
}
