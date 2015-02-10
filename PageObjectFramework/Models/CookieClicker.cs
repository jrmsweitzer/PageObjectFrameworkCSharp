using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class CookieClicker : PageObjectModelBase
    {
        private IWebDriver Driver { get; set; }
        private string _url = "http://orteil.dashnet.org/cookieclicker/";

        public CookieClicker(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            GoTo(_url);
        }

        private static readonly By BigCookie = By.Id("bigCookie");
        private static readonly By Cursor = By.Id("productName0");
        private static readonly By Grandma = By.Id("productName1");
        private static readonly By Farm = By.Id("productName2");
        private static readonly By Factory = By.Id("productName3");

        public CookieClicker ClickCookie(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                Click(BigCookie);
            }
            return this;
        }
    
        public CookieClicker BuyCursor()
        {
            Click(Cursor);
            return this;
        }

        public CookieClicker BuyGrandma()
        {
            Click(Grandma);
            return this;
        }

        public CookieClicker BuyFarm()
        {
            Click(Farm);
            return this;
        }

        public CookieClicker BuyFactory()
        {
            Click(Factory);
            return this;
        }
    }
}
