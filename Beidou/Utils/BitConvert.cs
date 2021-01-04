using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou
{
    public static class BitConvert
    {
        static bool isBigEndian = true;

        /// <summary>
        /// 获取或设置字节序
        /// </summary>
        public static bool IsBigEndian
        {
            get { return isBigEndian; }
            set { isBigEndian = value; }
        }

        /// <summary>
        /// 检查系统运行的字节序,true为大端,否则为小端
        /// </summary>
        /// <returns></returns>
        public static bool CheckSysIsBigEndian()
        {
            UInt16 flag = 0x4321;
            if ((byte)(flag >> 8) == 0x43)
                return false;
            else return true;
        }

        public static byte[] ToBytes(this UInt16 value)
        {
            if (isBigEndian)
            {
                return new byte[]{
                     (byte)(value >> 8),
                     (byte)value
                };
            }

            return new byte[] {
                (byte)value,
                (byte)(value>>8)
            };
        }

        public static byte[] ToBytes(this UInt32 value)
        {
            if (isBigEndian)
            {
                return new byte[] {
                 (byte)(value >> 24),
                 (byte)(value >> 16),
                 (byte)(value >> 8),
                 (byte)value
                 };
            }

            return new byte[] {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
        }

        public static byte[] ToBytes(this UInt64 value)
        {
            if (isBigEndian)
            {
                return new byte[]{
                  (byte)(value >> 56),
                  (byte)(value >> 48),
                  (byte)(value >> 40),
                  (byte)(value >> 32),
                  (byte)(value >> 24),
                  (byte)(value >> 16),
                  (byte)(value >> 8),
                  (byte)value
             };
            }

            return new byte[] {
            (byte)(byte)value,
                  (byte)(value >> 8),
                  (byte)(value >> 16),
                  (byte)(value >> 24),
                  (byte)(value >> 32),
                  (byte)(value >> 40),
                  (byte)(value >> 48),
                  (byte)(value >> 56)
            };
        }

        public static UInt16 ToUInt16(this byte[] value,int offset)
        {
            if (isBigEndian)
            {
                return (UInt16)((value[offset] << 8)| value[offset+1]);
            }

            return (UInt16)(value[offset]| (value[offset+1] << 8));
        }

        public static UInt32 ToUInt32(this byte[] value,int offset)
        {
            if (isBigEndian)
            {
                return (((UInt32)value[offset] << 24)
                   | ((UInt32)value[offset+1] << 16)
                   | ((UInt32)value[offset + 2] << 8)
                   | value[3]);
            }

            return value[offset]
                   | ((UInt32)value[offset + 1] << 8)
                   | ((UInt32)value[offset + 2] << 16)
                   | ((UInt32)value[offset + 3] << 24);
        }

        public static UInt64 ToUInt64(this byte[] value, int offset)
        {
            if (isBigEndian)
            {
                return (((UInt64)value[offset] << 56)
                  | ((UInt64)value[offset + 1] << 48)
                  | ((UInt64)value[offset + 2] << 40)
                  | ((UInt64)value[offset + 3] << 32)
                  | ((UInt64)value[offset + 4] << 24)
                  | ((UInt64)value[offset + 5] << 16)
                  | ((UInt64)value[offset + 6] << 8)
                  | value[offset + 7]);
            }

            return value[offset]
                 | ((UInt64)value[offset + 1] << 8)
                 | ((UInt64)value[offset + 2] << 16)
                 | ((UInt64)value[offset + 3] << 24)
                 | ((UInt64)value[offset + 4] << 32)
                 | ((UInt64)value[offset + 5] << 40)
                 | ((UInt64)value[offset + 6] << 48)
                 | ((UInt64)value[offset + 7] << 56);
        }


        /**
         * Byte转Bit
         */
        public static String ByteToBit(byte b)
        {
            return "" + (byte)((b >> 7) & 0x1) +
            (byte)((b >> 6) & 0x1) +
            (byte)((b >> 5) & 0x1) +
            (byte)((b >> 4) & 0x1) +
            (byte)((b >> 3) & 0x1) +
            (byte)((b >> 2) & 0x1) +
            (byte)((b >> 1) & 0x1) +
            (byte)((b >> 0) & 0x1);
        }
    }
}
