using Beidou;
using Beidou.JT808;
using Beidou.JT808.Reponse;
using H264;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RtpDecode
{
    public class H264Dec
    {
        private const int HI_H264DEC_OK = 0;
        private const int HI_H264DEC_NEED_MORE_BITS = -1;
        private const int HI_H264DEC_NO_PICTURE = -2;
        private VideoInit VideoInits;
        #region 解码器相关声明
        /// <summary>
        /// 数据的句柄
        /// </summary>
        /// <summary>
        /// 这是解码器属性信息
        /// </summary>
        private HiH264_DEC_ATTR_S decAttr;
        /// <summary>
        /// 这是解码器输出图像信息
        /// </summary>
        private HiH264_DEC_FRAME_S _decodeFrame = new H264Dec.HiH264_DEC_FRAME_S();
        /// <summary>
        /// 解码器句柄
        /// </summary>
        private IntPtr _decHandle;
        private static readonly double[,] YUV2RGB_CONVERT_MATRIX = new double[3, 3] { { 1, 0, 1.4022 }, { 1, -0.3456, -0.7145 }, { 1, 1.771, 0 } };
        //解码结束
        private bool IsEnd = false;
        private Video Videobody;
        private Video Audiobody;
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecCreate", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Hi264DecCreate(ref HiH264_DEC_ATTR_S pDecAttr);
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecDestroy", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Hi264DecDestroy(IntPtr hDec);
        /// <summary>
        /// 流解码
        /// </summary>
        /// <param name="hDec">解码器句柄</param>
        /// <param name="pStream">码流起始地址</param>
        /// <param name="iStreamLen">码流长度</param>
        /// <param name="ullPTS">时间戳信息</param>
        /// <param name="pDecFrame">图像信息</param>
        /// <param name="uFlags">解码模式 0：正常解码；1、解码完毕并要求解码器输出残留图像</param>
        /// <returns></returns>
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecFrame", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Hi264DecFrame(IntPtr hDec, IntPtr pStream, uint iStreamLen, ulong ullPTS, ref HiH264_DEC_FRAME_S pDecFrame, uint uFlags);
        /// <summary>
        /// 完整帧解码
        /// </summary>
        /// <param name="hDec">解码器句柄</param>
        /// <param name="pStream">码流起始地址</param>
        /// <param name="iStreamLen">码流长度</param>
        /// <param name="ullPTS">时间戳信息</param>
        /// <param name="pDecFrame">图像信息</param>
        /// <param name="uFlags">解码模式 0：正常解码；1、解码完毕并要求解码器输出残留图像</param>
        /// <returns></returns>
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecAU", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Hi264DecAU(IntPtr hDec, IntPtr pStream, uint iStreamLen, ulong ullPTS, ref HiH264_DEC_FRAME_S pDecFrame, uint uFlags);
        /// <summary>
        /// 解码器属性信息。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct HiH264_DEC_ATTR_S
        {
            /// <summary>
            /// 解码器输出图像格式，目前解码库只支持YUV420图像格式
            /// </summary>
            public uint uPictureFormat;
            /// <summary>
            /// 输入码流格式 0x00: 目前解码库只支持以“00 00 01”为nalu分割符的流式H.264码流 
            /// </summary>
            public uint uStreamInType;
            /// <summary>
            /// 图像宽度
            /// </summary>
            public uint uPicWidthInMB;
            /// <summary>
            /// 图像高度
            /// </summary>
            public uint uPicHeightInMB;
            /// <summary>
            /// 参考帧数目
            /// </summary>
            public uint uBufNum;
            /// <summary>
            /// 解码器工作模式
            /// </summary>
            public uint uWorkMode;
            /// <summary>
            /// 用户私有数据
            /// </summary>
            public IntPtr pUserData;
            /// <summary>
            /// 保留字
            /// </summary>
            public uint uReserved;

        }

        /// <summary>
        /// 解码器输出图像信息数据结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct HiH264_DEC_FRAME_S
        {
            /// <summary>
            /// Y分量地址
            /// </summary>
            public IntPtr pY;
            /// <summary>
            /// U分量地址
            /// </summary>
            public IntPtr pU;
            /// <summary>
            /// V分量地址
            /// </summary>
            public IntPtr pV;
            /// <summary>
            /// 图像宽度(以像素为单位)
            /// </summary>
            public uint uWidth;
            /// <summary>
            /// 图像高度(以像素为单位)
            /// </summary>
            public uint uHeight;
            /// <summary>
            /// 输出Y分量的stride (以像素为单位)
            /// </summary>
            public uint uYStride;
            /// <summary>
            /// 输出UV分量的stride (以像素为单位)
            /// </summary>
            public uint uUVStride;
            /// <summary>
            /// 图像裁减信息:左边界裁减像素数
            /// </summary>
            public uint uCroppingLeftOffset;
            /// <summary>
            /// 图像裁减信息:右边界裁减像素数
            /// </summary>
            public uint uCroppingRightOffset;
            /// <summary>
            /// 图像裁减信息:上边界裁减像素数
            /// </summary>
            public uint uCroppingTopOffset;
            /// <summary>
            /// 图像裁减信息:下边界裁减像素数
            /// </summary>
            public uint uCroppingBottomOffset;
            /// <summary>
            /// 输出图像在dpb中的序号
            /// </summary>
            public uint uDpbIdx;
            /// <summary>
            /// 图像类型：0:帧; 1:顶场; 2:底场 */
            /// </summary>
            public uint uPicFlag;
            /// <summary>
            /// 图像类型：0:帧; 1:顶场; 2:底场 */
            /// </summary>
            public uint bError;
            /// <summary>
            /// 图像是否为IDR帧：0:非IDR帧;1:IDR帧
            /// </summary>
            public uint bIntra;
            /// <summary>
            /// 时间戳
            /// </summary>
            public ulong ullPTS;
            /// <summary>
            /// 图像信号
            /// </summary>
            public uint uPictureID;
            /// <summary>
            /// 保留字
            /// </summary>
            public uint uReserved;
            /// <summary>
            /// 指向用户私有数据
            /// </summary>
            public IntPtr pUserData;

        }
        private static byte[] rgbFrame;
        // private readonly int bufferLen = 0x600;
        #endregion

        /// <summary>
        /// 解码器相关初始化,load中进行初始化
        /// </summary>
        public void Init()
        {
            decAttr = new HiH264_DEC_ATTR_S
            {
                // 这是解码器属性信息
                uPictureFormat = 0,
                uStreamInType = 0,
                /* 解码器最大图像宽高, D1图像(1280x720) */
                uPicWidthInMB = /*(uint)width*/352,
                uPicHeightInMB = /*(uint)height*/288,
                /* 解码器最大参考帧数: 16 */
                uBufNum = 30,
                /* bit0 = 1: 标准输出模式; bit0 = 0: 快速输出模式 */
                /* bit4 = 1: 启动内部Deinterlace; bit4 = 0: 不启动内部Deinterlace */
                uWorkMode = 0x00
            };
            //创建、初始化解码器句柄
            _decHandle = Hi264DecCreate(ref decAttr);
        }

        /// <summary>
        /// 解码流
        /// </summary>
        public void H264()
        {
            while (IsEnd)
            {
                if (Tools.H264Queue.Count > 0)
                {
                    Tools.H264Queue.TryDequeue(out byte[] tempByte);
                    IntPtr pData = Marshal.AllocHGlobal(tempByte.Length);
                    Marshal.Copy(tempByte, 0, pData, tempByte.Length);
                    int result = Hi264DecFrame(_decHandle, pData, (uint)tempByte.Length, 0, ref _decodeFrame, 0);
                    while (HI_H264DEC_NEED_MORE_BITS != result)
                    {
                        if (HI_H264DEC_NO_PICTURE == result)
                        {
                            IsEnd = true;
                            break;
                        }
                        if (HI_H264DEC_OK == result)/* 输出一帧图像 */
                        {
                            rgbFrame = new byte[3 * 352 * 288];
                            //计算 y u v 的长度
                            var yLength = _decodeFrame.uHeight * _decodeFrame.uYStride;
                            var uLength = _decodeFrame.uHeight * _decodeFrame.uUVStride / 2;
                            var vLength = uLength;

                            var yBytes = new byte[yLength];
                            var uBytes = new byte[uLength];
                            var vBytes = new byte[vLength];

                            //_decodeFrame 是解码后的数据对象，里面包含 YUV 数据、宽度、高度等信息
                            Marshal.Copy(_decodeFrame.pY, yBytes, 0, (int)yLength);
                            Marshal.Copy(_decodeFrame.pU, uBytes, 0, (int)uLength);
                            Marshal.Copy(_decodeFrame.pV, vBytes, 0, (int)vLength);

                            //转为yv12格式
                            byte[] yuvBytes = new byte[yBytes.Length + uBytes.Length + vBytes.Length];
                            Array.Copy(yBytes, 0, yuvBytes, 0, yBytes.Length);
                            Array.Copy(vBytes, 0, yuvBytes, yBytes.Length, vBytes.Length);
                            Array.Copy(uBytes, 0, yuvBytes, yBytes.Length + vBytes.Length, uBytes.Length);

                            //更新显示
                            ConvertYUV2RGB(yuvBytes, rgbFrame, 352, 288);
                            WriteBMP(rgbFrame, 352, 288);
                        }
                        /* 继续解码剩余H.264码流 */
                        result = Hi264DecFrame(_decHandle, IntPtr.Zero, 0, 0, ref _decodeFrame, 0);
                    }
                }
                Thread.Sleep(10);
            }
            /* 销毁解码器 */
            H264Dec.Hi264DecDestroy(_decHandle);
        }

        /// <summary>
        /// 将一桢 YUV 格式的图像转换为一桢 RGB 格式图像。
        /// </summary>
        /// <param name="yuvFrame">YUV 格式图像数据。</param>
        /// <param name="rgbFrame">RGB 格式图像数据。</param>
        /// <param name="width">图像宽（单位：像素）。</param>
        /// <param name="height">图像高（单位：像素）。</param>
        static void ConvertYUV2RGB(byte[] yuvFrame, byte[] rgbFrame, int width, int height)
        {
            int uIndex = width * height;
            int vIndex = uIndex + ((width * height) >> 2);
            int gIndex = width * height;
            int bIndex = gIndex * 2;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // R分量
                    int temp = (int)(yuvFrame[y * width + x] + (yuvFrame[vIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[0, 2]);
                    rgbFrame[y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));

                    // G分量
                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[uIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[1, 1] + (yuvFrame[vIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[1, 2]);
                    rgbFrame[gIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));

                    // B分量
                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[uIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[2, 1]);
                    rgbFrame[bIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));
                }
            }
        }
        void WriteBMP(byte[] rgbFrame, int width, int height)
        {
            // 写 BMP 图像文件。
            int yu = width * 3 % 4;
            int bytePerLine = 0;
            yu = yu != 0 ? 4 - yu : yu;
            bytePerLine = width * 3 + yu;

            int length = rgbFrame.GetLength(0);

            //将文件头以及数据写到内存流里面
            using (MemoryStream stream = new MemoryStream(length + 54))
            {
                using (BinaryWriter bw = new BinaryWriter(stream, Encoding.Default))
                {
                    bw.Write('B');
                    bw.Write('M');
                    bw.Write(bytePerLine * height + 54);
                    bw.Write(0);
                    bw.Write(54);
                    bw.Write(40);
                    bw.Write(width);
                    bw.Write(height);
                    bw.Write((ushort)1);
                    bw.Write((ushort)24);
                    bw.Write(0);
                    bw.Write(bytePerLine * height);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);

                    byte[] data = new byte[bytePerLine * height];
                    int gIndex = width * height;
                    int bIndex = gIndex * 2;
                    for (int y = height - 1, j = 0; y >= 0; y--, j++)
                    {
                        for (int x = 0, i = 0; x < width; x++)
                        {
                            data[y * bytePerLine + i++] = rgbFrame[bIndex + j * width + x];
                            // B                    
                            data[y * bytePerLine + i++] = rgbFrame[gIndex + j * width + x];
                            // G          
                            data[y * bytePerLine + i++] = rgbFrame[j * width + x];
                            // R          
                        }
                    }
                    bw.Write(data, 0, data.Length);
                    Bitmap image = new Bitmap(stream);
                    Form1.Form.pictureBox.Invoke(new EventHandler(UpdateErrorBoxs));
                    void UpdateErrorBoxs(object o, EventArgs es)
                    {
                        Form1.Form.pictureBox.Image = image;
                    }
                    stream.Flush();
                    stream.Close();
                    bw.Flush();
                    bw.Close();

                }
            }
        }
        /// <summary>
        /// 启动工作线程
        /// </summary>
        public void Start()
        {
            VideoInits = new VideoInit();
            IsEnd = true;
            ///处理原始视频RTP包
            Thread RTPVideoThread = new Thread(RData)
            {
                IsBackground = true
            };
            RTPVideoThread.Start();


            ///处理原始音频RTP包
            Thread RTPAudioThread = new Thread(AudioData)
            {
                IsBackground = true
            };
            RTPAudioThread.Start();

            ///H264分包处理
            Thread H264DThread = new Thread(PData)
            {
                IsBackground = true
            };
            H264DThread.Start();
            ///视频解码线程
            Thread Thread1 = new Thread(H264)
            {
                IsBackground = true
            };
            Thread1.Start();
        }
        /// <summary>
        /// 处理原始RTP包,分离H264码流
        /// </summary>
        public void RData()
        {
            int over = -1;
            bool flag = true;
            byte[] temp = null;
            while (IsEnd)
            {
                if (Tools.video.Count > 0)
                {
                    if (flag == true)
                    {
                        Tools.video.TryDequeue(out temp);
                        flag = false;
                    }
                    try
                    {
                        for (int index = 0; index < temp.Length - 4; index++)
                        {
                            if (temp[index] == 0x30 && temp[index + 1] == 0x31 && temp[index + 2] == 0x63 && temp[index + 3] == 0x64)
                            {
                                over = index;
                                if (over > 0)
                                {
                                    ///取出RTP单包
                                    byte[] temps = temp.Take(index).ToArray();
                                    ///解析RTP
                                    Videobody = VideoInits.Decode(temps);
                                    //视频放入H264队列
                                    Tools.VideoQueues.Enqueue(new ValueTuple<byte[], string, string>(Videobody.data, BitConvert.ByteToBit(Videobody.M_PT).Substring(1, 7), BitConvert.ByteToBit(Videobody.type).Substring(4, 4)));
                                    //取出剩余数据包
                                    temp = temp.Skip(index).ToArray();
                                    break;
                                }
                            }
                        }
                        if (over < 1)
                        {
                            ///取出下一个RTP包
                            if (Tools.video.Count > 0)
                            {
                                byte[] temp1 = temp;
                                Tools.video.TryDequeue(out byte[] temp2);
                                ///上一包的剩余数据与下一包拼接进入下一循环
                                temp = new byte[temp2.Length + temp1.Length];
                                temp1.CopyTo(temp, 0);
                                temp2.CopyTo(temp, temp1.Length);
                            }
                            else
                            {
                                while (IsEnd)
                                {
                                    if (Tools.video.Count <= 0)
                                    {
                                        Thread.Sleep(10);
                                    }
                                    else
                                    {
                                        byte[] temp1 = temp;
                                        Tools.video.TryDequeue(out byte[] temp2);
                                        ///上一包的剩余数据与下一包拼接进入下一循环
                                        temp = new byte[temp2.Length + temp1.Length];
                                        temp1.CopyTo(temp, 0);
                                        temp2.CopyTo(temp, temp1.Length);
                                        break;
                                    }
                                }
                            }
                        }
                        over = -1;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }

                }
                else
                {
                    Thread.Sleep(10);
                }
            }

        }
        /// <summary>
        /// H264分包处理
        /// </summary>
        public void PData()
        {
            while (IsEnd)
            {
                if (Tools.VideoQueues.Count > 0)
                {
                    Tools.VideoQueues.TryDequeue(out ValueTuple<byte[], string, string> data);
                    if (data.Item2 == "1100010")
                    {
                        switch (data.Item3)
                        {
                            case "0000":
                                Tools.H264Queue.Enqueue(data.Item1);
                                break;
                            case "0001":
                                Tools.TempList.Add(data.Item1);
                                break;
                            case "0010":
                                Tools.TempList.Add(data.Item1);
                                Tools.H264Queue.Enqueue(H264(Tools.TempList));
                                Tools.TempList.Clear();
                                break;
                            case "0011":
                                Tools.TempList.Add(data.Item1);
                                break;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(5);
                }
            }
            byte[] H264(List<byte[]> temp)
            {

                int length = 0;
                foreach (byte[] i in temp)
                {
                    length += i.Length;
                }
                byte[] H264D = new byte[length];
                int templength = 0;
                foreach (byte[] i in temp)
                {
                    i.CopyTo(H264D, templength);
                    templength += i.Length;
                }
                return H264D;

            }
        }
        /// <summary>
        /// 处理原始RTP包,分离G711码流
        /// </summary>
        public void AudioData()
        {
            int over = -1;
            bool flag = true;
            byte[] temp = null;
            while (IsEnd)
            {
                if (Tools.Audio.Count > 0)
                {
                    if (flag == true)
                    {
                        Tools.Audio.TryDequeue(out temp);
                        flag = false;
                    }
                    try
                    {
                        for (int index = 0; index < temp.Length - 4; index++)
                        {
                            if (temp[index] == 0x30 && temp[index + 1] == 0x31 && temp[index + 2] == 0x63 && temp[index + 3] == 0x64)
                            {
                                over = index;
                                if (over > 0)
                                {
                                    ///取出RTP单包
                                    byte[] temps = temp.Take(index).ToArray();
                                    ///解析RTP
                                    Audiobody = VideoInits.Decode(temps);
                                    if (Tools.AudioIsEnd)
                                    {
                                        //音频帧放入队列
                                        Tools.audioQueue.Enqueue(Audiobody.data);
                                    }
                                    //取出剩余数据包
                                    temp = temp.Skip(index).ToArray();
                                    break;
                                }
                            }
                        }
                        if (over < 1)
                        {
                            ///取出下一个RTP包
                            if (Tools.Audio.Count > 0)
                            {
                                byte[] temp1 = temp;
                                Tools.Audio.TryDequeue(out byte[] temp2);
                                ///上一包的剩余数据与下一包拼接进入下一循环
                                temp = new byte[temp2.Length + temp1.Length];
                                temp1.CopyTo(temp, 0);
                                temp2.CopyTo(temp, temp1.Length);
                            }
                            else
                            {
                                while (IsEnd)
                                {
                                    if (Tools.Audio.Count <= 0)
                                    {
                                        Thread.Sleep(20);
                                    }
                                    else
                                    {
                                        byte[] temp1 = temp;
                                        Tools.Audio.TryDequeue(out byte[] temp2);
                                        ///上一包的剩余数据与下一包拼接进入下一循环
                                        temp = new byte[temp2.Length + temp1.Length];
                                        temp1.CopyTo(temp, 0);
                                        temp2.CopyTo(temp, temp1.Length);
                                        break;
                                    }
                                }
                            }
                        }
                        over = -1;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }

                }
                else
                {
                    Thread.Sleep(20);
                }
            }

        }
    }
}