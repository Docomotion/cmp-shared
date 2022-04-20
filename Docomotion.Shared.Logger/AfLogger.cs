using System;
using System.IO;
using System.Xml;
using log4net;
using Microsoft.Win32;

namespace Docomotion.Shared.Logger
{
    public class FFLog4net
    {
        private ILog m_Log;

        private XmlDocument m_DefaultConfig = null;

        private const string m_DefaultSet = "<log4net>" +
                                                "<appender type=\"log4net.Appender.RollingFileAppender\" name=\"FreeForm\">" +
                                                  "<file value=\"default_log.txt\" />" +
                                                  "<threshold value=\"ALL\" />" +
                                                  "<appendToFile value=\"true\" />" +
                                                  "<rollingStyle value=\"Size\" />" +
                                                  "<maxSizeRollBackups value=\"10\" />" +
                                                  "<maximumFileSize value=\"1MB\" />" +
                                                  "<staticLogFileName value=\"true\" />" +
                                                  "<lockingModel type=\"log4net.Appender.FileAppender+MinimalLock\" />" +
                                                  "<layout type=\"log4net.Layout.PatternLayout\">" +
                                                    "<param name=\"ConversionPattern\" value=\"%date :%2thread %-5level %logger_%property{SessionGuid}:%message%newline\" />" +
                                                  "</layout>" +
                                                  "<filter type=\"log4net.Filter.LevelRangeFilter\">" +
                                                    "<param name = \"LevelMin\" value=\"DEBUG\"/>" +
                                                    "<param name = \"LevelMax\" value=\"ERROR\"/>" +
                                                  "</filter>" +
                                                "</appender>" +
                                                "<root>" +
                                                  "<level value=\"ALL\" />" +
                                                  "<appender-ref ref=\"FreeForm\" />" +
                                                "</root>" +
                                              "</log4net>";

        private FFLog4net(string configPath = null, string loggerName = "FreeForm_Logger", object logInstance = null)
        {
            if (logInstance == null)
            {
                 GetLoggerByPath(configPath, loggerName);
            }
            else
            {
                try
                {
                      m_Log = (ILog)logInstance;
                }
                catch
                {
                    GetLoggerByPath(configPath, loggerName);
                }
            }
        }

        private void SetDefaultConfiguration()
        {
            try
            {
                m_DefaultConfig = new XmlDocument();
                m_DefaultConfig.LoadXml(m_DefaultSet);

                log4net.Config.XmlConfigurator.Configure(m_DefaultConfig.DocumentElement);
            }
            catch { }
        }

        private void GetLoggerByPath(string configPath, string loggerName)
        {
            if (string.IsNullOrWhiteSpace(configPath))
                SetDefaultConfiguration();
            else
                log4net.Config.XmlConfigurator.Configure(new Uri(configPath));

            m_Log = LogManager.GetLogger(loggerName);
        }

        private static FFLog4net m_Instance = null;
        public static FFLog4net Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (typeof(FFLog4net))
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new FFLog4net();
                        }
                    }
                }

                return m_Instance;
            }
        }

        static public void Create(string configPath, string loggerName, object logInstance = null)
        {
            if (m_Instance == null)
            {
                lock (typeof(FFLog4net))
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new FFLog4net(configPath, loggerName, logInstance);
                    }
                }
            }
        }

        static public void CreateByRegistry(string loggerName)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Autofont\FreeForm\RT");

            string rtPath = null;

            if (registryKey != null)
            {
                rtPath = (string)registryKey.GetValue("Path");

                if(!string.IsNullOrEmpty(rtPath)) rtPath = Path.Combine(rtPath, "FreeFormEngine.config");
            }

            Create(rtPath, loggerName);
        }

        public void Error(string message, Exception exception)
        {
           m_Log.Error(message, exception);
        }

        public void Info(string message)
        {
            m_Log.Info(message);
        }

        public void SetSessionGuid(string message, string guid)
        {
            log4net.LogicalThreadContext.Properties["SessionGuid"] = guid;
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

        public void Warn(string message, Exception exception)
        {
            m_Log.Warn(message, exception);
        }

        public void Warn(string message)
        {
            m_Log.Warn(message);
        }
    }
}

 


