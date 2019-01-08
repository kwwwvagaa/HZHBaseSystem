using JZ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Loger
{
    public class LogFactory
    {
        /// <summary>
        /// 功能描述:获取一个日志对象
        /// 作　　者:beck.huang
        /// 创建日期:2018-11-22 15:20:25
        /// 任务编号:好餐谋后台管理系统
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>返回值</returns>
        public static ILoger Loger(LogType type = LogType.FILE)
        {
            ILoger loger = null;
            switch (type)
            {
                case LogType.FILE:
                    loger = LogForFile.Instance;
                    break;
                case LogType.DB:
                    loger = LogForDB.Instance;
                    break;
                default:
                    loger = LogForFile.Instance;
                    break;
            }
            return loger;
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 文件日志
            /// </summary>
            FILE = 1,
            /// <summary>
            /// 数据库日志
            /// </summary>
            DB = 2
        }
    }
}
