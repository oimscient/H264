using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace H264
{
    static class Tools
    {
       
        /// <summary>
        /// 视频数据接收队列
        /// </summary>
        public static ConcurrentQueue<byte[]> video = new ConcurrentQueue<byte[]>();
        /// <summary>
        /// 音频数据接收队列
        /// </summary>
        public static ConcurrentQueue<byte[]> Audio = new ConcurrentQueue<byte[]>();
        /// <summary>
        /// H264数据队列 数据体-M_PT-分包标记
        /// </summary>
        public static ConcurrentQueue<ValueTuple<byte[],string,string>> VideoQueues = new ConcurrentQueue<ValueTuple<byte[], string,string>>();
        /// <summary>
        /// H264 nalu数据队列
        /// </summary>
        public static ConcurrentQueue<byte[]> VideoNalu = new ConcurrentQueue<byte[]>();
        /// <summary>
        /// H264完整帧队列
        /// </summary>
        public static ConcurrentQueue<byte[]> H264Queue = new ConcurrentQueue<byte[]>();
        /// <summary>
        /// 音频帧队列
        /// </summary>1
        public static ConcurrentQueue<byte[]> audioQueue = new ConcurrentQueue<byte[]>();
        /// <summary>
        /// 录音队列
        /// </summary>1
        public static ConcurrentQueue<byte[]> RecorderQueue = new ConcurrentQueue<byte[]>();
        public static readonly object locks = new object();
        public static List<byte[]> TempList = new List<byte[]>();
        public static bool AudioIsEnd = false;
        //byte[]转换为Intptr
        public static IntPtr BytesToIntptr(byte[] bytes)
        {
            int size = bytes.Length;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return buffer;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }
}
