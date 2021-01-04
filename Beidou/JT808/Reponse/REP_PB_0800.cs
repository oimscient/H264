using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0800(多媒体事件信息上传)
    /// </summary>
    public class REP_PB_0800
    {
        public REP_PB_0800()
        {
        }
        public PB0800 Decode(byte[] msgBody)
        {
            return new PB0800()
            {
                MultimediaDataId = msgBody.ToUInt32(0),
                MultmediaType = msgBody[4],
                MultimediaFormat = msgBody[5],
                EventItemCoding = msgBody[6],
                ChannelId = msgBody[7]
            };
        }
    }
}
