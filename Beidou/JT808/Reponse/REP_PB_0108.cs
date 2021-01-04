using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 终端升级数据返回结果
    /// </summary>
    public class REP_PB_0108
    {
        public REP_PB_0108()
        {
        }
        /// <summary>
        /// 终端升级结果回应数据解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0108 Decode(byte[] msgBody)
        {
            return new PB0108()
            {
                UpdateType = msgBody[0],
                UpdateResult = msgBody[1]
            };
        }
    }
}
