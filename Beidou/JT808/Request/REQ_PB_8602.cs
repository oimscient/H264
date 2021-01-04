using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8602
    {

        /// <summary>
        /// 0x8600消息体数据打包
        /// </summary>
        /// <param name="info">矩形区域项</param>
        /// <returns></returns>
        public byte[] Encode(PB8602 info)
        {
            List<byte> buffer = new List<byte>((info.rectangleItemsInfo.Count * 33) + 1);

            buffer.Add(info.settingProperty);

            buffer.Add((byte)info.rectangleItemsInfo.Count);

            for (int i = 0; i < buffer[1]; ++i)
            {
                buffer.AddRange(RectangleAreaItem(info.rectangleItemsInfo[i]));
            }
            return buffer.ToArray();
        }

        /// <summary>
        /// 区域项
        /// </summary>
        /// <param name="info">矩形区域项</param>
        /// <returns></returns>
        public byte[] RectangleAreaItem(PB8602Item info)
        {
            List<byte> buffer = new List<byte>(33);

            //区域ID
            buffer.AddRange(info.rectangleId.ToBytes());
            //区域属性
            buffer.AddRange(info.rectangleProperty.ToBytes());
            //左上纬度
            buffer.AddRange(info.rectangleLeftTopLat.ToBytes());
            //左上经度
            buffer.AddRange(info.rectangleLeftTopLng.ToBytes());
            //右下纬度
            buffer.AddRange(info.rectangleRightDownLat.ToBytes());
            //右下经度
            buffer.AddRange(info.rectangleRightDownLng.ToBytes());
            if ((info.rectangleProperty & 0x01) == 1)
            {
                //起始时间
                buffer.AddRange(info.stime.TimeFormatToBCD());
                //结束时间
                buffer.AddRange(info.etime.TimeFormatToBCD());
            }
            if ((info.rectangleProperty & 0x02) == 2)
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
