using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using LOGGER = PageObjectFramework.Framework.SeleniumLogger;

namespace PageObjectFramework.Framework
{
    public class PageObjectTestBase : SeleniumDriver
    {
        private string _screenshotDirectory = ConfigurationManager.AppSettings["screenshotDirectory"];

        private Stopwatch _suiteStopwatch;
        private Stopwatch _testStopwatch;

        private string LOGNAME = ConfigurationManager.AppSettings["seleniumLogName"];
        private string PASS = "PASS";
        private string FAIL = "FAIL";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // This is where you put anything that needs to be run at the 
            // beginning of the test suite.

            LOGGER.GetLogger(LOGNAME).LogStartTestSuite();
            _suiteStopwatch = Stopwatch.StartNew();
        }

        [SetUp]
        public void TestSetUp()
        {
            // This is where you would put anything that needs to be run at 
            // the beginning of each individual test

            LOGGER.GetLogger(LOGNAME).LogStartTest(
                TestContext.CurrentContext.Test.Name);
            _testStopwatch = Stopwatch.StartNew();
        }

        [TearDown]
        public void TestTearDown()
        {
            // This is where you would put anything that needs to be run at
            // the end of each individual test.
            var context = TestContext.CurrentContext;
            if (context.Result.Status == TestStatus.Passed)
            {
                LOGGER.GetLogger(LOGNAME).LogPass(context.Test.Name);
                TakeScreenshot(PASS);
            }
            else
            {
                LOGGER.GetLogger(LOGNAME).LogFail(context.Test.Name);
                TakeScreenshot(FAIL);
            }
            _testStopwatch.Stop();
            LOGGER.GetLogger(LOGNAME).LogTime("Elapsed Time", _testStopwatch.Elapsed);
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Quit();
            Driver = null;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // This is where you put anything that needs to be run at the 
            // end of the test suite.
            LOGGER.GetLogger(LOGNAME).LogFinishTestSuite();
            _suiteStopwatch.Stop();
            LOGGER.GetLogger(LOGNAME).LogTime("Total Time", _suiteStopwatch.Elapsed);
            LOGGER.GetLogger(LOGNAME).LogDashedLine();
            LOGGER.GetLogger(LOGNAME).LogDashedLine();
            LOGGER.GetLogger(LOGNAME).LogDashedLine();
            KillChromeDrivers();
        }

        private void TakeScreenshot(string passOrFail)
        {
            CreateScreenshotDirectory();
            var ss = ((ITakesScreenshot)Driver).GetScreenshot();
            var context = TestContext.CurrentContext.Test;

            var testname = context.FullName.Split('.')[2];
            var methodname = context.Name;
            var browser = Driver.GetType().ToString().Split('.')[2];

            var sslocation = string.Format(@"{0}{1}-{2}.cs__{3}()__{4}.png",
                    _screenshotDirectory,
                    passOrFail,
                    testname,
                    methodname,
                    browser);

            ss.SaveAsFile(sslocation, System.Drawing.Imaging.ImageFormat.Png);
            LOGGER.GetLogger(LOGNAME).LogInfo(string.Format("Screenshot saved at {0}", sslocation));
        }

        private void CreateScreenshotDirectory()
        {
            if (!Directory.Exists(_screenshotDirectory))
            {
                Directory.CreateDirectory(_screenshotDirectory);
            }
        }

        private void KillChromeDrivers()
        {
            foreach (var p in Process.GetProcessesByName("chromedriver"))
            {
                p.Kill();
            }
        }
    }
}
