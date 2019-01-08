using JZ.Models;
using JZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JZ.API
{
    public class JZSocket
    {
        static SocketHelper.TcpServer server;

        public static void Start(int intPort)
        {
            SocketHelper.pushSockets = new SocketHelper.PushSockets(Rec);
            //防止二次开启引发异常
            if (server == null)
            {
                server = new SocketHelper.TcpServer();
                server.InitSocket(IPAddress.Any, intPort);               
            }
            server.Start();
        }

        public static void Stop()
        {
            server.Stop();
        }

        public static void Send(IPEndPoint ip, string data)
        {
            server.SendToClient(ip, data);
        }
        public static void Send(IPEndPoint ip, byte[] data)
        {
            server.SendToClient(ip, data);
        }

        public static void SendAll(string data)
        {
            server.SendToAll(data);
        }
        public static void SendAll(byte[] data)
        {
            server.SendToAll(data);
        }
        private static void Rec(SocketHelper.Sockets sks)
        {
            if (sks.ex != null)
            {
                //string.Format("客户端出现异常:{0}.!", sks.ex.Message);    
            }
            else
            {
                if (sks.NewClientFlag)
                {
                    //string.Format("新客户端:{0}连接成功.!", sks.Ip)
                }
                else
                {
                    byte[] buffer = new byte[sks.Offset];
                    Array.Copy(sks.RecBuffer, buffer, sks.Offset);
                    string str = string.Empty;
                    if (sks.Offset == 0)
                    {
                        str = "客户端下线";
                    }
                    else
                    {
                        str = Encoding.UTF8.GetString(buffer);
                        if (string.IsNullOrEmpty(str))
                        {
                            server.SendToClient(sks.Ip, ExcuteMessage.Error("传入参数为空！"));
                            return;
                        }

                        PostSourceEntity entity = str.ToJsonObject<PostSourceEntity>();
                        str = JZ.Server.PublicServer.CallFunction(entity);
                        server.SendToClient(sks.Ip, str);
                    }
                }
            }
        }

    }
}
