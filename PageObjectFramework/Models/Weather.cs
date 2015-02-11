using OpenQA.Selenium;
using PageObjectFramework.Framework;

namespace PageObjectFramework.Models
{
    public class Weather : PageObjectModelBase
    {
        private IWebDriver Driver;
        private string _url = "http://www.weather.gov/";
        private string _title = "National Weather Service";

        public Weather(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            GoTo(_url, _title);
        }

        private static readonly By LocationInput = By.Id("inputstring");
        private static readonly By GoButton = By.Id("btnSearch");
        private static readonly By TomorrowText = By.XPath("//span[.='Thursday']/..");

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
            return GetText(TomorrowText);
        }
    }
}
