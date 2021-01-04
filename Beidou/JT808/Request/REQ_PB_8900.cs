using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8900
    {

        /// <summary>
        /// 数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8900 info)
        {
            byte[] data = new byte[info.content.Length + 1];
            data[0] = info.mType;
            info.content.CopyTo(data, 1);
            return data;
        }
    }
}
