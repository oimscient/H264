using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 消息指令0x8003 补传分包请求
    /// </summary>
    public class REQ_PB_8003
    {
        public REQ_PB_8003()
        {
        }

        /// <summary>
        /// 补传包请求数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8003 info)
        {
            byte[] buffer = new byte[((byte)info.IDList.Count << 1) + 3];

            buffer[0] = (byte)(info.Serialnumber >> 8);
            buffer[1] = (byte)info.Serialnumber;

            buffer[2] = (byte)info.IDList.Count;

            int c = 3;
            for (int i = 0; i < buffer[2]; ++i)
            {
                buffer[i + c] = (byte)(info.IDList[i] >> 8);
                buffer[i + (c += 1)] = (byte)info.IDList[i];
            }
            return buffer;
        }
    }
}
