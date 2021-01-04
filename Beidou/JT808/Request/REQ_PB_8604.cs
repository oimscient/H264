using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8604
    {

        /// <summary>
        /// 0x8604消息体数据打包
        /// </summary>
        /// <param name="info">区域ID</param>
        /// <returns></returns>
        public byte[] Encode(PB8604 info)
        {
            List<byte> list = new List<byte>((info.polygonItemsInfo.Count << 3) + 21);

            //区域ID
            list.AddRange(info.polygonId.ToBytes());
            //区域属性
            list.AddRange(info.polygonProperty.ToBytes());

            if ((info.polygonProperty & 0x01) == 1)
            {
                //起始时间
                list.AddRange(info.stime.TimeFormatToBCD());
                //结束时间
                list.AddRange(info.etime.TimeFormatToBCD());
            }
            if ((info.polygonProperty & 0x02) == 2)
            {
                //最高速度
                list.AddRange(info.maxSpeed.ToBytes());
                //超速持续时间
                list.Add(info.overSpeedingTime);
            }
            //区域顶点项
            list.Add((byte)info.polygonItemsInfo.Count);
            for (int i = 0; i < info.polygonItemsInfo.Count; ++i)
            {
                list.AddRange(info.polygonItemsInfo[i].Value.ToBytes());
                list.AddRange(info.polygonItemsInfo[i].UInt32Value.ToBytes());
            }
            return list.ToArray();
        }

        /// <summary>
        /// 区域属性,所有参数均为0或1值
        /// </summary>
        /// <returns></returns>
        public UInt16 AreaAttribute(AreaAttribute info)
        {
            UInt16 attr = 0;
            attr |= info.accordingTime;
            attr |= (UInt16)(info.limitSpeed << 1);
            attr |= (UInt16)(info.inareaUpDriver << 2);
            attr |= (UInt16)(info.inareaUpPltf << 3);
            attr |= (UInt16)(info.outareaUpDriver << 4);
            attr |= (UInt16)(info.outareaUpPltf << 5);
            attr |= (UInt16)(info.latflag << 6);
            attr |= (UInt16)(info.lngflag << 7);
            attr |= (UInt16)(info.openflag << 8);
            attr |= (UInt16)(info.communicationflag << 14);
            attr |= (UInt16)(info.samplingflag << 15);
            return attr;
        }
    }
}
