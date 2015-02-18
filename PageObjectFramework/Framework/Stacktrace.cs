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
    public class Stacktrace
    {
        private static string _stacktraceDir = ConfigurationManager.AppSettings["stacktraceDirectory"];

        internal static void LogStackTrace(TestContext context, string browser)
        {
            var testname = context.Test.FullName.Split('.')[2].Split('<')[0];
            var methodname = context.Test.Name;

            var _stackFilePath = string.Format("{0}{1}.cs__{2}()__{3}__StackTrace.txt",
                _stacktraceDir,
                testname,
                methodname,
                browser);

            var stackTrace = new StackTrace(true);

            using (var outfile = new StreamWriter(_stackFilePath, false))
            {
                outfile.WriteLine(stackTrace.ToString());
            }
        }
    }
}
