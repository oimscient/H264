using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 消息指令0x8203(人工确认报警消息)
    /// </summary>
    public class REQ_PB_8203
    {
        public REQ_PB_8203()
        {
        }

        public byte[] Encode(PB8203 info)
        {
            byte[] buffer = new byte[6];

            buffer[0] = (byte)(info.Serialnumber >> 8);
            buffer[1] = (byte)info.Serialnumber;

            UInt32 alarmType = 0;
            alarmType |= (UInt32)(info.sureUrgentAlarm & 0x01);          //紧急报警
            alarmType |= (UInt32)((info.sureDangerAlarm & 0x01) << 3);      //危险报警
            alarmType |= (UInt32)((info.areaAlarm & 0x01) << 20);           //区域报警
            alarmType |= (UInt32)((info.roadAlarm & 0x01) << 21);           //线路报警
            alarmType |= (UInt32)((info.dtimeAlarm & 0x01) << 22);          //路段行驶时间报警
            alarmType |= (UInt32)((info.illegalStartAlarm & 0x01) << 27);   //非法点火报警
            alarmType |= (UInt32)((info.illegalMoveAlarm & 0x01) << 28);    //非法位移报警

            buffer[2] = (byte)(alarmType >> 24);
            buffer[3] = (byte)(alarmType >> 16);
            buffer[4] = (byte)(alarmType >> 8);
            buffer[5] = (byte)alarmType;

            return buffer;
        }
    }
}
