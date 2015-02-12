using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class Weather : PageObjectModelBase
    {
        public Weather(IWebDriver driver) : base(driver)
        {
            _url = "http://www.weather.gov/";
            _title = "National Weather Service";
            GoTo(_url, _title);
        }

        private static readonly By LocationInput = By.Id("inputstring");
        private static readonly By GoButton = By.Id("btnSearch");

        private static readonly By TomorrowDayText = By.XPath("(//div[@class='one-ninth-first'])[3]/p[@class='txt-ctr-caps']");
        private static readonly ByFormatter TomorrowText = ByFormatter.XPath("//span[.='{0}']/..");

        /// <summary>
        /// inputs your location into the location box
        /// <para> @param location - either your location (Baltimore, MD), or a zip code.</para>
        /// </summary>
        public Weather EnterLocation(string location)
        {
            SendKeys(LocationInput, location);
            Click(GoButton);
            return this;
        }

        /// <summary>
        /// Gets the text for tomorrow's weather.
        /// </summary>
        /// <returns></returns>
        public string GetTomorrowsWeather()
        {
            string tomorrow = ConvertToPascalCase(GetText(TomorrowDayText));
            Assert.AreEqual("Friday", tomorrow);
            return GetText(TomorrowText.Format(tomorrow));
        }

        private string ConvertToPascalCase(string dayOfWeek)
        {
            dayOfWeek = dayOfWeek.ToLower();
            return dayOfWeek.Substring(0, 1).ToUpper() + dayOfWeek.Substring(1, dayOfWeek.Length - 1);
            
        }
    }
}
