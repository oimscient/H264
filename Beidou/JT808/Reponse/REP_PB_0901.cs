using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0901(数据压缩上报)
    /// </summary>
    public class REP_PB_0901
    {
        public REP_PB_0901()
        {
        }
        /// <summary>
        /// (GZip压缩)压缩的数据
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0901 Decode(byte[] msgBody)
        {
            return new PB0901()
            {
                CompressData = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
