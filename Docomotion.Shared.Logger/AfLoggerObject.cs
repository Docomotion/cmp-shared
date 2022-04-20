using System;
using System.Linq;
using log4net;
using log4net.Appender;

namespace Docomotion.Shared.Logger
{
    public class FFLog4netObject
    {
        private ILog m_Log;

        public FFLog4netObject() { }

        public void GetLoggerByPath(string configPath, string loggerName)
        {
            if (string.IsNullOrWhiteSpace(configPath))
                log4net.Config.XmlConfigurator.Configure();
            else
                log4net.Config.XmlConfigurator.Configure(new Uri(configPath));

            m_Log = LogManager.GetLogger(loggerName);
        }

        public void SetLogOutputPath(string logPath, string appenderName)
        {
            var fileAppender = LogManager.GetRepository().GetAppenders().First(appender => appender.Name.Equals(appenderName));

            string configLogPath = ((FileAppender)fileAppender).File;

            configLogPath = System.IO.Path.GetFileName(configLogPath);

            configLogPath = System.IO.Path.Combine(logPath, configLogPath);

            ((FileAppender)fileAppender).File = configLogPath;

            ((FileAppender)fileAppender).ActivateOptions();
        }

        public void Error(string message, Exception exception)
        {
            m_Log.Error(message, exception);
        }

        public void Info(string message)
        {
            m_Log.Info(message);

        }

        public void Info(string message, Exception exception)
        {
            m_Log.Info(message, exception);
        }

        public void Debug(string message)
        {
            m_Log.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            m_Log.Debug(message, exception);
        }
    }
}
