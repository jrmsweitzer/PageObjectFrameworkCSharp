using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class YouTube : PageObject
    {
        /// <summary>
        /// The YouTube home page.
        /// </summary>
        public YouTube(IWebDriver driver)
            : base(driver)
        {
            _url = "http://www.youtube.com";
            GoTo(_url, "YouTube");
        }

        private static readonly By _inputSearch = By.Name("search_query");
        private static readonly By _btnSearch = By.Id("search-btn");
        private static readonly ByFormatter _linkVideoByIndex = ByFormatter.XPath("(//h3[@class='yt-lockup-title'])[{0}]");

        /// <summary>
        /// Searches YouTube for the given query
        /// <para> @param text - the text to search for on YouTube.</para>
        /// </summary>
        public YouTube SearchYouTube(string text)
        {
            ClearAndSendKeys(_inputSearch, text);
            Click(_btnSearch);
            Thread.Sleep(2000);
            return this;
        }

        /// <summary>
        /// Clicks video that is nth video down from the search results
        /// <para> @param n - the index of the video to click</para>
        /// </summary>
        public YouTube ClickVideoAtIndex(int n)
        {
            Click(_linkVideoByIndex.Format(n));
            return this;
        }
    }
}
