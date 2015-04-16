using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;
using System;

namespace PageObjectFramework.Framework
{
    public class SeleniumDriver
    {
        private static IWebDriver _driver;
        private static string _driverDirectory = SeleniumSettings.DriverDirectory;
        private static string driverType = SeleniumSettings.Driver;
        
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    switch(driverType.ToLower())
                    {
                        case "chrome":
                            _driver = new ChromeDriver(_driverDirectory);
                            break;
                        case "ie":
                        case "internet explorer":
                        case "internet":
                            _driver = new InternetExplorerDriver(_driverDirectory);
                            break;
                        case "safari":
                            _driver = new SafariDriver();
                            break;
                        case "phantom":
                        case "phantomjs":
                            _driver = new PhantomJSDriver();
                            break;
                        case "firefox":
                        default:
                            _driver = new FirefoxDriver();
                            break;
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
            int timeout = SeleniumSettings.DefaultTimeout;
            if (timeout != 0)
            {
                _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, timeout));
            }
            else
            {
                _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 15));
            }
        }
    }
}
