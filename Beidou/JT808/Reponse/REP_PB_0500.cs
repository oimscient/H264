using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0500(车辆控制应答)
    /// </summary>
    public class REP_PB_0500
    {
        public REP_PB_0500()
        {
        }
        /// <summary>
        /// 车辆控制应答解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0500 Decode(byte[] msgBody)
        {
            PB0500 item = new PB0500()
            {
                SerialNumber = msgBody.ToUInt16(0)
            };

            REP_PB_0200 body0200 = new REP_PB_0200();
            item.PositionInformation = body0200.Decode(msgBody.Copy(2, msgBody.Length - 2));

            return item;
        }
    }
}
