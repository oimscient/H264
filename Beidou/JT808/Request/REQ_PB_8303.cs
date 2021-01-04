using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8303
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");
        /// <summary>
        /// 信息点播数据体打包
        /// </summary>
        /// <param name="sType">设置类型,0：删除全部信息项,1：更新信息项,2：追加信息项,3：修改信息项</param>
        /// <param name="itemList">信息项列表</param>
        /// <returns></returns>
        public byte[] Encode(PB8303 info)
        {
            byte count = (byte)info.MessageList.Count;
            List<byte> buffer = new List<byte>(count * 12 + 1);

            //添加类型
            buffer.Add(info.SettingType);
            if (info.SettingType != 0)
            {
                //信息项数量
                buffer.Add(count);

                byte[] temp = null;

                //添加信息项列表
                for (int i = 0; i < count; i++)
                {
                    buffer.Add(info.MessageList[i].Value);

                    temp = encoding.GetBytes(info.MessageList[i].StringValue);
                    UInt16 len = (UInt16)temp.Length;
                    buffer.Add((byte)(len >> 8));
                    buffer.Add((byte)len);

                    buffer.AddRange(temp);
                }
            }
            return buffer.ToArray();
        }
    }
}
