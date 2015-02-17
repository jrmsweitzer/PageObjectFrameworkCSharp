using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;
using System;
using System.Configuration;

namespace PageObjectFramework.Framework
{
    public class SeleniumDriver<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private static IWebDriver _driver;
        private static string _driverDirectory = ConfigurationManager.AppSettings["driverDirectory"];
        
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    if (typeof(TWebDriver).Equals(typeof(FirefoxDriver)) ||
                        typeof(TWebDriver).Equals(typeof(SafariDriver)) ||
                        typeof(TWebDriver).Equals(typeof(PhantomJSDriver)))
                    {
                        _driver = new TWebDriver();
                    }
                    else
                    {
                        _driver = Activator.CreateInstance(typeof(TWebDriver), new object[] { _driverDirectory }) as IWebDriver;
                    }
                    ConfigureDriver();
                }
                return _driver;
            }
            protected set
            {
                _driver = value;
            }
        }

        internal static void ConfigureDriver()
        {
            SetTimeout();
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Window.Maximize();
        }

        private static void SetTimeout()
        {
            string strtimeout = ConfigurationManager.AppSettings["defaultTimeout"];
            int timeout;
            if (Int32.TryParse(strtimeout, out timeout))
            {
                if (timeout != 0)
                {
                    _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, timeout));
                }
            }
            else
            {
                _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 15));
            }
        }
    }
}
