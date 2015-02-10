using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models
{
    public class YouTube : PageObjectModelBase
    {
        private IWebDriver Driver { get; set; }
        private static readonly string _url = "http://www.youtube.com";

        public YouTube(IWebDriver driver)
            : base(driver)
        {
            Driver = driver;
            GoTo(_url, "YouTube");
        }

        private static readonly By SearchDialog = By.Name("search_query");
        private static readonly By SearchButton = By.Id("search-btn");
        private static readonly ByFormatter VideoByIndex = ByFormatter.XPath("(//h3[@class='yt-lockup-title'])[{0}]");

        public YouTube SearchYouTube(string text)
        {
            ClearAndSendKeys(SearchDialog, text);
            Click(SearchButton);
            Thread.Sleep(2000);
            return this;
        }

        public YouTube ClickVideoAtIndex(int index)
        {
            Click(VideoByIndex.Format(index));
            return this;
        }

        public YouTube FullScreen()
        {
            for (int x = 0; x < 10; x++)
            {
                SendKeys(Body, Keys.Tab);
            }
            SendKeys(Body, Keys.Enter);
            return this;
        }
    }
}
