using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 消息指令0x8103(设置终端参数)
    /// </summary>
    public class REQ_PB_8103
    {
        public REQ_PB_8103()
        {
        }

        public byte[] Encode(PB8103 info)
        {
            List<byte> buffer = new List<byte>(info.Parameters.Count * 10);

            byte count = (byte)info.Parameters.Count;
            buffer.Add(count);

            for (int i = 0; i < count; ++i)
            {
                //参数ID
                buffer.AddRange(info.Parameters[i].Value.ToBytes());
                //参数长度
                buffer.Add((byte)info.Parameters[i].BytesValue.Length);
                //参数值
                buffer.AddRange(info.Parameters[i].BytesValue);
            }

            return buffer.ToArray();
        }
    }
}
