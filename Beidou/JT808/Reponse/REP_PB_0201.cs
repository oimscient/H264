using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 位置信息查询应答
    /// </summary>
    public class REP_PB_0201
    {
        public REP_PB_0201()
        {
        }
        public PB0201 Decode(byte[] msgBody)
        {
            PB0201 item = new PB0201()
            {
                SerialNumber = msgBody.ToUInt16(0)
            };

            REP_PB_0200 body0200 = new REP_PB_0200();
            item.PositionInformation = body0200.Decode(msgBody.Copy(2, msgBody.Length - 2));

            return item;
        }
    }
}
