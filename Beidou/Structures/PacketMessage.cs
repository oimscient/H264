using System;
using System.Runtime.InteropServices;

namespace Beidou.Structures
{
    /// <summary>
    /// 数据打包信息结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketFrom
    {
        private byte _msgFlag = 0x7e;
        /// <summary>
        /// 消息包标志位0x7e（头标志和尾标识）共用
        /// </summary>
        public byte msgFlag { get { return _msgFlag; } }
        /// <summary>
        /// 消息ID
        /// </summary>
        public UInt16 msgId;
        /// <summary>
        /// 消息流水号,默认从0开始
        /// </summary>
        public UInt16 msgSerialnumber;
        /// <summary>
        /// 包总数,默认为1，只有分包才用
        /// </summary>
        public UInt16 pTotal = 1;
        /// <summary>
        /// 包序号，默认从1开始，只有分包才用
        /// </summary>
        public UInt16 pSerialnumber = 1;
        /// <summary>
        /// 加密标志,默认不加密,0不加密,否则其他加密方式
        /// </summary>
        public byte pEncryptFlag = 0;
        /// <summary>
        /// 默认不分包,0不分包，1分包
        /// </summary>
        public byte pSubFlag = 0;
        /// <summary>
        /// 六位BCD编码的SIM卡号,默认分配6个字节空间
        /// </summary>
        public byte[] simNumber = new byte[6];
        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] msgBody = null;

        internal byte[] Encoding()
        {
            UInt16 k = 12, blen = 0;
            //计算包的长度
            blen = (UInt16)(msgBody == null ? 0 : msgBody.Length);

            //数据包总长度
            int pLen = blen + (pSubFlag == 0 ? 12 : 16);

            //分配数据长度
            byte[] buffer = new byte[pLen];

            //消息头ID
            buffer[0] = (byte)(msgId >> 8);   //存高位
            buffer[1] = (byte)msgId;          //存低位

            //消息属性
            byte[] arr = (new PacketAttribute()
            {
                paEncryptFlag = pEncryptFlag,
                paSubFlag = pSubFlag,
                paMessageBodyLength = blen
            }).Encoding();

            buffer[2] = arr[0];
            buffer[3] = arr[1];

            //手机号
            simNumber.CopyTo(buffer, 4);//6 byte
            //buffer[4] = simNumber[0];
            //buffer[5] = simNumber[1];
            //buffer[6] = simNumber[2];
            //buffer[7] = simNumber[3];
            //buffer[8] = simNumber[4];
            //buffer[9] = simNumber[5];

            //流水号
            buffer[10] = (byte)(msgSerialnumber >> 8);
            buffer[11] = (byte)msgSerialnumber;

            //判断是否分包
            if (pSubFlag != 0)
            {
                //包总数
                buffer[12] = (byte)(pTotal >> 8);
                buffer[13] = (byte)pTotal;
                //包序号
                buffer[14] = (byte)(pSerialnumber >> 8);
                buffer[15] = (byte)pSerialnumber;
                k = 16;
            }
            if (blen > 0)
            {
                msgBody.CopyTo(buffer, k);
            }

            return Escape(buffer);
        }

        /// <summary>
        /// 数据转义
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        internal unsafe byte[] Escape(byte[] buffer)
        {
            int i = 0, index = 1, len = buffer.Length;
            int rlen = len + 3 + (len >> 4);
            byte checkcode = buffer[0];

            fixed (byte* dst = new byte[rlen], src = buffer)
            {
                dst[0] = _msgFlag;

                while (i < len)
                {
                    switch (*(src + i))
                    {
                        case 0x7e:
                            {
                                *(dst + index) = 0x7d;
                                *(dst + index + 1) = 0x02;
                                index += 2;
                            }
                            break;
                        case 0x7d:
                            {
                                *(dst + index) = 0x7d;
                                *(dst + index + 1) = 0x01;
                                index += 2;
                            }
                            break;
                        default:
                            *(dst + index) = *(src + i);
                            ++index;
                            break;
                    }

                    if (i >= 1)
                    {
                        checkcode ^= *(src + i);
                    }

                    ++i;
                }

                switch (checkcode)
                {
                    case 0x7e:
                        {
                            *(dst + index) = 0x7d;
                            *(dst + index + 1) = 0x02;
                            index += 2;
                        }
                        break;
                    case 0x7d:
                        {
                            *(dst + index) = 0x7d;
                            *(dst + index + 1) = 0x01;
                            index += 2;
                        }
                        break;
                    default:
                        {
                            *(dst + index) = checkcode;
                            ++index;
                        }
                        break;
                }

                *(dst + index) = _msgFlag;
                ++index;

                byte[] nbuffer = new byte[index];
                fixed (byte* b = nbuffer)
                {
                    Nactive.Api.memcpy(b, dst, index);
                }
                return nbuffer;
            }
        }
    }

    /// <summary>
    /// 消息包封装项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketTag
    {
        /// <summary>
        /// 消息包总数
        /// </summary>
        public UInt16 ptTotal;
        /// <summary>
        ///包序号
        /// </summary>
        public UInt16 ptSerialnumber;
    }

    /// <summary>
    /// 消息体属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketAttribute
    {
        /// <summary>
        ///分包标志,1分包,0不分包
        /// </summary>
        public byte paSubFlag;
        /// <summary>
        /// 加密标志,
        /// </summary>
        public byte paEncryptFlag;
        /// <summary>
        /// 消息体长度
        /// </summary>
        public UInt16 paMessageBodyLength;

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public byte[] Encoding()
        {
            UInt16 value = (UInt16)((paEncryptFlag << 10) | (paSubFlag << 13) | paMessageBodyLength);
            return new byte[2] { 
              (byte)(value>>8),
              (byte)value
            };
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="pAttribute"></param>
        public void Decoding(UInt16 pAttribute)
        {
            paMessageBodyLength = (UInt16)(pAttribute & 0x03FF);
            paEncryptFlag = (byte)((pAttribute >> 10) & 0x01);
            paSubFlag = (byte)((pAttribute >> 13) & 0x01);
        }
    }

    /// <summary>
    /// 数据包消息头结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketHead
    {
        /// <summary> 
        /// 消息头ID
        /// </summary>
        public UInt16 phMessageId;
        /// <summary>
        ///  消息流水号
        /// </summary>
        public UInt16 phSerialnumber;
        /// <summary>
        /// BCD码的Sim卡号,默认分配6个字节空间
        /// </summary>
        public byte[] hSimNumber = new byte[6];
        /// <summary>
        /// 消息包封装项结构
        /// </summary>
        public PacketTag phPackeHeadTag;
        /// <summary>
        /// 消息体属性
        /// </summary>
        public PacketAttribute phPacketHeadAttribute;
    }

    /// <summary>
    /// 数据包信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketMessage
    {
        /// <summary>
        /// 标识位,0x7e
        /// </summary>
        public UInt16 pmFlag = 0x7e;
        /// <summary>
        /// 校验码
        /// </summary>
        public byte pmCheckcode;
        /// <summary>
        /// 消息头
        /// </summary>
        public PacketHead pmPacketHead;
        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] pmMessageBody;
    }
}
