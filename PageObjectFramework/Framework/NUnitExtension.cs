using NUnit.Core;
using NUnit.Core.Extensibility;
using NUnit.Framework;
using System;
using System.Configuration;
using LOGGER = PageObjectFramework.Framework.SeleniumLogger;

namespace PageObjectFramework.Framework
{
    [NUnitAddinAttribute(Type = ExtensionType.Core,
        Name = "Log Failure Messages",
        Description = "Logs test results to the logger")]
    public class NUnitExtension : IAddin, EventListener
    {
        private string LOGNAME = ConfigurationManager.AppSettings["seleniumLogName"];

        public bool Install(IExtensionHost host)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("Install");
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            listeners.Install(this);
            return true;
        }

        public void RunStarted(string name, int testCount)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("RunStarted");
        }

        public void SuiteStarted(NUnit.Core.TestName testName)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("SuiteStarted");
        }
        public void TestStarted(NUnit.Core.TestName testName)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("TestStarted");
        }
        public void TestFinished(NUnit.Core.TestResult result)
        {
            Stacktrace.AddTestResult(result);
            Stacktrace.LogStackTrace();
        }
        public void SuiteFinished(NUnit.Core.TestResult result)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("SuiteFinished");
        }
        public void RunFinished(NUnit.Core.TestResult result)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("RunFinished");
        }


        public void TestOutput(NUnit.Core.TestOutput testOutput)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("TestOutput");
        }
        public void RunFinished(Exception exception)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("RunFinished");
        }
        public void UnhandledException(Exception exception)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("UnhandledException");
        }
    }
}
