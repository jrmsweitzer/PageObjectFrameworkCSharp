using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;
using LOGGER = PageObjectFramework.Framework.SeleniumLogger;

namespace PageObjectFramework.Framework
{
    public class PageObjectTestBase : SeleniumDriver
    {
        private string _screenshotDirectory = @"C:\Selenium\Results\Screenshots\";

        private Stopwatch _suiteStopwatch;
        private Stopwatch _testStopwatch;

        private string LOGNAME = "SeleniumLog";
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
        }

        private void TakeScreenshot(string passOrFail)
        {
            CreateScreenshotDirectory();
            var ss = ((ITakesScreenshot)Driver).GetScreenshot();
            var sslocation = string.Format(@"{0}{1}_{2}_{3}.jpeg",
                    _screenshotDirectory,
                    passOrFail,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    TestContext.CurrentContext.Test.Name);
            ss.SaveAsFile(sslocation, System.Drawing.Imaging.ImageFormat.Jpeg);
            LOGGER.GetLogger(LOGNAME).LogInfo(string.Format("Screenshot saved at {0}", sslocation));
        }

        private void CreateScreenshotDirectory()
        {
            if (!Directory.Exists(_screenshotDirectory))
            {
                Directory.CreateDirectory(_screenshotDirectory);
            }
        }
    }
}
