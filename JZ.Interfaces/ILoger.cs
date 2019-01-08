using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Interfaces
{
    public interface ILoger
    {
        /// <summary>
        /// 初始化日志，仅需要调用1次
        /// </summary>
        void InitLog(string strConfigFile);
        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void WriteInfo(string msg);
        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void WriteInfo(string msg, Exception ex);
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void WriteError(string msg);
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void WriteError(string msg, Exception ex);
        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void WriteWarning(string msg);
        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void WriteWarning(string msg, Exception ex);
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void WriteDebug(string msg);
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void WriteDebug(string msg, Exception ex);
    }
}
