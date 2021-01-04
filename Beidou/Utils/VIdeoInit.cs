using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Beidou.JT808.Reponse
{
    /// <summary>
    /// 音视频消息体解析
    /// </summary>
    public class VideoInit
    {
        Video item;
        /// <summary>
        /// 音视频消息体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public Video Decode(byte[] msgBody)
        {
            item = new Video();
            try
            {
                int indexOffset = 0;
                item.state = msgBody.ToUInt32(indexOffset += 4);
                item.V_P_X_CC = msgBody[indexOffset];
                item.M_PT = msgBody[indexOffset += 1];
                item.num = msgBody.ToUInt16(indexOffset += 1);
                item.SIM = msgBody.Copy(indexOffset + 2, 6);
                item.ID = msgBody[indexOffset += 8];
                item.type = msgBody[indexOffset += 1];
                item.Time = msgBody.Copy(indexOffset + 2, 8);
                if (BitConvert.ByteToBit(item.type).Substring(0, 4) == "0011")
                {
                    //音频
                    item.length = msgBody.ToUInt16(indexOffset += 9);
                    item.data = msgBody.Copy(indexOffset + 2, item.length);
                }
                else
                {
                    //视频
                    item.Last_I_F = msgBody.ToUInt16(indexOffset += 9);
                    item.Last_F = msgBody.ToUInt16(indexOffset += 2);
                    item.length = msgBody.ToUInt16(indexOffset += 2);
                    item.data = msgBody.Copy(indexOffset + 2, item.length);
                }
                return item;
            }
            catch {
                return item;
            }
        }

    }
}

