using H264;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace videos
{
    public class Connect
    {
        public static Socket Videoclient;
        public static Socket Audioclient;
        public byte[] dest;
        public int length;
        public byte[] dest2;
        public int length2;
        public void Receive()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    length = Videoclient.Receive(buffer);
                    if (length == 0)
                    {
                        MessageBox.Show("连接断开！请退出");
                        break;
                    }
                    dest = new byte[length];
                    Array.Copy(buffer, dest, length);
                    if (Encoding.UTF8.GetString(dest) == "close")
                    {
                        MessageBox.Show("当前车辆视频通道被占用！请退出");
                        break;
                    }
                    else if (Encoding.UTF8.GetString(dest) == "none")
                    {
                        MessageBox.Show("终端设备不在线！请退出");
                        break;
                    }
                    Tools.video.Enqueue(dest);
                }
                catch
                {
                    MessageBox.Show("连接断开！请退出");
                    break;
                }
            }
        }
        public void ConnectServer()
        {

            try
            {
                Videoclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint point = new IPEndPoint(IPAddress.Parse("139.129.241.169"), int.Parse("8088"));
                Videoclient.Connect(point);
                Thread Thread = new Thread(Receive)
                {
                    IsBackground = true
                };
                Thread.Start();
                Audioclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint point2 = new IPEndPoint(IPAddress.Parse("139.129.241.169"), int.Parse("8091"));
                Audioclient.BeginConnect(point2, asyncResult =>
                {
                    Audioclient.EndConnect(asyncResult);
                    AsynRecive(Audioclient);
                }, null);
                Open(1, "video");
            }
            catch
            {
                MessageBox.Show("连接失败！请退出");
            }
        }

        /// <summary>
        /// 异步接收消息
        /// </summary>
        /// <param name="socket"></param>
        public void AsynRecive(Socket socket)
        {
            byte[] data = new byte[1024];
            try
            {
                socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    try
                    {
                        int length = socket.EndReceive(asyncResult);
                        dest2 = new byte[length];
                        Array.Copy(data, dest2, length);
                        Tools.Audio.Enqueue(dest2);
                        AsynRecive(socket);
                    }
                    catch
                    {

                    }
                }, null);
            }
            catch
            {

            }
        }



        public static void Stop(int order, string type)
        {
            string text = null;
            switch (order)
            {
                case 1:
                    //关闭视频
                    text = "013304521781,0x9102,0";
                    break;
                case 4:
                    //关闭对讲
                    text = "013304521781,0x9102,4";
                    break;
            }
            if (type == "video")
            {
                Videoclient.Send(Encoding.UTF8.GetBytes(text));
            }
            else
            {
                AsyncSend(Audioclient, Encoding.UTF8.GetBytes(text));
            }
        }
        public static void Open(int order, string type)
        {
            string text = null;
            switch (order)
            {
                case 1:
                    //打开视频
                    text = "013304521781,0x9101,1";
                    break;
                case 2:
                    //打开对讲
                    text = "013304521781,0x9101,2";
                    break;
            }
            if (type == "video")
            {
                Videoclient.Send(Encoding.UTF8.GetBytes(text));
            }
            else
            {
                AsyncSend(Audioclient, Encoding.UTF8.GetBytes(text));
            }
        }

        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="p"></param>
        private static void AsyncSend(Socket client, byte[] p)
        {
            if (client == null) return;
            try
            {
                client.BeginSend(p, 0, p.Length, SocketFlags.None, asyncResult =>
                {
                    client.EndSend(asyncResult);
                }, null);
            }
            catch
            {

            }
        }
    }
}
