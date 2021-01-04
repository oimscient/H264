using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 0x0107消息指令解析(查询终端属性应答)
    /// </summary>
    public class REP_PB_0107
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");

        public REP_PB_0107()
        {
        }
        /// <summary>
        /// MSGBody模块属性
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] MSGBodyAttribute(byte data)
        {
            return new byte[]
            { 
             (byte)(data & 0x0001),
             (byte)((data & 0x0002)>>1),
             (byte)((data & 0x0004)>>2),
             (byte)((data & 0x0008)>>3)
            };
        }

        /// <summary>
        /// 查询终端属性应答解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0107 Decode(byte[] msgBody)
        {
            int indexOffset = 0;
            byte len = 0;

            PB0107 item = new PB0107()
            {
                TerminalType = msgBody.ToUInt16(indexOffset),
                ManufacturerId = msgBody.Copy(indexOffset += 2, 5),
                TerminalModel = msgBody.Copy(indexOffset += 5, 20),
                TerminalId = msgBody.Copy(indexOffset += 20, 7),
                TerminalSIM = msgBody.Copy(indexOffset += 7, 10)
            };

            len = msgBody[indexOffset];
            item.TerminalHardwareVersionNumber = encoding.GetString(msgBody.Copy(indexOffset += 1, len));

            len = msgBody[indexOffset += len];
            item.TerminalFirmwareVersionNumber = encoding.GetString(msgBody.Copy(indexOffset += 1, len));

            item.GnssModuleProperties = msgBody[indexOffset += len];

            item.CommunicationModuleProperties = msgBody[indexOffset += 1];

            return item;
        }
    }
}
