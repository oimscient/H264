using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Beidou
{
    using Structures;
    public class PacketProvider:IPacketProvider
    {
        public PacketProvider()
        { }
        public static PacketProvider CreateProvider()
        {
            return new PacketProvider();
        }
        /// <summary>
        /// 序列化结构信息为字节序
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public byte[] Encode(PacketFrom item)
        {
            return item.Encoding();
        }
        /// <summary>
        /// 反序列化字节序为结构信息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public PacketMessage Decode(byte[] buffer,int offset,int count)
        {
            //建立数据包结构体对象并赋值
            PacketMessage packetInfo = new PacketMessage();
            if (count <= 14 || count >= 1040)
            {
                /*  throw new Exception(string.Format("数据包有效长度为>14 且 <=1040,当前字节长度...{0}", buffer.Length));*/
                packetInfo = null;
                return packetInfo;

            }
            byte checknum = 0;
            //转义还原
            byte[] rawData = ReEscape(buffer, offset, count, ref checknum);
/*            //验证校验码
            if (checknum != rawData[rawData.Length - 1])
            {
                packetInfo = null;
                throw new Exception(string.Format("校验码不符合,值:dst[{0}]/src[{1}]", checknum, rawData[rawData.Length - 1]));
              
            } */          
            int pos = 0;
            //解析消息头
            packetInfo.pmPacketHead = DecodeRawHead(rawData, ref pos);
            if (packetInfo.pmPacketHead.phPacketHeadAttribute.paMessageBodyLength > 0)
            {
                int len = rawData.Length - pos - 1;
/*                //数据长度不符合
                if (packetInfo.pmPacketHead.phPacketHeadAttribute.paMessageBodyLength != len)
                {
                    *//*throw new Exception(string.Format("原始数据体长度和目标长度不符:src[{0}]/dst[{1}]",
                        packetInfo.pmPacketHead.phPacketHeadAttribute.paMessageBodyLength, len));*//*
                    packetInfo = null;
                    return packetInfo;
                }*/
                if (len==-1) {
                    packetInfo = null;
                    return packetInfo;
                }
                //消息体
                try
                {
                    packetInfo.pmMessageBody = new byte[len];
                    rawData.CopyTo(pos, packetInfo.pmMessageBody, 0, len);
                    //校验码
                    packetInfo.pmCheckcode = checknum;
                } catch {
                    packetInfo = null;
                    return packetInfo;
                }
            }
            return packetInfo;
        }
        /// <summary>
        /// 根据偏移量开始解析数据包
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public PacketMessage Decode(byte[] buffer)
        {
            return Decode(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 反序列化字节序为消息头结构信息，该消息必需为原始数据包(不需要反转义和校验的数据包)
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos">解析的位置,初始化为1开始</param>
        /// <returns></returns>
        public PacketHead DecodeRawHead(byte[] buffer, ref int pos)
        {
            PacketHead headInfo = new PacketHead();
            //获取消息ID
            headInfo.phMessageId = buffer.ToUInt16(pos);
            //获取消息体属性
            UInt16 attr = buffer.ToUInt16(pos += 2);
            headInfo.phPacketHeadAttribute = PakcetAttributeDecode(attr);
            //获取电话号码
            buffer.CopyTo(pos += 2, headInfo.hSimNumber, 0, 6);
            //获取消息流水号
            headInfo.phSerialnumber = buffer.ToUInt16(pos += 6);
            //消息包封装项
            if (headInfo.phPacketHeadAttribute.paSubFlag == 1)
            {
                headInfo.phPackeHeadTag = new PacketTag()
                {
                    ptTotal = buffer.ToUInt16(pos += 2),
                    ptSerialnumber = buffer.ToUInt16(pos += 2)
                };
            }
            pos += 2;
            return headInfo;
        }
        /// <summary>
        /// 消息属性解码
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public PacketAttribute PakcetAttributeDecode(UInt16 attr)
        {
            return new PacketAttribute()
            {
                paMessageBodyLength = (UInt16)(attr & 0x03FF),
                paEncryptFlag = (byte)((attr >> 10) & 0x01),
                paSubFlag = (byte)((attr >> 13) & 0x01)
            };
        }
        /// <summary>
        /// 消息还原，并且计算校验码,过滤标志位
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="checkcode"></param>
        /// <returns></returns>
        private unsafe byte[] ReEscape(byte[] buffer, int offset, int count, ref byte checkcode)
        {
            int sindex = offset + 1, slen = count - 1, i = 0;
            byte cvalue, cnvalue;
            fixed (byte* src = buffer, dst = new byte[count - 2])
            {
                while (sindex < slen)
                {
                    cvalue = *(src + sindex);
                    cnvalue = *(src + sindex + 1);
                    if (cvalue == 0x7d && cnvalue == 0x02)
                    {
                        *(dst + i) = 0x7e;
                        ++sindex;
                        goto pos;
                    }
                    else if (cvalue == 0x7d && cnvalue == 0x01)
                    {
                        *(dst + i) = 0x7d;
                        ++sindex;
                    }
                    else
                    {
                        *(dst + i) = cvalue;
                    }
                    pos:
                    //计算校验码
                    if (i == 0)
                    {
                        checkcode = *(dst + i);
                    }
                    else
                    {
                        if (sindex < (slen - 1))
                        {
                            checkcode ^= *(dst + i);
                        }
                    }
                    ++sindex;
                    ++i;
                }
                byte[] nbuffer = new byte[i];
                fixed (byte* nb = nbuffer)
                {
                    Nactive.Api.memcpy(nb, dst, i);
                }
                return nbuffer;
            }
        }
    }
}
