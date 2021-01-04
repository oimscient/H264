using Beidou;
using Beidou.JT808;
using Beidou.Utils;
using NAudio.Codecs;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H264
{
    class Recorder

    { //创建输入对象
        private readonly WaveInEvent waveIn = new WaveInEvent();
        private WaveFormat WaveFormat;
        private void PlaySound()
        {
            //输入音频参数设置 8k/16位/通道1
            WaveFormat = new WaveFormat(8000,16, 1);
            //为wavein的回调函数添加事件，用于操作数据
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(WaveIn_DataAvailable);
        }
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
        // e.Buffer为用于操作的数组，类型为byte,长度1600
            Tools.RecorderQueue.Enqueue(e.Buffer);
        }
        public void StartRecording()
        {
            PlaySound();
            waveIn.StartRecording();
        }
        public void StopRecording()
        {
            waveIn.StopRecording();
        }
        /// <summary>
        /// pcm->G711A
        /// </summary>
        /// <param name="g711Buffer"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] Encode(byte[] data, int offset, int length)
        {
            byte[] encoded = new byte[length / 2];
            int outIndex = 0;
            for (int n = 0; n < length; n += 2)
            {
                encoded[outIndex++] = ALawEncoder.LinearToALawSample(BitConverter.ToInt16(data, offset + n));
            }
            return encoded;
        }
    }
}
