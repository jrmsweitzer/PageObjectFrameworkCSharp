using NUnit.Core;
using NUnit.Core.Extensibility;
using System;

namespace PageObjectFramework.Framework
{
    [NUnitAddinAttribute(Type = ExtensionType.Core,
        Name = "Log Failure Messages",
        Description = "Logs test results to the logger")]
    public class NUnitExtension : IAddin, EventListener
    {
        private string _logName = SeleniumSettings.SeleniumLogName;

        public bool Install(IExtensionHost host)
        {
            //LOGGER.GetLogger(LOGNAME).LogInfo("Install");
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            listeners.Install(this);
            return true;
        }

        public void RunStarted(string name, int testCount) { }
        public void SuiteStarted(NUnit.Core.TestName testName) { }
        public void TestStarted(NUnit.Core.TestName testName) { }
        public void TestFinished(NUnit.Core.TestResult result)
        {
            Stacktrace.AddTestResult(result);
            Stacktrace.LogStackTrace();
        }
        public void SuiteFinished(NUnit.Core.TestResult result) { }
        public void RunFinished(NUnit.Core.TestResult result) { }

        public void TestOutput(NUnit.Core.TestOutput testOutput) { }
        public void RunFinished(Exception exception) { }
        public void UnhandledException(Exception exception) { }
    }
}
