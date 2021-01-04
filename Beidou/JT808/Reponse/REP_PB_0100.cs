using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    ///<summary>
    /// 终端注册
    ///</summary>
    public class REP_PB_0100
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");
        public REP_PB_0100()
        {  }

        /// <summary>
        /// 终端注册数据包解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0100 Decode(byte[] msgBody)
        {
            return new PB0100()
            {
                ProvincialId = msgBody.ToUInt16(0),
                CityId = msgBody.ToUInt16(2),
                ManufacturerId = msgBody.Copy(4, 5),
                TerminalModel = msgBody.Copy(9, 20),
                TerminalId = msgBody.Copy(29, 7),
                ColorPlates = msgBody[36],
                VehicleIdentification = encoding.GetString(msgBody.Copy(37, msgBody.Length - 37))
            };
        }
    }
}
