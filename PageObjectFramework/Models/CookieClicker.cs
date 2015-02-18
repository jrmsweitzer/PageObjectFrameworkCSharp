using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class CookieClicker : PageObject
    {
        public CookieClicker(IWebDriver driver) : base(driver)
        {
            _url = "http://orteil.dashnet.org/cookieclicker/";
            GoTo(_url);
            Sleep(1000);
        }

        private static readonly By _btnBigCookie = By.Id("bigCookie");
        private static readonly By _btnCursor = By.Id("productName0");
        private static readonly By _btnGrandma = By.Id("productName1");
        private static readonly By _btnFarm = By.Id("productName2");
        private static readonly By _btnFactory = By.Id("productName3");

        /// <summary>
        /// Clicks the big cookie
        /// <para> @param numTimes - the number of times to click the big cookie.</para>
        /// </summary>
        public CookieClicker ClickCookie(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                Click(_btnBigCookie);
            }
            return this;
        }
    
        /// <summary>
        /// Buys a Cursor if you have enough funds.
        /// </summary>
        public CookieClicker BuyCursor()
        {
            Click(_btnCursor);
            return this;
        }

        /// <summary>
        /// Buys a Grandma if you have enough funds.
        /// </summary>
        public CookieClicker BuyGrandma()
        {
            Click(_btnGrandma);
            return this;
        }

        /// <summary>
        /// Buys a Farm if you have enough funds.
        /// </summary>
        public CookieClicker BuyFarm()
        {
            Click(_btnFarm);
            return this;
        }

        /// <summary>
        /// Buys a Factory if you have enough funds.
        /// </summary>
        public CookieClicker BuyFactory()
        {
            Click(_btnFactory);
            return this;
        }
    }
}
