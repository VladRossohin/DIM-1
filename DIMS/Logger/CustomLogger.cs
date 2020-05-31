using NLog;
using System;

namespace DIMS.Logger
{
    public static class CustomLogger
    {
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public static void WriteLog(LogLevel logType, Exception exception, string message, params string[] args)
        {
            var loggerEventEmitter = new LogEventInfo(logType, logger.Name, null, message, args);

            if (exception != null)
            {
                loggerEventEmitter.Exception = exception;

                if (exception.InnerException != null)
                {
                    loggerEventEmitter.Exception = exception.InnerException;
                }
            }

            // check with wrapper type typeof(Logger);
            logger.Log(typeof(CustomLogger), loggerEventEmitter);
        }

        public static void Info(string message, params string[] args)
        {
            WriteLog(LogLevel.Info, null, message, args);
        }

        // log not trivial situation
        public static void Warning(string message, params string[] args)
        {
            WriteLog(LogLevel.Warn, null, message, args);
        }

        public static void Error(string message, Exception exception, params string[] args)
        {
            WriteLog(LogLevel.Error, exception, message, args);
        }

        public static void Error(Exception exception)
        {
            WriteLog(LogLevel.Error, null, "Error occured. Error Message='{0}'\r\n StackTrace={1}", exception.Message, exception.StackTrace);
        }

        public static void Trace(string message, params string[] args)
        {
            WriteLog(LogLevel.Trace, null, message, args);
        }

        public static void Debug(string message, params string[] args)
        {
            WriteLog(LogLevel.Debug, null, message, args);
        }
    }
}
