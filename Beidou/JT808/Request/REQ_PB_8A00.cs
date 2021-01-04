using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8A00
    {
        /// <summary>
        /// 平台公钥数据体打包,Value,密钥key,BytesValue:rsa密钥key
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(UInt32Bytes info)
        {
            byte[] data = new byte[4 + info.BytesValue.Length];
            data[0] = (byte)(info.Value >> 24);
            data[1] = (byte)(info.Value >> 16);
            data[2] = (byte)(info.Value >> 8);
            data[3] = (byte)info.Value;

            info.BytesValue.CopyTo(data, 4);
            return data;
        }
    }
}
