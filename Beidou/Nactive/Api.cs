using System;
using System.Runtime.InteropServices;

namespace Beidou.Nactive
{
    public class Api
    { 
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern byte* memcpy(
            byte* dst,
            byte* src,
            int count);

        [DllImport("msvcrt.dll")]
        public static extern unsafe int memcmp(void* src, void* dst, int count);
    }
}
