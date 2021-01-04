using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8800
    {
        /// <summary>
        /// 消息指令0x8800数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8800 info)
        {
            byte count = (byte)(info.pIdList.Count);
            int len = 4 + (info.pIdList == null ? 0 : (count << 1));

            byte[] buffer = new byte[len];
            info.MediaId.ToBytes().CopyTo(buffer, 0);

            if (len > 4)
            {
                buffer[4] = count;
                int c = 5;
                for (int i = 0; i < count; ++i)
                {
                    buffer[c] = (byte)(info.pIdList[i] >> 8);
                    buffer[c + 1] = (byte)(info.pIdList[i]);
                    ++c;
                }
            }
            return buffer;
        }
    }
}
