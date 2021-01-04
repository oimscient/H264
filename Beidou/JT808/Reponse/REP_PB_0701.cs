using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0701(电子运单上报)
    /// </summary>
    public class REP_PB_0701
    {
        public REP_PB_0701()
        {
        }
        public PB0701 Decode(byte[] msgBody)
        {
            return new PB0701()
            {
                ElectronicWaybill = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
