using NUnit.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectFramework.Framework
{
    // I had to follow the Singleton Design Pattern when creating this class.
    // I needed The TestResult, which is only accessible from the NUnitExtension
    // class, but I also needed the TestContext, which is already lost by the
    // time we reached that method in the NUnitExtension class.
    //
    // This class creates a Stack Trace file with name:
    // TestClassName.cs__TestMethodName()__Browser__Stacktrace.txt,
    // and will have both the failure message (from TestResult), including any
    // custom failure messages created with Assert.Fail(message), and the full
    // stack trace given on the failure (from TestContext).
    public class Stacktrace
    {
        private static string _stacktraceDir = ConfigurationManager.AppSettings["stacktraceDirectory"];

        private static Stacktrace _stackTrace;
        private static TestContext _context;
        private static string _browser;
        private static TestResult _result;
        private Stacktrace() { }

        public static Stacktrace GetTracer()
        {
            if (_stackTrace == null)
            {
                _stackTrace = new Stacktrace();
            }
            return _stackTrace;
        }
        public static void AddContext(TestContext context)
        {
            _context = context;
        }
        public static void AddBrowser(string browser)
        {
            _browser = browser;
        }
        public static void AddTestResult(TestResult result)
        {
            _result = result;
        }

        internal static void LogStackTrace()
        {
            if (_context != null && _context.Result.Status == TestStatus.Failed)
            {
                var testname = _context.Test.FullName.Split('.')[2].Split('<')[0];
                var methodname = _context.Test.Name;

                var _stackFilePath = string.Format("{0}{1}.cs__{2}()__{3}__StackTrace.txt",
                    _stacktraceDir,
                    testname,
                    methodname,
                    _browser);

                var stackTrace = new StackTrace(true);

                using (var outfile = new StreamWriter(_stackFilePath, false))
                {
                    outfile.WriteLine(_result.Message);
                    outfile.WriteLine("");
                    outfile.WriteLine(stackTrace.ToString());
                }
            }

            _stackTrace = null;
            _context = null;
            _browser = null;
            _result = null;
        }
    }
}
