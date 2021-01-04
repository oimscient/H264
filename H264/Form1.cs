using Beidou;
using Beidou.JT808;
using Beidou.Utils;
using FVD.Common;
using System;
using System.Windows.Forms;
using videos;

namespace H264
{
    public partial class Form1 : Form
    {
        public static Form1 Form;
        H264Dec H264Dec;
        Connect Receive;

        public Form1()
        {
            Form = this;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            H264Dec = new H264Dec();
            H264Dec.Init();
            AudioParse.NaudioInit();
            Receive = new Connect();
            Receive.ConnectServer();
            H264Dec.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form.button3.Text == "关闭对讲")
            {
                Connect.Stop(4, "audio");
                AudioParse.AudioStop();
                Form.button3.Text = "打开对讲";
            }
            else
            {
                Connect.Open(2, "audio");
                AudioParse.AudioPaly();
                Form.button3.Text = "关闭对讲";
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connect.Stop(1, "video");
            Connect.Stop(4, "audio");
        }
    }
}
