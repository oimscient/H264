using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
   public class REQ_PB_8606
    {
        /// <summary>
        /// 消息指令0x8606数据体打包
        /// </summary>
        /// <param name="RouteInfo">路线信息结构</param>
        /// <returns></returns>
        public byte[] Encode(PB8606 info)
        {
            List<byte> buffer = new List<byte>(info.Items.Count * 25 + 18);

            //路线ID
            buffer.AddRange(info.routeId.ToBytes());
            //路线属性
            buffer.AddRange(info.routeProperty.ToBytes());
            if ((info.routeProperty & 0x01) == 1)
            {
                //起始时间
                buffer.AddRange(info.stime.TimeFormatToBCD());
                //结束时间
                buffer.AddRange(info.stime.TimeFormatToBCD());
            }
            //拐点个数
            buffer.AddRange(((UInt16)info.Items.Count).ToBytes());
            //拐点项
            for (int i = 0; i < info.Items.Count; ++i)
            {
                buffer.AddRange(InflectionPointItem(info.Items[i]));
            }
            return buffer.ToArray();
        }

        /// <summary>
        /// 拐点项
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] InflectionPointItem(PB8606Item info)
        {
            List<byte> item = new List<byte>(25);

            //拐点ID
            item.AddRange(info.inflectionPointId.ToBytes());
            //路段ID
            item.AddRange(info.routeSectionId.ToBytes());
            //拐点纬度
            item.AddRange(info.inflectionPointLat.ToBytes());
            //拐点经度
            item.AddRange(info.inflectionPointLng.ToBytes());
            //路段宽度
            item.Add(info.routeSectionWidth);
            //路段属性
            item.Add(info.routeSectionProperty);

            if ((info.routeSectionProperty & 0x01) == 1)
            {
                //路段行驶过长阀值
                item.AddRange(info.routeSectionLongTime.ToBytes());
                //路段行驶过短阀值
                item.AddRange(info.routeSectionShortTime.ToBytes());
            }

            if ((info.routeSectionProperty & 0x02) == 2)
            {
                //路段最高速度
                item.AddRange(info.routeSectionMaxSpeed.ToBytes());
                //超速持续时间
                item.Add(info.routeSectionSpeedingTime);
            }
            return item.ToArray();
        }

        /// <summary>
        /// 路段属性(参数均为0或1的值)
        /// </summary>
        /// <returns></returns>
        public byte RoadAttribute(RoadSectionAttribute info)
        {
            byte b = 0;
            b |= (byte)(info.drivertime & 0x01);
            b |= (byte)((info.limitspeed & 0x01) << 1);
            b |= (byte)((info.latflag & 0x01) << 2);
            b |= (byte)((info.lngflag & 0x01) << 3);
            return b;
        }

        /// <summary>
        /// 路线属性
        /// </summary>
        /// <returns></returns>
        public UInt16 RouteAttribute(RoadAttribute info)
        {
            UInt16 attr = 0;
            attr |= (UInt16)(info.accordingtime & 0x01);
            attr |= (UInt16)((info.inRouteUpDriver & 0x01) << 2);
            attr |= (UInt16)((info.inRouteUpPltf & 0x01) << 3);
            attr |= (UInt16)((info.outRouteUpDriver & 0x01) << 4);
            attr |= (UInt16)((info.outRouteUpPltf & 0x01) << 5);
            return attr;
        }
    }
}
