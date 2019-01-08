using JZ.Models;
using JZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace JZ.API
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“JZWCF”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 JZWCF.svc 或 JZWCF.svc.cs，然后开始调试。
    public class JZWCF : IJZWCF
    {
        public string PostSource(string strJson)
        {
            try
            {
                if (string.IsNullOrEmpty(strJson))
                    return "传入参数为空！";

                PostSourceEntity entity = strJson.ToJsonObject<PostSourceEntity>();
                string str = string.Empty;

                str = JZ.Server.PublicServer.CallFunction(entity);

                return str;
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }
        private static ServiceHost m_host = null;
        public static void Start(int intPort)
        {
            if (m_host == null)
            {
                m_host = new ServiceHost(typeof(JZWCF));

                //绑定
                System.ServiceModel.Channels.Binding httpBinding = new BasicHttpBinding();
                //终结点
                m_host.AddServiceEndpoint(typeof(IJZWCF), httpBinding, "http://localhost:" + intPort + "/");
                if (m_host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
                {
                    //行为
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;

                    //元数据地址
                    behavior.HttpGetUrl = new Uri("http://localhost:" + intPort + "/JZWCF");
                    m_host.Description.Behaviors.Add(behavior);

                    //启动
                    m_host.Open();
                }
            }
        }

        public static void Stop()
        {
            if (m_host != null)
            {
                m_host.Close();
            }
        }
    }
}
