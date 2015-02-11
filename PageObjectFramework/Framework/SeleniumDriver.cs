using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Configuration;

namespace PageObjectFramework.Framework
{
    public class SeleniumDriver
    {
        private static IWebDriver _driver;
        
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    string driverConfig = ConfigurationManager.AppSettings["browser"];
                    if (!string.IsNullOrEmpty(driverConfig))
                    {
                        switch (driverConfig)
                        {
                            case "Chrome":
                                _driver = new ChromeDriver(@"../../Drivers");
                                ConfigureDriver();
                                break;
                            case "Firefox":
                                _driver = new FirefoxDriver();
                                ConfigureDriver();
                                break;
                            case "IE":
                                _driver = new InternetExplorerDriver(@"../../Drivers");
                                ConfigureDriver();
                                break;
                            default:
                                Console.WriteLine("App.config key error.");
                                Console.WriteLine("Defaulting to Firefox");
                                _driver = new ChromeDriver();
                                ConfigureDriver();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("* * * * DEFAULTMODE * * * *");
                        Console.WriteLine("App.config key not present.");
                        _driver = new ChromeDriver();
                        ConfigureDriver();
                    }
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
