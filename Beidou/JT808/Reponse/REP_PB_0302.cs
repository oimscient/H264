using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0203(提问应答)
    /// </summary>
    public class REP_PB_0302
    {
        public REP_PB_0302()
        {
        }
        public PB0302 Decode(byte[] msgBody)
        {
            return new PB0302()
            {
                SerialNumber = msgBody.ToUInt16(0),
                AnswerId = msgBody[2]
            };
        }
    }
}
