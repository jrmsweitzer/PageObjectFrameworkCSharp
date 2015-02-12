using OpenQA.Selenium;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models.Heroku
{
    public class HerokuCheckboxes : PageObjectModelBase, IHerokuApp
    {

        public HerokuCheckboxes(IWebDriver driver) : base(driver)
        {
        }

        public static readonly By UncheckedCheckBox = By.XPath("//form/input[1]");
        public static readonly By CheckedCheckBox = By.XPath("//form/input[2]");

        /// <summary>
        /// Toggles the Unchecked checkbox
        /// <para>@param numTimes - the number of times to toggle it.</para>
        /// </summary>
        public HerokuCheckboxes ToggleUncheckedBox(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                Click(UncheckedCheckBox);
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
                Click(CheckedCheckBox);
                Thread.Sleep(1000);
            }
            return this;
        }
    }
}
