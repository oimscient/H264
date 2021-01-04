using Beidou.JT808;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beidou.Utils
{
   public  class RTP
    {
        public static byte[] Encode(RTPBody info)
        {
            List<byte> buffer = new List<byte>(976);
            for (int i = 0; i < info.state.Length;i++)
            {
                buffer.Add(info.state[i]);
            }
            buffer.Add(info.Vpxc);
            buffer.Add(info.MPT);
            buffer.Add((byte)(info.index >> 8));
            buffer.Add((byte)info.index);
            foreach (var i in info.hSimNumber)
            {
                buffer.Add(i);
            }
            buffer.Add(info.chanle);
            buffer.Add(info.type);
            foreach (var i in info.time)
            {
                buffer.Add(i);
            }
            buffer.Add((byte)(info.length >> 8));
            buffer.Add((byte)info.length);
            foreach (var i in info.data)
            {
                buffer.Add(i);
            }
            return buffer.ToArray();
        }
    }
}
