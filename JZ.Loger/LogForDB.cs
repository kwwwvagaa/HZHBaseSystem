using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JZ.Interfaces;

namespace JZ.Loger
{
    internal class LogForDB : ILoger
    {
        private LogForDB() { }
        private static ILoger _instance = new LogForDB();      
        public static ILoger Instance
        {
            get
            {
                return LogForDB._instance;
            }
        }

        public void InitLog(string strConfigFile)
        {
            throw new NotImplementedException();
        }

        public void WriteInfo(string msg)
        {
            throw new NotImplementedException();
        }

        public void WriteInfo(string msg, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void WriteError(string msg)
        {
            throw new NotImplementedException();
        }

        public void WriteError(string msg, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void WriteWarning(string msg)
        {
            throw new NotImplementedException();
        }

        public void WriteWarning(string msg, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void WriteDebug(string msg)
        {
            throw new NotImplementedException();
        }

        public void WriteDebug(string msg, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
