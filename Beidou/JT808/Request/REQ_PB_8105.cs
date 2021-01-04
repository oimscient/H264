using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 消息指令0x8105(终端控制)
    /// </summary>
    public class REQ_PB_8105
    {
        public REQ_PB_8105()
        {
        }

        public byte[] Encode(PB8105 info)
        {
            int len = (info.CmdParameters == null ? 1 : (info.CmdParameters.Length + 1));

            byte[] retBytes = new byte[len];
            retBytes[0] = info.Cmd;

            info.CmdParameters.CopyTo(retBytes, 1);
            return retBytes;
        }
    }
}
