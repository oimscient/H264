using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 消息指令0x0801(多媒体数据上传)
    /// </summary>
    public class REP_PB_0801
    {
        public REP_PB_0801()
        {
        }
        /// <summary>
        /// 多媒体数据上传
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0801 Decode(byte[] msgBody)
        {
            int indexOffset = 0;
            REP_PB_0200 body0200 = new REP_PB_0200();

            PB0801 item = new PB0801()
            {
                MultimediaDataId = msgBody.ToUInt32(0),
                MultmediaType = msgBody[indexOffset += 4],
                MultimediaFormat = msgBody[indexOffset += 1],
                EventItemCoding = msgBody[indexOffset += 1],
                ChannelId = msgBody[indexOffset += 1]
            };

            item.PositionInformation = body0200.Decode(msgBody.Copy(indexOffset += 1, 28));

            item.MultimediaPackage = msgBody.Copy(indexOffset += 28, msgBody.Length - indexOffset);

            return item;
        }
    }
}
