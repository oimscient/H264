using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0303(信息点播/取消)
    /// </summary>
    public class REP_PB_0303
    {
        public REP_PB_0303()
        {
        }
        public PB0303 Decode(byte[] msgBody)
        {
            return new PB0303()
            {
                MessageType = msgBody[0],
                OperationIdentity = msgBody[1]
            };
        }
    }
}
