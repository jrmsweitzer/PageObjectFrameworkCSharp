using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PageObjectFramework.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace PageObjectFramework.Framework
{
    public class PageObjectModelBase
    {
        // In this class, we will define commonly used methods so they can be 
        // called from any of our Page Objects.

        protected IWebDriver Driver { get; set; }
        private bool logActions = 
            ConfigurationManager.AppSettings["logAllActions"] == "true" ? 
            true : 
            false;
        private SeleniumLogger Logger;
        private int defaultTimeout = Int32.Parse(ConfigurationManager.AppSettings["defaultTimeout"]) * 1000;
        private Stopwatch _stopwatch;

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
        /// <para> @param value - the text to input into the input box.</para>
        /// </summary>
        protected void ClearAndSendKeys(By by, string value)
        {
            Clear(by);
            if (logActions)
            {
                Logger.LogMessage(string.Format("SndKy: {0}", value));
                Logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(value);
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

        /// <summary> Finds all elements by the given selector
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected ICollection<IWebElement> FindAll(By by)
        {
            return Driver.FindElements(by);
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
        public string GetUrl()
        {
            return Driver.Url;
        }

        /// <summary>Go to the given URL.
        /// <para> @param url - the full url of the page you want to visit</para>
        /// <para> @param expectedTitle - </para>
        /// <para> An optional title to add. If you supply a title, the test will </para>
        /// <para> fail if the page you end up on doesn't match the given title. </para>
        /// </summary>
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

        /// <summary>Inputs data into an input box
        /// <para> @param by - the by selector for the given element</para>
        /// <para> @param value - the text to input into the input box.</para>
        /// </summary>
        protected void SendKeys(By by, string value)
        {
            if (logActions)
            {
                Logger.LogMessage(string.Format("SndKy: {0}", value));
                Logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(value);
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

        /// <summary>
        /// Pauses play until a given element is no longer on the DOM.
        /// <para>@param by - the by selector for the given element</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the element to be deleted.</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForElementToBeDeleted(By by, int timeout)
        {
            _stopwatch.Start();
            while (FindAll(by).Count > 0)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    Assert.Fail(string.Format("Element '{0}' was still visible after {1} seconds!",
                        by.ToString(), timeout / 1000));
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }

        /// <summary>
        /// Pauses play until a given element is no longer on the DOM.
        /// <para>@param by - the by selector for the given element</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the element to be deleted.</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForElementToBeDeleted(By by)
        {
            WaitForElementToBeDeleted(by, defaultTimeout);
        }

        /// <summary>
        /// Pauses play until a given element becomes visible.
        /// <para>@param by - the by selector for the given element</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the element to exist</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForElementToExist(By by, int timeout)
        {
            _stopwatch.Start();
            while (FindAll(by).Count == 0)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    Assert.Fail(string.Format("Could not find element '{0}' after {1} seconds!",
                        by.ToString(), timeout / 1000));
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }

        /// <summary>
        /// Pauses play until a given element becomes visible.
        /// <para>@param by - the by selector for the given element</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the element to exist</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForElementToExist(By by)
        {
            // Overloaded
            WaitForElementToExist(by, defaultTimeout);
        }

        /// <summary>
        /// Pauses play until the page is on the given URL.
        /// <para>@param url - the url of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the url</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForUrl(string url, int timeout)
        {
            _stopwatch.Start();
            while (GetUrl() != url)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    Assert.Fail(string.Format("Was not on url '{0}' after {1} seconds!",
                        url, timeout / 1000));
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }

        /// <summary>
        /// Pauses play until the page is on the given URL.
        /// <para>@param url - the url of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the url</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForUrl(string url)
        {
            // Overloaded
            WaitForUrl(url, defaultTimeout);
        }
    }
}
