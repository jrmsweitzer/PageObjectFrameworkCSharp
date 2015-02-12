using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models.Heroku
{
    public class HerokuCheckboxes : PageObject, IHerokuApp
    {

        public HerokuCheckboxes(IWebDriver driver) : base(driver)
        {
        }

        public static readonly By _cbUnchecked = By.XPath("//form/input[1]");
        public static readonly By _cbChecked = By.XPath("//form/input[2]");

        /// <summary>
        /// Toggles the Unchecked checkbox
        /// <para>@param numTimes - the number of times to toggle it.</para>
        /// </summary>
        public HerokuCheckboxes ToggleUncheckedBox(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                Click(_cbUnchecked);
                Thread.Sleep(1000);
            }
            return this;
        }

        /// <summary>
        /// Toggles the Checked checkbox
        /// <para>@param numTimes - the number of times to toggle it.</para>
        /// </summary>
        public HerokuCheckboxes ToggleCheckedBox(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                Click(_cbChecked);
                Thread.Sleep(1000);
            }
            return this;
        }
    }
}
