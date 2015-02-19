using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using System.IO;
using LOGGER = PageObjectFramework.Framework.SeleniumLogger;

namespace PageObjectFramework.Framework
{
    public class PageObjectTest<TWebDriver> : SeleniumDriver<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private Stopwatch _suiteStopwatch;
        private Stopwatch _testStopwatch;

        private string PASS = "PASS";
        private string FAIL = "FAIL";

        private string _logName = SeleniumSettings.SeleniumLogName;

        private bool _takeScreenshotOnFail = SeleniumSettings.TakeScreenshotOnTestFail;
        private bool _takeScreenshotOnPass = SeleniumSettings.TakeScreenshotOnTestPass;
        private string _screenshotDirectory = SeleniumSettings.ScreenshotDirectory;

        private bool _logStackTrace = SeleniumSettings.LogStackTrace;
        private string _stacktraceDir = SeleniumSettings.StacktraceDirectory;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // This is where you put anything that needs to be run at the 
            // beginning of the test suite.

            LOGGER.GetLogger(_logName).LogStartTestSuite();
            var browser = Driver.GetType().ToString().Split('.')[2];
            LOGGER.GetLogger(_logName).LogBrowser(browser);
            _suiteStopwatch = Stopwatch.StartNew();
        }

        [SetUp]
        public void TestSetUp()
        {
            // This is where you would put anything that needs to be run at 
            // the beginning of each individual test

            LOGGER.GetLogger(_logName).LogStartTest(
                TestContext.CurrentContext.Test.Name);
            _testStopwatch = Stopwatch.StartNew();
        }

        [TearDown]
        public void TestTearDown()
        {
            // This is where you would put anything that needs to be run at
            // the end of each individual test.
            var context = TestContext.CurrentContext;
            var testname = context.Test.FullName.Split('.')[2].Split('<')[0];
            var methodname = context.Test.Name;
            var browser = Driver.GetType().ToString().Split('.')[2];

            if (context.Result.Status == TestStatus.Passed)
            {
                LOGGER.GetLogger(_logName).LogPass(context.Test.Name);
                if (_takeScreenshotOnPass)
                {
                    TakeScreenshot(PASS, testname, methodname, browser);
                }
            }
            else
            {
                LOGGER.GetLogger(_logName).LogFail(context.Test.Name);

                if (_logStackTrace)
                {
                    Stacktrace.AddContext(context);
                    Stacktrace.AddBrowser(browser);

                    var _stackFilePath = string.Format("{0}{1}.cs__{2}()__{3}__StackTrace.txt",
                        _stacktraceDir,
                        testname,
                        methodname,
                        browser);

                    LOGGER.GetLogger(_logName).LogInfo(string.Format("Stacktrace file saved at {0}", _stackFilePath));
                }

                if (_takeScreenshotOnFail)
                {
                    TakeScreenshot(FAIL, testname, methodname, browser);
                }
            }
            _testStopwatch.Stop();
            LOGGER.GetLogger(_logName).LogTime("Elapsed Time", _testStopwatch.Elapsed);
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Quit();
            Driver = null;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // This is where you put anything that needs to be run at the 
            // end of the test suite.
            LOGGER.GetLogger(_logName).LogFinishTestSuite();
            _suiteStopwatch.Stop();
            LOGGER.GetLogger(_logName).LogTime("Total Time", _suiteStopwatch.Elapsed);
            LOGGER.GetLogger(_logName).LogDashedLine();
            KillDrivers();
        }

        private void TakeScreenshot(string passOrFail, string testname, string methodname, string browser)
        {
            CreateScreenshotDirectory();
            var ss = ((ITakesScreenshot)Driver).GetScreenshot();
            var context = TestContext.CurrentContext.Test;

            var sslocation = string.Format(@"{0}{1}-{2}.cs__{3}()__{4}.png",
                    _screenshotDirectory,
                    passOrFail,
                    testname,
                    methodname,
                    browser);

            ss.SaveAsFile(sslocation, System.Drawing.Imaging.ImageFormat.Png);
            LOGGER.GetLogger(_logName).LogInfo(string.Format("Screenshot saved at {0}", sslocation));
        }

        private void CreateScreenshotDirectory()
        {
            if (!Directory.Exists(_screenshotDirectory))
            {
                Directory.CreateDirectory(_screenshotDirectory);
            }
        }

        private void KillDrivers()
        {
            // Chrome
            foreach (var p in Process.GetProcessesByName("chromedriver"))
            {
                p.Kill();
            }

            // IE
            foreach (var p in Process.GetProcessesByName("Command line server for the IE driver"))
            {
                p.Kill();
            }
        }
    }
}
