using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace PageObjectFramework.Framework
{
    public class SeleniumLogger
    {
        #region Constant Strings
        protected const string Warning = " WARN ";
        protected const string Info = " INFO ";
        protected const string Error = " ERROR";
        protected const string Start = " START";
        protected const string Finish = " FNSHD";
        protected const string Pass = " PASS ";
        protected const string Fail = " FAIL ";
        protected const string Time = " TIME ";
        protected const string Message = " -----";
        protected const string DashedLine = "-------------------------------------------------";
        #endregion

        private static readonly Dictionary<string, SeleniumLogger> LoggerDict =
            new Dictionary<string, SeleniumLogger>();
        private string _logFilePath;

        // The Directory of the log files.
        private string _logDir = ConfigurationManager.AppSettings["logDirectory"];

        /** SeleniumLogger GetLogger(string descriptiveLogName)
         * 
         * @param - descriptive name of log.
         * ie. TestDetails, DatabaseCalls, etc.
         * 
         * Shows up in _logDir/DATE_descriptiveLogName.txt
         * 
         * This method uses a modified Singleton Design Pattern.
         * In the Singleton Design Pattern, the constructor is set to private,
         * and there is a static GetObject method and a private static instance
         * of that object. For instance, in the normal Singleton Pattern, I
         * would create a private static _logger, and the GetLogger() method
         * would call the constructor if _logger is null before sending back
         * _logger, or just simply return _logger if it has already been
         * constructed. 
         * 
         * In this modified version of the Singleton Design Pattern, we have a
         * private static List<Logger>, and when GetLogger() is called, it 
         * checks that list to see if it's already created, before returning it,
         * creating it if necessary.
         */
        public static SeleniumLogger GetLogger(string descriptiveLogName)
        {
            SeleniumLogger loggerInst;
            if (!LoggerDict.ContainsKey(descriptiveLogName))
            {
                loggerInst = new SeleniumLogger(descriptiveLogName);
                LoggerDict.Add(descriptiveLogName, loggerInst);
            }
            return LoggerDict[descriptiveLogName];
        }

        #region Private Methods
        // Private Constructor. Only accessible through the public
        // GetLogger(descriptiveLogName) method.
        private SeleniumLogger(string descriptiveLogName)
        {
            CreateLogDirectory();
            var datetime = DateTime.Now;
            this._logFilePath = string.Format("{0}{1}_{2}.txt",
                    _logDir,
                    datetime.ToString("yyyy-MM-dd"),
                    descriptiveLogName);

            if (!File.Exists(this._logFilePath))
            {
                WriteStartMessage(datetime);
            }
        }

        private void CreateLogDirectory()
        {
            if (!Directory.Exists(_logDir))
            {
                Directory.CreateDirectory(_logDir);
            }
        }

        private void WriteStartMessage(DateTime datetime)
        {
            Log("Starting Log...", Message);
        }

        private void Log(string message, string level)
        {
            const string msgfmt = "{0}{1}- {2}";
            var datetime = DateTime.Now;

            using (var outfile = new StreamWriter(_logFilePath, true))
            {
                outfile.WriteLine(msgfmt, 
                    datetime.ToString("HH:mm:ss"), level, message);
            }
        }
        #endregion

        #region Public Methods
        public void LogDashedLine()
        {
            Log(DashedLine, Message);
        }

        public void LogError(string errorMessage)
        {
            Log(errorMessage, Error);
        }

        public void LogFail(string testName)
        {
            Log(testName + "() failed.", Fail);
        }

        public void LogFinishTestSuite()
        {
            LogDashedLine();
            Log("Finished Test Suite!", Finish);
            LogDashedLine();
        }

        public void LogInfo(string infoMessage)
        {
            Log(infoMessage, Info);
        }

        public void LogMessage(string message)
        {
            Log(message, Message);
        }

        public void LogPass(string testName)
        {
            Log(testName + "() passed!", Pass);
        }

        public void LogStartTest(string testName)
        {
            LogDashedLine();
            Log(testName + "() started!", Start);
            LogDashedLine();
        }

        public void LogStartTestSuite()
        {
            LogDashedLine();
            Log("Starting Test Suite!", Start);
        }

        public void LogTime(string message, TimeSpan timeSpan)
        {
            Log(string.Format("{0}: {1}",
                message, timeSpan), Time);
        }

        public void LogWarning(string warningMessage)
        {
            Log(warningMessage, Warning);
        }
        #endregion
    }
}
