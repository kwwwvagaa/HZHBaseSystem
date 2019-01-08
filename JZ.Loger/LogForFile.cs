using JZ.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Loger
{
    internal class LogForFile : ILoger
    {
        private LogForFile() { }
        private static ILoger _instance = new LogForFile();
        /// <summary>
        /// log4net实例
        /// </summary>
        public static ILoger Instance
        {
            get
            {
                return LogForFile._instance;
            }
        }
        private static Dictionary<string, ILog> m_lstLog = new Dictionary<string, ILog>();
        public void InitLog(string strLog4NetConfigFile)
        {
            if (!File.Exists(strLog4NetConfigFile))
            {
                throw new FileNotFoundException("日志配置文件不存在。");
            }
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(strLog4NetConfigFile));

            m_lstLog["info_logo"] = log4net.LogManager.GetLogger("info_logo");
            m_lstLog["error_logo"] = log4net.LogManager.GetLogger("error_logo");
            m_lstLog["warning_logo"] = log4net.LogManager.GetLogger("warning_logo");
            m_lstLog["debug_logo"] = log4net.LogManager.GetLogger("debug_logo");
        }

        public void WriteInfo(string msg)
        {
            if (m_lstLog["info_logo"].IsInfoEnabled)
            {
                m_lstLog["info_logo"].Info(msg);
            }
        }

        public void WriteInfo(string msg, Exception ex)
        {
            if (m_lstLog["info_logo"].IsInfoEnabled)
            {
                m_lstLog["info_logo"].Info(msg, ex);
            }
        }

        public void WriteError(string msg)
        {
            if (m_lstLog["error_logo"].IsInfoEnabled)
            {
                m_lstLog["error_logo"].Error(msg);
            }
        }

        public void WriteError(string msg, Exception ex)
        {
            if (m_lstLog["error_logo"].IsInfoEnabled)
            {
                m_lstLog["error_logo"].Error(msg, ex);
            }
        }

        public void WriteWarning(string msg)
        {
            if (m_lstLog["warning_logo"].IsInfoEnabled)
            {
                m_lstLog["warning_logo"].Warn(msg);
            }
        }

        public void WriteWarning(string msg, Exception ex)
        {
            if (m_lstLog["warning_logo"].IsInfoEnabled)
            {
                m_lstLog["warning_logo"].Warn(msg, ex);
            }
        }

        public void WriteDebug(string msg)
        {
            if (m_lstLog["debug_logo"].IsInfoEnabled)
            {
                m_lstLog["debug_logo"].Debug(msg);
            }
        }

        public void WriteDebug(string msg, Exception ex)
        {
            if (m_lstLog["debug_logo"].IsInfoEnabled)
            {
                m_lstLog["debug_logo"].Debug(msg, ex);
            }
        }
    }
}
