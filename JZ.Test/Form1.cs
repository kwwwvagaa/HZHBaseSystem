using JZ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JZ.Tools;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace JZ.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 创建提交数据
        private string CreateAddModel()
        {
            JZ.Models.Test test = new Models.Test() { Value = Guid.NewGuid().ToString() };
            PostSourceEntity model = new PostSourceEntity()
            {
                NameSpace = "JZ.Server",
                ClassName = "TestServer",
                FunctionName = "Insert",
                Parameters = test.ToJson()
            };
            return model.ToJson();
        }
        private string CreateSearchModel()
        {
            PostSourceEntity model = new PostSourceEntity()
            {
                NameSpace = "JZ.Server",
                ClassName = "TestServer",
                FunctionName = "Search",
                Parameters = "1=1"
            };
            return model.ToJson();
        }

        private string CreateDeleteModel()
        {
            PostSourceEntity model = new PostSourceEntity()
            {
                NameSpace = "JZ.Server",
                ClassName = "TestServer",
                FunctionName = "DeleteByWhere",
                Parameters = "1=1"
            };
            return model.ToJson();
        }

        private string CreateEditModel(JZ.Models.Test test)
        {
            PostSourceEntity model = new PostSourceEntity()
            {
                NameSpace = "JZ.Server",
                ClassName = "TestServer",
                FunctionName = "Update",
                Parameters = test.ToJson()
            };
            return model.ToJson();
        }
        #endregion

        #region wcf
        private void button1_Click(object sender, EventArgs e)
        {
            JZWCF.JZWCFClient wcf = new JZWCF.JZWCFClient();
            string str = wcf.PostSource(CreateAddModel());
            ExcuteResultMsg msg = str.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                MessageBox.Show("成功\n\r" + str);
            }
            else
            {
                MessageBox.Show("失败：" + str);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            JZWCF.JZWCFClient wcf = new JZWCF.JZWCFClient();
            string str = wcf.PostSource(CreateSearchModel());
            ExcuteResultMsg msg = str.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                MessageBox.Show("成功\n\r" + str);
            }
            else
            {
                MessageBox.Show("失败：" + str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            JZWCF.JZWCFClient wcf = new JZWCF.JZWCFClient();
            string str = wcf.PostSource(CreateDeleteModel());
            ExcuteResultMsg msg = str.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                MessageBox.Show("成功\n\r" + str);
            }
            else
            {
                MessageBox.Show("失败：" + str);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            JZWCF.JZWCFClient wcf = new JZWCF.JZWCFClient();
            string str = wcf.PostSource(CreateSearchModel());
            ExcuteResultMsg msg = str.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                var lst = msg.Result.ToJsonObject<List<JZ.Models.Test>>();
                if (lst != null && lst.Count > 0)
                {

                    lst[0].Value = Guid.NewGuid().ToString();
                    str = wcf.PostSource(CreateEditModel(lst[0]));
                    msg = str.ToJsonObject<ExcuteResultMsg>();
                    if (msg.Code == ResultCode.Success)
                    {
                        MessageBox.Show("修改成功\n\r" + str);
                    }
                    else
                    {
                        MessageBox.Show("修改失败：" + str);
                    }
                }
                else
                {
                    MessageBox.Show("没有查询到数据");
                    return;
                }
            }
            else
            {
                MessageBox.Show("查询数据失败：" + str);
            }
        }

        #endregion

        #region webapi
        private HttpClient CreateHttpClient(string apiUrl)
        {
            if (!apiUrl.EndsWith("/"))
                apiUrl += "/";
            var client = new HttpClient();
            TimeSpan s = new TimeSpan(0, 5, 0);
            client.Timeout = s;
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("UserAgent", HttpContext.Current != null ? HttpContext.Current.Request.UserAgent : string.Empty);
            client.DefaultRequestHeaders.Add("UserAddr", HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : string.Empty);
            //  client.DefaultRequestHeaders.Add("UserToken", LoginHelper.UserToken);
            return client;
        }

        private string ExecuteHttp(string strUrl, string postData)
        {
            HttpClient client = CreateHttpClient(strUrl);

            HttpResponseMessage response = client.PostAsJsonAsync(strUrl, postData).Result;

            //无论错误还是成功，都从流里面获取返回的文本
            return response.Content.ReadAsStringAsync().Result;

        }
        private void button8_Click(object sender, EventArgs e)
        {
            string str = ExecuteHttp(textBox2.Text, CreateAddModel());
            string str1 = str.ToJsonObject<string>();
            ExcuteResultMsg msg = str1.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                MessageBox.Show("成功\n\r" + str);
            }
            else
            {
                MessageBox.Show("失败：" + str);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str = ExecuteHttp(textBox2.Text, CreateSearchModel());
            string str1 = str.ToJsonObject<string>();
            ExcuteResultMsg msg = str1.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                MessageBox.Show("成功\n\r" + str);
            }
            else
            {
                MessageBox.Show("失败：" + str);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = ExecuteHttp(textBox2.Text, CreateDeleteModel());
            string str1 = str.ToJsonObject<string>();
            ExcuteResultMsg msg = str1.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                MessageBox.Show("成功\n\r" + str);
            }
            else
            {
                MessageBox.Show("失败：" + str);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = ExecuteHttp(textBox2.Text, CreateSearchModel());
            string str1 = str.ToJsonObject<string>();
            ExcuteResultMsg msg = str1.ToJsonObject<ExcuteResultMsg>();
            if (msg.Code == ResultCode.Success)
            {
                var lst = msg.Result.ToJsonObject<List<JZ.Models.Test>>();
                if (lst != null && lst.Count > 0)
                {

                    lst[0].Value = Guid.NewGuid().ToString();
                    str = ExecuteHttp(textBox2.Text, CreateEditModel(lst[0]));
                    str1 = str.ToJsonObject<string>();
                    msg = str1.ToJsonObject<ExcuteResultMsg>();
                    if (msg.Code == ResultCode.Success)
                    {
                        MessageBox.Show("修改成功\n\r" + str);
                    }
                    else
                    {
                        MessageBox.Show("修改失败：" + str);
                    }
                }
                else
                {
                    MessageBox.Show("没有查询到数据");
                    return;
                }
            }
            else
            {
                MessageBox.Show("查询数据失败：" + str);
            }
        }
        #endregion



        #region socket
        SocketHelper.TcpClients client;
        private void button13_Click(object sender, EventArgs e)
        {
            SocketHelper.pushSockets = new SocketHelper.PushSockets(Rec);//注册推送器
            client = new SocketHelper.TcpClients();
            string[] ips = textBox3.Text.Split(':');
            string ip = ips[0];
            string port = ips[1];
            client.InitSocket(ip, int.Parse(port));
            client.Start();
        }
        bool blnUpdate = false;
        /// <summary>
        /// 处理推送过来的消息
        /// </summary>
        /// <param name="rec"></param>
        private void Rec(SocketHelper.Sockets sks)
        {

            if (sks.ex != null)
            {
                //在这里判断ErrorCode  可以自由扩展
                switch (sks.ErrorCode)
                {
                    case SocketHelper.Sockets.ErrorCodes.objectNull:
                        break;
                    case SocketHelper.Sockets.ErrorCodes.ConnectError:
                        break;
                    case SocketHelper.Sockets.ErrorCodes.ConnectSuccess:
                        break;
                    case SocketHelper.Sockets.ErrorCodes.TrySendData:
                        break;
                    default:
                        break;
                }
                MessageBox.Show(sks.ex.ToString());
            }
            else
            {
                byte[] buffer = new byte[sks.Offset];
                Array.Copy(sks.RecBuffer, buffer, sks.Offset);
                string str = Encoding.UTF8.GetString(buffer);
                if (str == "ServerOff")
                {
                    MessageBox.Show("服务端主动关闭");
                }
                else
                {
                    ExcuteResultMsg msg = str.ToJsonObject<ExcuteResultMsg>();
                    if (blnUpdate)
                    {
                        blnUpdate = false;
                        var lst = msg.Result.ToJsonObject<List<JZ.Models.Test>>();
                        if (lst != null && lst.Count > 0)
                        {

                            lst[0].Value = Guid.NewGuid().ToString();
                            client.SendData(CreateEditModel(lst[0]));                           
                        }
                        else
                        {
                            MessageBox.Show("没有查询到数据");
                            return;
                        }
                    }
                    else
                    {
                        if (msg.Code == ResultCode.Success)
                        {
                            MessageBox.Show("成功\n\r" + str);
                        }
                        else
                        {
                            MessageBox.Show("失败：" + str);
                        }
                    }


                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            blnUpdate = false;
            client.SendData(CreateAddModel());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            blnUpdate = false;
            client.SendData(CreateSearchModel());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            blnUpdate = false;
            client.SendData(CreateDeleteModel());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            blnUpdate = true;
            client.SendData(CreateSearchModel());
        }
        #endregion


    }
}
