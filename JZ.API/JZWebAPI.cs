using JZ.Models;
using JZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace JZ.API
{
    public class JZWebAPI
    {
        public static HttpSelfHostServer m_serverhost = null;
        public static void Start(int intPort)
        {
            HttpSelfHostConfiguration _config = new HttpSelfHostConfiguration("http://localhost:" + intPort);
            _config.MaxReceivedMessageSize = int.MaxValue;
            _config.MaxBufferSize = int.MaxValue;
            _config.Formatters.Clear();
            _config.Formatters.Add(new JsonMediaTypeFormatter());
            _config.Routes.MapHttpRoute(
             name: "DefaultApinew",
             routeTemplate: "api/{controller}/{action}/{id}",
             defaults: new { id = RouteParameter.Optional }
         );

            //start 
            m_serverhost = new HttpSelfHostServer(_config);
            m_serverhost.OpenAsync().Wait();
        }       
    }

    public class PublicAPIController : ApiController
    {
        [HttpPost]
        public string PostSource([FromBody]string strJson)
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
        [HttpGet]
        public string Test()
        {
            return ExcuteMessage.Error("测试消息");
        }
    }
}
