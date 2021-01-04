using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8700
    {
        /// <summary>
        /// 车辆行驶记录采集数据体打包
        /// </summary>
        /// <param name="cmd">命令字</param>
        /// <param name="pdata">请求数据包</param>
        /// <returns></returns>
        public byte[] Encode(PB8700 info)
        {
            byte[] buffer = new byte[17];
            buffer[0] = info.Cmd;
            CmdBody(info.item).CopyTo(buffer, 1);
            return buffer;
        }

        /// <summary>
        /// 请求数据包
        /// </summary>
        /// <param name="startTime">起始时间,格式如：2014120116141800</param>
        /// <param name="endTime">结束时间,格式如：2014120116141800</param>
        /// <param name="maxCount">数据块的最大个数</param>
        /// <returns></returns>
        private byte[] CmdBody(PB8700Item info)
        {
            byte[] buffer = new byte[16];

            info.StartTime.ToBCD().CopyTo(buffer, 0);

            info.EndTime.ToBCD().CopyTo(buffer, 7);

            info.MaxCount.ToBytes().CopyTo(buffer, 14);

            return buffer;
        }
    }
}
