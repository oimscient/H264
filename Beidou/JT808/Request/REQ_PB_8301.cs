using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 事件设置
    /// </summary>
    public class REQ_PB_8301
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");
        public REQ_PB_8301()
        {
        }

        /// <summary>
        /// 事件设置数据体
        /// </summary>
        /// <param name="eventType">事件类型,0:删除所有事件(该命令后不带后继字节);1:更新事件;2:追加事件;3:修改事件;4:删除特定事件(该命令后不带事件内容);</param>
        /// <param name="eventItems">事件项集合(个数不超过255)</param>
        /// <returns></returns>
        public byte[] Encode(PB8301 info)
        {
            byte count = (byte)info.eventItems.Count;
            List<byte> buffer = new List<byte>(count * 12);

            buffer.Add(info.eventType);
            if (info.eventType != 0)
            {
                buffer.Add(count);
                byte[] temp = null;
                for (int i = 0; i < count; ++i)
                {
                    buffer.Add(info.eventItems[i].Value);

                    temp = encoding.GetBytes(info.eventItems[i].StringValue);
                    buffer.Add((byte)temp.Length);

                    buffer.AddRange(temp);
                }
            }
            return buffer.ToArray();
        }
    }
}
