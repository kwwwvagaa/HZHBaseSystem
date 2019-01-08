using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JZ.APIServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;

            JZ.Loger.LogFactory.Loger().InitLog(System.IO.Path.Combine(Application.StartupPath, "log4net.config"));//设置日志配置

            string strConncetion = System.Configuration.ConfigurationManager.AppSettings["JZDbContext"];
            JZ.Server.PublicServer.InitDBConnection(strConncetion);//设置连接字符串

            int intSocketPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["socketPort"]);
            JZ.API.JZSocket.Start(intSocketPort);//启动socket

            int intWcfPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["wcfPort"]);
            JZ.API.JZWCF.Start(intWcfPort);//启动wcf

            int intwebapiPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["webapiPort"]);
            JZ.API.JZWebAPI.Start(intwebapiPort);//启动webapi

            JZ.Server.LoadCache.LoadAllCacheData();//加载缓存

            Application.Run(new Form1());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            JZ.API.JZWCF.Stop();
            JZ.API.JZSocket.Stop();
        }
    }
}
