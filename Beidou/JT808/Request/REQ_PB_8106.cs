using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beidou;
namespace Beidou.JT808.Request
{
    /// <summary>
    /// 0x8106消息指令 查询指定终端参数
    /// </summary>
    public class REQ_PB_8106
    {
        public REQ_PB_8106()
        {
        }
        public byte[] Encode(PB8106 info)
        {
            byte[] buffer = new byte[((byte)info.IDList.Count << 2) + 1];
            buffer[0] = (byte)info.IDList.Count;
            byte[] temp = null;
            int index = 1;
            for (int i = 0; i < info.IDList.Count; ++i)
            {
                temp = info.IDList[i].ToBytes();
                temp.CopyTo(buffer, index);
                index += 4;
            }
            return buffer;
        }
    }
}
