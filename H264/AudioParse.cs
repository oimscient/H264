using Beidou;
using Beidou.JT808;
using Beidou.Utils;
using NAudio.Codecs;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using videos;

namespace H264
{
    class AudioParse
    {
        private static Thread AudioThread;
        private static Thread AudioSendThread;
        private static Thread RecorderThread;
        private static WaveOut waveOut;            //播放器
        private static BufferedWaveProvider bufferedWaveProvider;       //5s缓存区
        private static WaveFormat wf;
        private static readonly Recorder Recorder = new Recorder();
        private static readonly byte[] his = new byte[] { 0, 1, 160, 0 };
        private static byte[] G711A;
        private static byte[] PCM;
        private static UInt16 index;
        private static long time;
        /// <summary>
        /// Naudio初始化
        /// </summary>
        public static void NaudioInit()
        {

            Thread AudioThread = new Thread(audio)
            {
                IsBackground = true
            };
            AudioThread.Start();
            void audio()
            {
                waveOut = new WaveOut();
                wf = new WaveFormat(8000, 16, 1);
                bufferedWaveProvider = new BufferedWaveProvider(wf);
                waveOut.Init(bufferedWaveProvider);
                waveOut.Play();
            }
        }
        /// <summary>
        /// 向音频缓存区中添加数据，不要将缓存区填满
        /// </summary>
        /// <param name="data">要填入的数据</param>
        /// <param name="position">数据的起始位置</param>
        /// <param name="len">数据的长度</param>
        public static void AddDataToBufferedWaveProvider(byte[] data, int position, int len)
        {
            bufferedWaveProvider.AddSamples(data, position, len);
        }
        /// <summary>
        /// 对讲开启
        /// </summary>
        public static void AudioPaly()
        {
            Tools.AudioIsEnd = true;
            AudioThread = new Thread(Playaudio)
            {
                IsBackground = true
            };
            AudioThread.Start();

            AudioSendThread = new Thread(SendAudio)
            {
                IsBackground = true
            };
            AudioSendThread.Start();

            RecorderThread = new Thread(Recorder.StartRecording)
            {
                IsBackground = true
            };
            RecorderThread.Start();

            void Playaudio()
            {
                while (Tools.AudioIsEnd)
                {
                    if (Tools.audioQueue.Count > 0)
                    {
                        Tools.audioQueue.TryDequeue(out byte[] AudioByte);
                        if (AudioByte.Length > 10000) continue;
                        byte[] G711data = AudioByte.Skip(4).ToArray();
                        byte[] PCMbuffer = Decode(G711data, 0, G711data.Length);
                        AddDataToBufferedWaveProvider(PCMbuffer, 0, PCMbuffer.Length);
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
            }

            void SendAudio()
            {
                index = 0;
                time = 1000000000000000;
                while (Tools.AudioIsEnd)
                {
                    if (Tools.RecorderQueue.Count > 1)
                    {
                        Tools.RecorderQueue.TryDequeue(out byte[] AudioByte);
                        Tools.RecorderQueue.TryDequeue(out byte[] AudioByte2);
                        try
                        {
                            PCM = AudioByte.Concat(AudioByte2).ToArray();
                            for (int i = 0; i < 5; i++)
                            {
                                G711A = new byte[324];
                                G711A = his.Concat(Recorder.Encode(PCM.Skip(i * 640).Take(640).ToArray(), 0, 640)).ToArray();
                                byte[] RtpBuffer = RTP.Encode(new RTPBody()
                                {
                                    state = new byte[] { 48, 49, 99, 100 },
                                    Vpxc = 129,
                                    MPT = 134,
                                    index = index,
                                    hSimNumber = Extension.ToBCD("013304521781"),
                                    chanle = 6,
                                    type = 51,
                                    time = split(time),
                                    length = 324,
                                    data = G711A
                                });
                                index += 1;
                                time += 40;
                                AsynSend(Connect.Audioclient, RtpBuffer);
                                Thread.Sleep(20);
                            }
                            byte[] split(long times)
                            {
                                int i = 0;
                                List<byte> list = new List<byte>();
                                string str = times.ToString();
                                while (i < 16)
                                {
                                    list.Add(byte.Parse(str.Substring(i, 2)));
                                    i += 2;
                                }
                                return list.ToArray();
                            }

                        }
                        catch
                        {
                        }

                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
            }
        }

        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="message"></param>
        public static void AsynSend(Socket socket, byte[] message)
        {
            if (socket == null) return;
            try
            {
                socket.BeginSend(message, 0, message.Length, SocketFlags.None, asyncResult =>
                {
                    socket.EndSend(asyncResult);
                }, null);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 对讲关闭
        /// </summary>
        public static void AudioStop()
        {
            Tools.AudioIsEnd = false;
            Recorder.StopRecording();
            bufferedWaveProvider.ClearBuffer();
            AudioThread.DisableComObjectEagerCleanup();
        }
        /// <summary>
        /// G711_>pcm
        /// </summary>
        /// <param name="g711Buffer"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] Decode(byte[] data, int offset, int length)
        {
            byte[] decoded = new byte[length * 2];
            int outIndex = 0;
            for (int n = 0; n < length; n++)
            {
                short decodedSample = ALawDecoder.ALawToLinearSample(data[n + offset]);
                decoded[outIndex++] = (byte)(decodedSample & 0xFF);
                decoded[outIndex++] = (byte)(decodedSample >> 8);
            }
            return decoded;
        }
    }
}
