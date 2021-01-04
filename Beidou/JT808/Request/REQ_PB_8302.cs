using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beidou.JT808.Request
{
    /// <summary>
    /// 提问下发
    /// </summary>
   public class REQ_PB_8302
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");
        public REQ_PB_8302()
        { }
        /// <summary>
        /// 提问下发数据体打包
        /// </summary>
        /// <param name="flag">标志</param>
        /// <param name="question">问题内容</param>
        /// <param name="answerList">答案列表</param>
        /// <returns></returns>
        public byte[] Encode(PB8302 info)
        {
            //问题内容转换为bytes
            byte[] _question = encoding.GetBytes(info.question);
            List<byte> buffer = new List<byte>((info.answerList.Count << 3) + _question.Length + 1);

            //添加标志
            buffer.Add(FlagComposite(info.urgent, info.tts, info.showf));
            //添加问题内容长度
            byte len = (byte)_question.Length;
            buffer.Add(len);
            //添加问题内容
            buffer.AddRange(_question);

            byte[] temp = null;
            //添加答案列表
            for (int i = 0; i < info.answerList.Count; i++)
            {
                buffer.Add(info.answerList[i].Value);

                temp = encoding.GetBytes(info.answerList[i].StringValue);
                buffer.Add((byte)temp.Length);

                buffer.AddRange(temp);
            }
            return buffer.ToArray();
        }

        /// <summary>
        /// 标志位合成值为0-1
        /// </summary>
        /// <param name="urgent">1紧急</param>
        /// <param name="tts">1终端TTS报读</param>
        /// <param name="showf">1广告屏显示</param>
        /// <returns></returns>
        private byte FlagComposite(byte urgent, byte tts, byte showf)
        {
            byte def = 0;
            def |= urgent;
            def |= (byte)(tts << 3);
            def |= (byte)(showf << 4);
            return def;
        }
    }
}
