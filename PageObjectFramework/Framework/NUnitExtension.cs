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
        public bool Install(IExtensionHost host)
        {
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            listeners.Install(this);
            return true;
        }

        public void TestFinished(NUnit.Core.TestResult result)
        {
            var res = result;
        }

        public void TestStarted(NUnit.Core.TestName testName)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void TestOutput(NUnit.Core.TestOutput testOutput)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void RunStarted(string name, int testCount)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void RunFinished(NUnit.Core.TestResult result)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void RunFinished(Exception exception)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void SuiteStarted(NUnit.Core.TestName testName)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void SuiteFinished(NUnit.Core.TestResult result)
        {
            //this is one of the unused event handlers. It remains empty.
        }

        public void UnhandledException(Exception exception)
        {
            //this is one of the unused event handlers. It remains empty.
        }
    }
}
