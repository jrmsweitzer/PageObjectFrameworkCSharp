using NUnit.Framework;
using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class Yammer : PageObject
    {
        public Yammer(IWebDriver driver)
            : base(driver)
        {
            _url = "http://www.yammer.com";
            GoTo(_url, "Yammer");
        }

        private static readonly By _linkLogIn = By.LinkText("Log In");
        private static readonly By _inputEmail = By.Id("login");
        private static readonly By _inputPassword = By.Id("password");
        private static readonly By _btnLogInButton = By.XPath("//span[.='Log In']/..");
        private static readonly By _linkWhatAreYouWorkingOn = By.LinkText("What are you working on?");
        private static readonly By _linkShareSomethingWithThisGroup = By.LinkText("Share something with this group...");
        private static readonly By _inputStatus = By.XPath("(//textarea[contains(@id,'yamjs')])[1]");
        private static readonly By _btnPost = By.XPath("(//button[@data-qaid='post_button'])[1]");
        private static readonly ByFormatter _linkGroupByName = ByFormatter.XPath(
            "//ul[contains(@class,'group')]//span[contains(.,'{0}')]/..");

        /// <summary>
        /// Clicks group by its name.
        /// <para> @param groupName - the name of the group to click.</para>
        /// </summary>
        public Yammer ClickGroup(string groupName)
        {
            if (Find(_linkGroupByName.Format(groupName)) == null)
            {
                Assert.Fail(string.Format("Cannot find group by name {0}."),
                    groupName);
            }
            Click(_linkGroupByName.Format(groupName));
            return this;
        }

        /// <summary>
        /// Signs in to the Yammer account with the given credentials
        /// <para> @param email - the email to sign in with</para>
        /// <para> @param password - the password to sign in with</para>
        /// </summary>
        public Yammer LogInWithCredentials(string email, string password)
        {
            Click(_linkLogIn);
            SendKeys(_inputEmail, email);
            SendKeys(_inputPassword, password);
            Click(_btnLogInButton);
            Sleep(5000);
            return this;
        }
        
        /// <summary>
        /// Posts the given message to Yammer.
        /// <para> @param message - the message to post to Yammer.</para>
        /// </summary>
        public Yammer PostNewMessage(string message)
        {
            Click(_linkWhatAreYouWorkingOn);
            ClearAndSendKeys(_inputStatus, message);
            Click(_btnPost);
            return this;
        }
    }
}
