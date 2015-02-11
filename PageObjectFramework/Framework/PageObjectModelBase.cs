using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PageObjectFramework.Models;
using System;
using System.Configuration;
using System.Threading;

namespace PageObjectFramework.Framework
{
    public class PageObjectModelBase
    {
        // In this class, we will define commonly used methods so they can be 
        // called from any of our Page Objects.

        private IWebDriver Driver { get; set; }
        private bool logActions = 
            ConfigurationManager.AppSettings["logAllActions"] == "true" ? 
            true : 
            false;
        private SeleniumLogger Logger;

        /**
         *  Generic constructor
         */
        public PageObjectModelBase(IWebDriver driver)
        {
            Driver = driver;
            if (logActions)
            {
                Logger = SeleniumLogger.GetLogger("Actions");
            }
        }  
        
        // Shared XPaths
        protected static readonly By Body = By.XPath("//body");
        protected static readonly By Title = By.XPath("//title");
    
        // Shared Methods

        /// <summary>Clears an input box
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected void Clear(By by)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("Clear: {0}", by));
            }
            Find(by).Clear();
        }

        /// <summary>Clears then inputs data into an input box
        /// <para> @param by - the by selector for the given element</para>
        /// <para> @param inputText - the text to input into the input box.</para>
        /// </summary>
        protected void ClearAndSendKeys(By by, string inputText)
        {
            Clear(by);
            if (logActions)
            {
                Logger.LogMessage(string.Format("SndKy: {0}", inputText));
                Logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(inputText);
        }

        /// <summary>Click the element at the given selector.
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected void Click(By by)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("Click: {0}", by));
            }
            var element = Find(by);
            var actions = new Actions(Driver);
            actions.MoveToElement(element).Click().Perform();
        }

        /// <summary>Finds the element by the given selector
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected IWebElement Find(By by)
        {
            return Driver.FindElement(by);
        }

        /// <summary>Gets everything inside the html tags for the given selector
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected string GetInnerHtml(By by)
        {
            return Find(by).GetAttribute("innerHTML");
        }

        /// <summary>Gets the text from the given locator
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected string GetText(By by)
        {
            return Find(by).Text;
        }

        /// <summary>
        /// Gets the title of the current page
        /// </summary>
        protected string GetTitle()
        {
            return GetInnerHtml(Title);
        }

        /// <summary>
        /// Gets the URL of the current page
        /// </summary>
        protected string GetUrl()
        {
            return Driver.Url;
        }

        /// <summary>Go to the given URL.
        /// <para> @param url - the full url of the page you want to visit</para>
        /// <para> @param expectedTitle - </para>
        /// <para> An optional title to add. If you supply a title, the test will </para>
        /// <para> fail if the page you end up on doesn't match the given title. </para>
        /// </summary>
        protected void GoTo(string url, string expectedTitle = "optionalTitle")
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("GoUrl: {0}", url));
            }
            var rootUrl = new Uri(url);
            Driver.Navigate().GoToUrl(rootUrl);

            if ( "optionalTitle" != expectedTitle &&
                !Driver.Title.Contains(expectedTitle))
            {
                var errMsg = String.Format(
                    "PageObjectBase: We're not on the expected page! " + 
                    "Expected: {0}; Actual: {1}", 
                    expectedTitle, Driver.Title);
                throw new NoSuchWindowException(errMsg);
            }
        }

        /// <summary>Inputs data into an input box
        /// <para> @param by - the by selector for the given element</para>
        /// <para> @param inputText - the text to input into the input box.</para>
        /// </summary>
        protected void SendKeys(By by, string inputText)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("SndKy: {0}", inputText));
                Logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(inputText);
        }

        /// <summary>Selects an option from a select box based on text
        /// <para> @param by - the by selector for the given element </para>
        /// <para> @param optionText - the text to select by </para>
        /// </summary>
        protected void SelectByText(By by, string optionText)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("Selct: {0}", optionText));
                Logger.LogMessage(string.Format("   at: {0}", by));
            }
            var select = new SelectElement(Find(by));
            if (null != select)
            {
                select.SelectByText(optionText);
            }
            else
            {
                var errMsg = String.Format(
                    "PageObjectBase: There is no option '{0}' in {1}.",
                    optionText, by);
                throw new OpenQA.Selenium.ElementNotVisibleException(errMsg);
            }
        }

        /// <summary> Submits form for the given locator.
        /// <para> @param by - the by selector for the given element </para>
        /// </summary>
        protected void Submit(By by)
        {
            Find(by).Submit();
        }
    }
}
