using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class CookieClicker : PageObjectModelBase
    {
        private string _url = "http://orteil.dashnet.org/cookieclicker/";

        public CookieClicker(IWebDriver driver) : base(driver)
        {
            GoTo(_url);
        }

        private static readonly By BigCookie = By.Id("bigCookie");
        private static readonly By Cursor = By.Id("productName0");
        private static readonly By Grandma = By.Id("productName1");
        private static readonly By Farm = By.Id("productName2");
        private static readonly By Factory = By.Id("productName3");

        /// <summary>
        /// Clicks the big cookie
        /// <para> @param numTimes - the number of times to click the big cookie.</para>
        /// </summary>
        public CookieClicker ClickCookie(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                Click(BigCookie);
            }
            return this;
        }
    
        /// <summary>
        /// Buys a Cursor if you have enough funds.
        /// </summary>
        public CookieClicker BuyCursor()
        {
            Click(Cursor);
            return this;
        }

        /// <summary>
        /// Buys a Grandma if you have enough funds.
        /// </summary>
        public CookieClicker BuyGrandma()
        {
            Click(Grandma);
            return this;
        }

        /// <summary>
        /// Buys a Farm if you have enough funds.
        /// </summary>
        public CookieClicker BuyFarm()
        {
            Click(Farm);
            return this;
        }

        /// <summary>
        /// Buys a Factory if you have enough funds.
        /// </summary>
        public CookieClicker BuyFactory()
        {
            Click(Factory);
            return this;
        }
    }
}
