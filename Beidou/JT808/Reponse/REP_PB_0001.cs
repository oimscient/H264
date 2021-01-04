using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0001(终端通用应答)
    /// </summary>
    public class REP_PB_0001
    {
        public REP_PB_0001()
        {
        }

        public PB0001 Decode(byte[] msgBody)
        {
            return new PB0001()
            {
                serialNumber = msgBody.ToUInt16(0),
                messageId = msgBody.ToUInt16(2),
                result = msgBody[4]
            };
        }
    }
}
