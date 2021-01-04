using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou
{
    public static class Extension
    {
        public static void CopyTo(this byte[] src, int srcIndex, byte[] dst, int dstIndex, int count)
        {
            unsafe
            {
                fixed (byte* _src = src, _dst = dst)
                {
                    MemoryCopy(_src + srcIndex, _dst + dstIndex, count);
                }
            }
        }

        public static byte[] Copy(this byte[] src, int srcIndex, int count)
        {
            byte[] dst = new byte[count];
            unsafe
            {
                fixed (byte* _src = src, _dst = dst)
                {
                    MemoryCopy(_src + srcIndex, _dst, count);
                }
            }
            return dst;
        }

        public static int CompareTo(this byte[] src, byte[] dst)
        {
            int count = src.Length - dst.Length;
            if (count != 0) return count;

            count = dst.Length;

            unsafe
            {
                fixed (byte* _src = src, _dst = dst)
                {
                    return BytesCompareTo(_src, _dst, count);
                }
            }
        }

        /// <summary>
        /// BCD编码字节转换为char数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static char[] BCDToChar(this byte[] bytes)
        {
            char[] number = new char[(bytes.Length << 1)];
            int tempCount = 0, i = 0;

            unsafe
            {
                fixed (byte* src = bytes)
                {
                    fixed (char* dst = number)
                    {
                        while (i < bytes.Length)
                        {
                            *(dst + tempCount) = (char)(*(src + i) >> 4);
                            *(dst + tempCount + 1) = (char)(*(src + i) & 0x0F);
                            tempCount += 2;
                            ++i;
                        }
                    }
                }
            }
            return number;
        }

        /// <summary>
        /// BCD编码字节转换为string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BCDToString(this byte[] bytes)
        {
            char[] strChars = new char[bytes.Length << 1];

            int len = 0, index = 0;
            unsafe
            {
                fixed (byte* d = bytes)
                {
                    byte* ds = d;
                    do
                    {
                        strChars[index] += (char)(((*ds) >> 4) + 48);
                        strChars[index += 1] += (char)(((*ds) & 0x0F) + 48);
                        ++ds;
                        ++index;
                        ++len;
                    }
                    while (len < bytes.Length && index < strChars.Length);
                }
            }
            return new string(strChars);
        }

        /// <summary>
        /// char转换为BCD编码字节
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static byte[] ToBCD(this char[] chars)
        {
            byte[] number = new byte[(chars.Length >> 1)];
            int len = number.Length;
            unsafe
            {
                fixed (char* sr = chars)
                {
                    fixed (byte* dst = number)
                    {
                        char* s = sr;
                        byte* dt = dst;
                        do
                        {
                            *(dt++) = (byte)(((*(s++) - 48) << 4) | (*(s++) - 48));
                        }
                        while ((--len) > 0);
                    }
                }
            }
            return number;
        }

        /// <summary>
        /// 字符串转换为BCD编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBCD(this string value)
        {
            //保证为偶数位
            if (value.Length % 2 != 0) value.Insert(0, "0");

            byte[] number = new byte[(value.Length >> 1)];

            int len = number.Length;
            unsafe
            {
                fixed (char* sr = value)
                {
                    fixed (byte* dst = number)
                    {
                        char* s = sr;
                        byte* dt = dst;
                        do
                        {
                            *(dt++) = (byte)(((*(s++) - 48) << 4) | (*(s++) - 48));
                        }
                        while ((--len) > 0);
                    }
                }
            }
            return number;
        }

        public static DateTime BCDToTimeFormat(this byte[] data, TimeFormat tFormat = TimeFormat.yyMMddHHmmss)
        {
            string str = data.BCDToString();
            return DateTime.ParseExact(str, tFormat.ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将日期转换为BCD格式
        /// </summary>
        /// <param name="time"></param>
        /// <param name="tFormat">时间格式</param>
        /// <returns></returns>
        public static byte[] TimeFormatToBCD(this DateTime time, TimeFormat tFormat = TimeFormat.yyMMddHHmmss)
        {
            string timeStr = time.ToString(tFormat.ToString());
            return timeStr.ToBCD();
        }

        /// <summary>
        /// 序列化为时间格式格式
        /// </summary>
        /// <param name="timeString"></param>
        /// <param name="tFormat">时间格式</param>
        /// <returns></returns>
        public static string StringTimeFormat(this string timeString, TimeFormat tFormat = TimeFormat.yyyyMMddHHmmss)
        {
            return DateTime.ParseExact(timeString, tFormat.ToString(), System.Globalization.CultureInfo.InvariantCulture).ToString();
        }

        public static int IndexOf(string original,string partten)
        {
            return JKMP(original, partten);
        }

        private static int JKMP(string str, string partten)
        {
            int j = 0, i = 0, pi = -1;
            while (i < str.Length)
            {
                if (str[i] == partten[j])
                {
                    if (pi == -1 && j != 0 && partten[0] == str[i])
                        pi = i;//记录主串中的字符是否有和子串的第一个字符相同的位置
                    ++i;
                    ++j;
                    if (j >= partten.Length) //匹配完成
                        return i - j;
                }
                else
                {
                    if (pi != -1)//字符不相同则移动pi个长度，提高效率的关键部分就是这个地方了。
                    {
                        i = pi + 1;
                        j = 1;
                        pi = -1;
                    }
                    else//和普通的匹配一样，匹配不成功则子串从0开始,主串继续下一个匹配
                    {
                        j = 0;
                        ++i;
                    }
                }
            }
            return -1;
        }

        internal static unsafe int BytesCompareTo(byte* src, byte* dst, int count)
        {

            int index = 0, temp = 0;
            do
            {
                temp = (*(src + index)) - (*(dst + index));
                if (temp > 0) return temp;
                else if (temp < 0) return temp;
                ++index;
            }
            while (index < count);
            return temp;
        }

        internal static unsafe ulong BlockCompareTo(byte* src, byte* dst, int count)
        {
            ulong temp = 0;
            int c = count;
            if (c >= 16)
            {
                do
                {
                    temp = *((ulong*)src) - *((ulong*)dst);
                    if (temp != 0) return temp;

                    temp = *((ulong*)(src + 8)) - *((ulong*)(dst + 8));
                    if (temp != 0) return temp;

                    dst += 16;
                    src += 16;
                }
                while ((c -= 16) >= 16);
            }
            if (c > 0)
            {
                if ((c & 8) != 0)
                {
                    temp = *((ulong*)src) - *((ulong*)dst);
                    if (temp != 0) return temp;

                    dst += 8;
                    src += 8;
                }
                if ((c & 4) != 0)
                {
                    temp = *((uint*)src) - *((uint*)dst);
                    if (temp != 0) return temp;

                    dst += 4;
                    src += 4;
                }
                if ((c & 2) != 0)
                {
                    temp = (ulong)(*((ushort*)src) - *((ushort*)dst));
                    if (temp != 0) return temp;

                    dst += 2;
                    src += 2;
                }
                if ((c & 1) != 0)
                {
                    temp = (ulong)(src[0] - dst[0]);
                    if (temp != 0) return temp;

                    //dst++;
                    //src++;
                }
            }
            return temp;
        }

        internal static unsafe void MemoryCopy(byte* src, byte* dest, int count)
        {
            if (count >= 16)
            {
                do
                {
                    *((ulong*)dest) = *((ulong*)src);
                    *((ulong*)(dest + 8)) = *((ulong*)(src + 8));
                    dest += 16;
                    src += 16;
                }
                while ((count -= 16) >= 16);
            }
            if (count > 0)
            {
                if ((count & 8) != 0)
                {
                    *((ulong*)dest) = *((ulong*)src);
                    dest += 8;
                    src += 8;
                }
                if ((count & 4) != 0)
                {
                    *((uint*)dest) = *((uint*)src);
                    dest += 4;
                    src += 4;
                }
                if ((count & 2) != 0)
                {
                    *((ushort*)dest) = *((ushort*)src);
                    dest += 2;
                    src += 2;
                }
                if ((count & 1) != 0)
                {
                    dest[0] = src[0];
                    //dest++;
                    //src++;
                }
            }

        }
    }

    public enum TimeFormat
    {
        yyMMdd,
        yyMMddHHmmss,
        yyyyMMddHHmmss,
        yyyyMM,
        HHmmss,
        HHmm,
        HH
    }
}
