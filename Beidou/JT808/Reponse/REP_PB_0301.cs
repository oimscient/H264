using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 事件报告
    /// </summary>
    public class REP_PB_0301
    {
        public REP_PB_0301()
        {
        }
        public PB0301 Decode(byte[] msgBody)
        {
            return new PB0301()
            {
                EventId = msgBody[0]
            };
        }
    }
}
