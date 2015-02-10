using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PageObjectFramework.Models;
using System;
using System.Threading;

namespace PageObjectFramework.Framework
{
    public class PageObjectModelBase
    {
        // In this class, we will define commonly used methods so they can be 
        // called from any of our Page Objects.

        private IWebDriver Driver { get; set; }
        private bool logActions = true;
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
        public static readonly By Body = By.XPath("//body");
        public static readonly By Title = By.XPath("//title");
    
        // Shared Methods


        /** Clear() - Clears an input box
         * 
         * @param by - the by selector for the given element
         * 
         * @return void
         */
        public void Clear(By by)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("Clear: {0}", by));
            }
            Find(by).Clear();
        }

        /** ClearAndSendKeys() - Inputs data into an input box
         * 
         * @param by - the by selector for the given element
         * @param inputText - the text to input into the input box.
         * 
         * @return void
         */
        public void ClearAndSendKeys(By by, string inputText)
        {
            Clear(by);
            if (logActions)
            {
                Logger.LogMessage(string.Format("SndKy: {0}", inputText));
                Logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(inputText);
        }

        /** Click() - Click the element at the given selector.
         * 
         * @param by - the by selector for the given element
         * 
         * @return void
         */
        public void Click(By by)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("Click: {0}", by));
            }
            var element = Find(by);
            var actions = new Actions(Driver);
            actions.MoveToElement(element).Click().Perform();
        }

        /** Find() - Finds the element by the given selector
         * 
         * @param by - the by selector for the given element
         * 
         * @return IWebElement - The element that the given selector points to.
         */
        public IWebElement Find(By by)
        {
            return Driver.FindElement(by);
        }

        /** GetInnerHtml() - Gets everything inside the html tags for the 
         *                  element at the given by selector.
         *                  
         * @param by - the by selector for the given element
         * 
         * @return string - the innerHTML of the webElement
         */
        public string GetInnerHtml(By by)
        {
            return Find(by).GetAttribute("innerHTML");
        }

        public string GetText(By by)
        {
            return Find(by).Text;
        }

        /** GetTitle() - Gets the title of the current page
         * 
         * @return string - The current page's title.
         */
        public string GetTitle()
        {
            return GetInnerHtml(Title);
        }

        /** GetUrl() - Gets the URL of the current page
         * 
         * @return string - The current page's URL.
         */
        public string GetUrl()
        {
            return Driver.Url;
        }

        /** Goto() - Go to the given URL.
         * 
         * @param url - the full url of the page you want to visit
         * @param expectedTitle - 
         *      An optional title to add. If you supply a title, the test will
         *      fail if the page you end up on doesn't match the given title.
         * 
         * @return void
         */
        public void GoTo(string url, string expectedTitle = "optionalTitle")
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

        /** SendKeys() - Inputs data into an input box
         * 
         * @param by - the by selector for the given element
         * @param inputText - the text to input into the input box.
         * 
         * @return void
         */
        public void SendKeys(By by, string inputText)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("SndKy: {0}", inputText));
                Logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(inputText);
        }

        /** SelectByText() - Selects an option from a select box based on text
         * 
         * @param by - the by selector for the given element
         * @param optionText - the text to select by
         */
        public void SelectByText(By by, string optionText)
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
    }
}
