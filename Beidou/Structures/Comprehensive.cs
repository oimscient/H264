using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beidou.Structures
{
   public class Comprehensive
    {
        /// <summary>
        /// Appect身份
        /// </summary>
       public enum Id : byte
        {
            /// <summary>
            /// 车辆
            /// </summary>
            Car = 1,
            /// <summary>
            /// 用户
            /// </summary>
            User = 2,
            /// <summary>
            /// 音视频终端
            /// </summary>
            Video = 3
        }
        /// <summary>
        /// 全局Appect标识
        /// </summary>
        public static bool flag = true;
    }
}
