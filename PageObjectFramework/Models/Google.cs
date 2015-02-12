﻿using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class Google : PageObject
    {
        public Google(IWebDriver driver)
            : base(driver)
        {
            _url = "http://www.google.com";
            GoTo(_url, "Google");
        }

        private static readonly By _inputSearch = By.Name("q");
        private static readonly By _btnSearch = By.Name("btnG");

        /* Here are two example methods of using the newly generated objects with friendlier
         * names so that it clearly identifies the task being completed
         * Note that there is no validation on these, so if they are to fail there will 
         * be no friendly output provided other than the failure.
         */
        public Google AppendSearchText(string text)
        {
            SendKeys(_inputSearch, text);
            return this;
        }
        public Google EnterSearchText(string text)
        {
            ClearAndSendKeys(_inputSearch, text);
            return this;
        }
        public Google Search()
        {
            Click(_btnSearch);
            return this;
        }
        public Google ReturnToGoogle()
        {
            GoTo(_url, "Google");
            return new Google(_driver);
        }
    }
}
