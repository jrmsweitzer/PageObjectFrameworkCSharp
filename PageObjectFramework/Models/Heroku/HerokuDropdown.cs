using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models.Heroku
{
    public class HerokuDropdown : PageObject, IHerokuApp
    {

        public HerokuDropdown(IWebDriver driver) : base(driver)
        {
        }

        public static readonly By Dropdown = By.Id("dropdown");

        public static readonly string Option1 = "Option 1";
        public static readonly string Option2 = "Option 2";

        /// <summary>
        /// Selects Option based on the given text
        /// <para> @param option - the option to select</para>
        /// </summary>
        public HerokuDropdown SelectOption(string option)
        {
            SelectByText(Dropdown, option);
            Thread.Sleep(1000);
            return this;
        }
    }
}
