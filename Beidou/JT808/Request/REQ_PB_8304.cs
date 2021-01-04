using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    public class REQ_PB_8304
    {

        /// <summary>
        ///  信息服务数据体打包
        /// </summary>
        /// <param name="info">,Value:消息类型，StringValue:消息内容</param>
        /// <returns></returns>
        public byte[] Encode(ByteString info)
        {
            byte[] txt = Encoding.GetEncoding("GBK").GetBytes(info.StringValue);
            byte[] data = new byte[txt.Length + 3];
            //信息类型
            data[0] = info.Value;

            //信息长度
            UInt16 mlen = (UInt16)txt.Length;
            data[1] = (byte)(mlen >> 8);
            data[2] = (byte)mlen;

            //添加信息内容
            txt.CopyTo(data, 3);
            return data;
        }
    }
}
