using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 终端注册返回应答
    /// </summary>
    public class REQ_PB_8100
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");
        public REQ_PB_8100()
        {
        }

        /// <summary>
        /// 数据打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8100 info)
        {
            byte[] codes = null;
            int len = 3;
            if (info.Result == 0)
            {
                codes = encoding.GetBytes(info.AuthenticationCode);
                len = 3 + codes.Length;
            }
            byte[] data = new byte[len];

            data[0] = (byte)(info.Serialnumber >> 8);
            data[1] = (byte)info.Serialnumber;
            data[2] = info.Result;
            if (info.Result == 0)
            {
                codes.CopyTo(data, 3);
            }
            return data;
        }
    }
}
