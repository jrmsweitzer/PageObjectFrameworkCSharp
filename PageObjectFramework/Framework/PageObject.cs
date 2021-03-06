﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PageObjectFramework.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PageObjectFramework.Framework
{
    /// <summary>
    /// The Model Base for all Page Objects.
    /// <para> </para>
    /// <para>Any class implementing PageObjectModelBase should have a constructor like so:</para>
    /// <para>public PageObjectNameGoesHere(IWebDriver driver) : base(driver)</para>
    /// <para>{</para>
    /// <para>    // Everything in here is optional</para>
    /// <para>    _url = "https://www.google.com";</para>
    /// <para>    _title = "Google";</para>
    /// <para>    GoTo(_url, _title);</para>
    /// <para>}</para>
    /// </summary>
    public class PageObject
    {
        // Driver and Page-specific stuff
        protected IWebDriver _driver { get; set; }
        protected string _url { get; set; }
        protected string _title { get; set; }

        // Config stuff
        private string _actionLog = SeleniumSettings.ActionLogName;
        private bool _logActions = SeleniumSettings.LogAllActions;

        private int _defaultTimeout = SeleniumSettings.DefaultTimeout * 1000;

        private SeleniumLogger _logger;
        protected WindowHandler _windowHandler;

        /**
         *  Generic constructor
         */
        public PageObject(IWebDriver driver)
        {
            _driver = driver;
            _windowHandler = new WindowHandler(_driver);

            if (_logActions)
            {
                _logger = SeleniumLogger.GetLogger(_actionLog);
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
            if (_logActions)
            {
                _logger.LogMessage(string.Format("Clear: {0}", by));
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
            SendKeys(by, value);
        }

        /// <summary>Click the element at the given selector.
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        internal void Click(By by)
        {
            if (_logActions)
            {
                _logger.LogMessage(string.Format("Click: {0}", by));
            }
            Find(by).Click();
        }

        protected bool ElementExists(By by)
        {
            return FindAll(by).Count > 0 ? true : false;
        }

        /// <summary>Finds the element by the given selector
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected IWebElement Find(By by)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (_driver.FindElements(by).Count == 0)
            {
                if (_stopwatch.ElapsedMilliseconds > _defaultTimeout)
                {
                    throw new NoSuchElementException("Could not find element " + by.ToString());
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();

            return _driver.FindElement(by);
        }

        /// <summary> Finds all elements by the given selector
        /// <para> @param by - the by selector for the given element</para>
        /// </summary>
        protected ICollection<IWebElement> FindAll(By by)
        {
            return _driver.FindElements(by);
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
        public string GetTitle()
        {
            return GetInnerHtml(Title);
        }

        /// <summary>
        /// Gets the URL of the current page
        /// </summary>
        public string GetUrl()
        {
            return _driver.Url;
        }

        /// <summary>Go to the given URL.
        /// <para> @param url - the full url of the page you want to visit</para>
        /// <para> @param expectedTitle - </para>
        /// <para> An optional title to add. If you supply a title, the test will </para>
        /// <para> fail if the page you end up on doesn't match the given title. </para>
        /// </summary>
        public void GoTo(string url, string expectedTitle = "optionalTitle")
        {
            if (_logActions)
            {
                _logger.LogMessage(string.Format("GoUrl: {0}", url));
            }
            var rootUrl = new Uri(url);
            _driver.Navigate().GoToUrl(rootUrl);

            if ("optionalTitle" != expectedTitle)
            {
                WaitForTitle(expectedTitle);
            }
        }

        /// <summary>Inputs data into an input box
        /// <para> @param by - the by selector for the given element</para>
        /// <para> @param value - the text to input into the input box.</para>
        /// </summary>
        protected void SendKeys(By by, string value)
        {
            if (_logActions)
            {
                _logger.LogMessage(string.Format("SndKy: {0}", value));
                _logger.LogMessage(string.Format("   to: {0}", by));
            }
            Find(by).SendKeys(value);
        }

        /// <summary>Selects an option from a select box based on text
        /// <para> @param by - the by selector for the given element </para>
        /// <para> @param optionText - the text to select by </para>
        /// </summary>
        internal void SelectByText(By by, string optionText)
        {
            if (_logActions)
            {
                _logger.LogMessage(string.Format("Selct: {0}", optionText));
                _logger.LogMessage(string.Format("   at: {0}", by));
            }
            var select = new SelectElement(Find(by));
            if (!select.Equals(null))
            {
                try
                {
                    select.SelectByText(optionText);
                }
                catch
                {
                    var errMsg = String.Format(
                        "PageObjectBase: There is no option '{0}' in {1}.",
                        optionText, by);
                    throw new InvalidSelectOptionException(errMsg);
                }
            }
            else
            {
                string errMsg = "Cannot find element " + by.ToString();
                throw new NoSuchElementException(errMsg);
            }
        }

        /// <summary> Suspends the current thread for a specified time.
        /// <para>@param millisecondsTimeout - the timeout in milliseconds</para>
        /// </summary>
        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
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
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (FindAll(by).Count > 0)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    string errMsg = string.Format(
                        "Element '{0}' was still visible after {1} seconds!",
                        by.ToString(), timeout / 1000);
                    throw new WrongPageException(errMsg);
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
            WaitForElementToBeDeleted(by, _defaultTimeout);
        }

        /// <summary>
        /// Pauses play until a given element becomes visible.
        /// <para>@param by - the by selector for the given element</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the element to exist</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForElementToExist(By by, int timeout)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (FindAll(by).Count == 0)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    var errMsg = string.Format(
                        "Could not find element '{0}' after {1} seconds!",
                        by.ToString(), timeout / 1000);
                    throw new ElementNotVisibleException(errMsg);
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
            WaitForElementToExist(by, _defaultTimeout);
        }

        /// <summary>
        /// Pauses play until the current Url contains partialUrl string.
        /// <para>@param partialUrl - the partial url of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the url</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForPartialUrl(string partialUrl, int timeout)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (!GetUrl().Contains(partialUrl))
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    var errMsg = string.Format(
                        "Url did not contain string '{0}' after {1} seconds!",
                        partialUrl, timeout / 1000);

                    throw new WrongPageException(errMsg);
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }

        /// <summary>
        /// Pauses play until the current Url contains partialUrl string.
        /// <para>@param partialUrl - the partial url of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the url</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForPartialUrl(string partialUrl)
        {
            WaitForPartialUrl(partialUrl, _defaultTimeout);
        }

        /// <summary>
        /// Pauses play until the current Title contains expectedTitle string.
        /// <para>@param expectedTitle - the expected title of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the title</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        private void WaitForTitle(string expectedTitle, int timeout)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (!GetTitle().Contains(expectedTitle))
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    var errMsg = String.Format(
                        "We're not on the expected page! " +
                        "Expected Title: {0}; Actual Title: {1}",
                        expectedTitle, GetTitle());
                    throw new WrongPageException(errMsg);
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();



            if ("optionalTitle" != expectedTitle &&
                !GetTitle().Contains(expectedTitle))
            {
                var errMsg = String.Format(
                    "PageObject: We're not on the expected page! " +
                    "Expected: {0}; Actual: {1}",
                    expectedTitle, GetTitle());
                throw new WrongPageException(errMsg);
            }
        }

        /// <summary>
        /// Pauses play until the current Title contains expectedTitle string.
        /// <para>@param expectedTitle - the expected title of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the title</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        private void WaitForTitle(string expectedTitle)
        {
            WaitForTitle(expectedTitle, _defaultTimeout);
        }

        /// <summary>
        /// Pauses play until the page is on the given URL.
        /// <para>@param url - the url of the page to wait for</para>
        /// <para>@param timeout (optional) - the time, in milliseconds, to wait for the url</para>
        /// <para>If no time is given for the timeout, will use the default timeout.</para>
        /// </summary>
        protected void WaitForUrl(string url, int timeout)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (GetUrl() != url)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    var errMsg = string.Format("Was not on url '{0}' after {1} seconds!\nCurrent url: {2}",
                        url,
                        timeout / 1000,
                        GetUrl());

                    throw new WrongPageException(errMsg);
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
            WaitForUrl(url, _defaultTimeout);
        }

    }
}
