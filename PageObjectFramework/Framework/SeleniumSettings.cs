using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectFramework.Framework
{
    public class SeleniumSettings
    {
        public static string ActionLogName
        {
            get
            {
                return ConfigurationManager.AppSettings["actionLogName"];
            }
        }

        public static int DefaultTimeout
        {
            get
            {
                return Int32.Parse(
                    ConfigurationManager.AppSettings["defaultTimeout"]);
            }
        }

        public static string Driver
        {
            get
            {
                return ConfigurationManager.AppSettings["driver"];
            }
        }

        public static string DriverDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["driverDirectory"];
            }
        }

        public static bool LogAllActions
        {
            get
            {
                return ConfigurationManager.AppSettings["logAllActions"] == "true" ?
                    true :
                    false;
            }
        }

        public static string LogDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["logDirectory"];
            }
        }

        public static bool LogStackTrace
        {
            get
            {
                return ConfigurationManager.AppSettings["logStackTrace"] == "true" ?
                    true :
                    false;
            }
        }

        public static string ScreenshotDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["screenshotDirectory"];
            }
        }

        public static string SeleniumLogName
        {
            get
            {
                return ConfigurationManager.AppSettings["seleniumLogName"];
            }
        }

        public static string StacktraceDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["stacktraceDirectory"];
            }
        }

        public static bool TakeScreenshotOnTestFail
        {
            get
            {
                return ConfigurationManager.AppSettings["takeScreenshotOnTestFail"] == "true" ?
                    true :
                    false;
            }
        }

        public static bool TakeScreenshotOnTestPass
        {
            get
            {
                return ConfigurationManager.AppSettings["takeScreenshotOnTestPass"] == "true" ?
                    true :
                    false;
            }
        }
    }
}
