using Beidou.Structures;
using Beidou.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_9003
    {
        private static Socket sockets;
        private static ValueTuple<string, DateTime, bool, Thread> val;
        private static byte[] body_9003=null;
        private static byte[] buffer;
        public static void R9003(IPacketProvider pConvert, string[] data)
        {
            for (int i = 0; i < SControl.CarDict.Count; i++)
            {
                sockets = SControl.CarDict.ElementAt(i).Key;
                val = SControl.CarDict[sockets];
                if (val.Item1 == data[0])
                {
                    buffer = pConvert.Encode(new PacketFrom()
                    {
                        msgBody = body_9003,
                        msgId = JT808Cmd.REQ_9003,
                        msgSerialnumber = 0,
                        pEncryptFlag = 0,
                        pSerialnumber = 1,
                        pSubFlag = 0,
                        pTotal = 1,
                        simNumber = Extension.ToBCD(data[0]),
                    });
                    sockets.Send(buffer);
                    break;
                }
            }
        }
    }
}
