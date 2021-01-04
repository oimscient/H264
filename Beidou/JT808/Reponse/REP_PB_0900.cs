using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0900(数据上行透传)
    /// </summary>
    public class REP_PB_0900
    {
        public REP_PB_0900()
        {
        }
        public PB0900 Decode(byte[] msgBody)
        {
            return new PB0900()
            {
                PassthroughType = msgBody[0],
                PassThroughContent = msgBody.Copy(1, msgBody.Length - 1)
            };
        }
    }
}
