using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8600
    {
        /// <summary>
        /// 0x8600消息体数据打包
        /// </summary>
        /// <param name="info">圆形区域信息结构</param>
        /// <returns></returns>
        public byte[] Encode(PB8600 info)
        {
            List<byte> list = new List<byte>((info.circleItemsInfo.Count * 33) + 1);

            list.Add(info.settingProperty);

            list.Add((byte)info.circleItemsInfo.Count);

            for (int i = 0; i < list[1]; ++i)
            {
                list.AddRange(CircleAreaItem(info.circleItemsInfo[i]));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 区域项
        /// </summary>
        /// <param name="info">圆形区域项</param>
        /// <returns></returns>
        public byte[] CircleAreaItem(PB8600Item info)
        {
            List<byte> buffer = new List<byte>(33);

            //区域ID
            buffer.AddRange(info.circleId.ToBytes());
            //区域属性
            buffer.AddRange(info.circleProperty.ToBytes());
            //中心点纬度
            buffer.AddRange(info.circleCenterLat.ToBytes());
            //中心点经度
            buffer.AddRange(info.circleCenterLng.ToBytes());
            //区域半径
            buffer.AddRange(info.circleRadius.ToBytes());
            if ((info.circleProperty & 0x01) == 1)
            {
                //起始时间
                buffer.AddRange(info.stime.TimeFormatToBCD());
                //结束时间
                buffer.AddRange(info.etime.TimeFormatToBCD());
            }
            if ((info.circleProperty & 0x02) == 2)
            {
                //最高速度
                buffer.AddRange(info.maxSpeed.ToBytes());

                //超速持续时间
                buffer.Add(info.overSpeedingTime);
            }
            return buffer.ToArray();
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
