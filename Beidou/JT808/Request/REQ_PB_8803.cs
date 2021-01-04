using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8803
    {
        /// <summary>
        /// 存储多媒体数据上传命令
        /// </summary>
        /// <param name="info">多媒体数据上传结构</param>
        /// <returns></returns>
        public byte[] Encode(PB8803 info)
        {
            //计算数组长度
            byte[] data = new byte[16];

            data[0] = info.mType;
            data[1] = info.channelId;
            data[2] = info.eventCode;
            //添加时间
            info.stime.TimeFormatToBCD().CopyTo(data, 3);
            info.etime.TimeFormatToBCD().CopyTo(data, 9);
            //删除或保留
            data[15] = info.delFlag;
            return data;
        }
    }
}
