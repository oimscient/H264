using System;
using System.Runtime.InteropServices;

namespace Beidou.Structures
{
      [StructLayout(LayoutKind.Sequential)]
    public struct JT808_1078Cmd
    {
        #region 终端发送消息
        /// <summary>
        /// 终端注销
        /// </summary>
        public const UInt16 RSP_0003 = 0x0003;
        /// <summary>
        /// 终端心跳
        /// </summary>
        public const UInt16 RSP_0002 = 0x0002;
        /// <summary>
        /// 终端注册
        /// </summary>
        public const UInt16 RSP_0100 = 0x0100;
        /// <summary>
        /// 终端鉴权
        /// </summary>
        public const UInt16 RSP_0102 = 0x0102;
        /// <summary>
        /// 平台查询指定终端参数应答
        /// </summary>
        public const UInt16 RSP_0104 = 0x0104;
        /// <summary>
        /// 终端升级结果
        /// </summary>
        public const UInt16 RSP_0108 = 0x0108;
        /// <summary>
        /// 终端位置信息汇报(实时信息)
        /// </summary>
        public const UInt16 RSP_0200 = 0x0200;
        /// <summary>
        /// 终端位置信息查询应答
        /// </summary>
        public const UInt16 RSP_0201 = 0x0201;
        /// <summary>
        /// 终端查询终端属性应答
        /// </summary>
        public const UInt16 RSP_0107 = 0x0107;
        /// <summary>
        /// 摄像头立即拍摄应答
        /// </summary>
        public const UInt16 RSP_0805 = 0x0805;
        /// <summary>
        /// 终端通用应答
        /// </summary>
        public const UInt16 RSP_0001 = 0x0001;
        /// <summary>
        /// 多媒体事件信息上传
        /// </summary>
        public const UInt16 RSP_0800 = 0x0800;
        /// <summary>
        /// 多媒体数据上传应答
        /// </summary>
        public const UInt16 RSP_0801 = 0x0801;
        /// <summary>
        /// 存储多媒体数据检索应答
        /// </summary>
        public const UInt16 RSP_0802 = 0x0802;
        /// <summary>
        /// 行驶记录数据上传
        /// </summary>
        public const UInt16 RSP_0700 = 0x0700;
        /// <summary>
        /// 电子运单请求
        /// </summary>
        public const UInt16 RSP_0701 = 0x0701;
        /// <summary>
        /// 驾驶员身份信息采集上报
        /// </summary>
        public const UInt16 RSP_0702 = 0x0702;
        /// <summary>
        /// 定位数据批量上传
        /// </summary>
        public const UInt16 RSP_0704 = 0x0704;
        /// <summary>
        /// 车辆控制应答
        /// </summary>
        public const UInt16 RSP_0500 = 0x0500;
        /// <summary>
        /// 事件报告
        /// </summary>
        public const UInt16 RSP_0301 = 0x0301;
        /// <summary>
        /// 提问应答
        /// </summary>
        public const UInt16 RSP_0302 = 0x0302;
        /// <summary>
        /// 信息点播/取消应答
        /// </summary>
        public const UInt16 RSP_0303 = 0x0303;
        /// <summary>
        /// 数据压缩上报
        /// </summary>
        public const UInt16 RSP_0901 = 0x0901;
        /// <summary>
        /// 数据上行透传应答
        /// </summary>
        public const UInt16 RSP_0900 = 0x0900;
        /// <summary>
        ///终端公钥应答
        /// </summary>
        public const UInt16 RSP_0A00 = 0x0A00;
        #endregion

        #region 平台请求消息
        /// <summary>
        /// 终端注册应答
        /// </summary>
        public const UInt16 REQ_8100 = 0x8100;
        /// <summary>
        /// 平台查询指定终端参数
        /// </summary>
        public const UInt16 REQ_8106 = 0x8106;
        /// <summary>
        /// 平台查询终端属性请求
        /// </summary>
        public const UInt16 REQ_8107 = 0x8107;
        /// <summary>
        /// 发送终端升级包请求
        /// </summary>
        public const UInt16 REQ_8108 = 0x8108;
        /// <summary>
        /// 终端控制
        /// </summary>
        public const UInt16 REQ_8105 = 0x8105;
        /// <summary>
        /// 平台通用应答
        /// </summary>
        public const UInt16 REQ_8001 = 0x8001;
        /// <summary>
        /// 平台补传分包请求
        /// </summary>
        public const UInt16 REQ_8003 = 0x8003;
        /// <summary>
        /// 平台摄像头立即拍摄请求
        /// </summary>
        public const UInt16 REQ_8801 = 0x8801;
        /// <summary>
        /// 设置终端参数
        /// </summary>
        public const UInt16 REQ_8103 = 0x8103;
        /// <summary>
        /// 查询终端参数请求
        /// </summary>
        public const UInt16 REQ_8104 = 0x8104;
        /// <summary>
        /// 查询终端属性请求
        /// </summary>
        public const UInt16 REQ_8017 = 0x8107;
        /// <summary>
        /// 平台位置信息查询请求
        /// </summary>
        public const UInt16 REQ_8201 = 0x8201;
        /// <summary>
        /// 临时位置跟踪控制请求
        /// </summary>
        public const UInt16 REQ_8202 = 0x8202;
        /// <summary>
        /// 确认报警请求
        /// </summary>
        public const UInt16 REQ_8203 = 0x8203;
        /// <summary>
        /// 文本信息下发
        /// </summary>
        public const UInt16 REQ_8300 = 0x8300;
        /// <summary>
        /// 事件设置
        /// </summary>
        public const UInt16 REQ_8301 = 0x8301;
        /// <summary>
        /// 车辆控制请求
        /// </summary>
        public const UInt16 REQ_8500 = 0x8500;
        /// <summary>
        /// 圆形区域请求 
        /// </summary>
        public const UInt16 REQ_8600 = 0x8600;
        /// <summary>
        /// 删除圆形区域请求
        /// </summary>
        public const UInt16 REQ_8601 = 0x8601;
        /// <summary>
        /// 矩形区域请求
        /// </summary>
        public const UInt16 REQ_8602 = 0x8602;
        /// <summary>
        /// 删除矩形区域请求
        /// </summary>
        public const UInt16 REQ_8603 = 0x8603;
        /// <summary>
        /// 多边形区域请求
        /// </summary>
        public const UInt16 REQ_8604 = 0x8604;
        /// <summary>
        /// 删除多边形区域请求
        /// </summary>
        public const UInt16 REQ_8605 = 0x8605;
        /// <summary>
        /// 路线请求
        /// </summary>
        public const UInt16 REQ_8606 = 0x8606;
        /// <summary>
        /// 删除路线区域请求
        /// </summary>
        public const UInt16 REQ_8607 = 0x8607;
        /// <summary>
        /// 上报驾驶员身份信息请求
        /// </summary>
        public const UInt16 REQ_8702 = 0x8702;
        /// <summary>
        /// 行驶记录数据采集命令
        /// </summary>
        public const UInt16 REQ_8700 = 0x8700;
        /// <summary>
        /// 行驶记录参数下传命令
        /// </summary>
        public const UInt16 REQ_8701 = 0x8701;
        /// <summary>
        /// 录音开始命令
        /// </summary>
        public const UInt16 REQ_8804 = 0x8804;
        /// <summary>
        /// 电话回拨
        /// </summary>
        public const UInt16 REQ_8400 = 0x8400;
        /// <summary>
        /// 设置电话本请求
        /// </summary>
        public const UInt16 REQ_8401 = 0x8401;
        /// <summary>
        /// 问题下发请求
        /// </summary>
        public const UInt16 REQ_8302 = 0x8302;
        /// <summary>
        /// 信息点播菜单设置请求
        /// </summary>
        public const UInt16 REQ_8303 = 0x8303;
        /// <summary>
        /// 信息服务发送请求
        /// </summary>
        public const UInt16 REQ_8304 = 0x8304;
        /// <summary>
        /// 数据下行透传请求
        /// </summary>
        public const UInt16 REQ_8900 = 0x8900;
        /// <summary>
        /// 平台公钥请求
        /// </summary>
        public const UInt16 REQ_8A00 = 0x8A00;
        /// <summary>
        /// 登录验证请求
        /// </summary>
        public const UInt16 REQ_9100 = 0x9100;
        /// <summary>
        /// 音视频请求
        /// </summary>
        public const UInt16 REQ_9101 = 0x9101;
/*        /// <summary>
        /// 请求下载基本信息
        /// </summary>
        public const UInt16 REQ_9102 = 0x9102;*/
        /// <summary>
        /// 回复消息接收应答
        /// </summary>
        public const UInt16 REQ_9104 = 0x9104;
        /// <summary>
        /// 车辆历史记录查询请求
        /// </summary>
        public const UInt16 REQ_9106 = 0x9106;
        /// <summary>
        /// 修改密码请求
        /// </summary>
        public const UInt16 REQ_9108 = 0x9108;
        /// <summary>
        /// 查询车辆信息请求
        /// </summary>
        public const UInt16 REQ_9109 = 0x9109;
        /// <summary>
        /// 终端音视频属性
        /// </summary>
        public const UInt16 REQ_9003 = 0x9003;
        /// <summary>
        /// 终端音视频指令
        /// </summary>
        public const UInt16 REQ_9102 = 0x9102;
        #endregion
    }
      [StructLayout(LayoutKind.Sequential)]
    public struct JT809Cmd
    {
        #region 子业务类型
        #region UP_EXG_MSG 主链路动态信息交换消息
        /// <summary>
        /// 上传车辆注册信息
        /// </summary>
        public const UInt16 UP_EXG_MSG_REGISTER = 0x1201;
        /// <summary>
        /// 实时上传车辆定位信息
        /// </summary>
        public const UInt16 UP_EXG_MSG_REAL_LOCATION = 0x1202;
        /// <summary>
        /// 车辆定位信息自动补报
        /// </summary>
        public const UInt16 UP_EXG_MSG_HISTORY_LOCATION = 0x1203;
        /// <summary>
        /// 启动车辆定位信息交换应答
        /// </summary>
        public const UInt16 UP_EXG_MSG_RETURN_STARTUP_ACK = 0x1205;
        /// <summary>
        /// 结束车辆定位信息交换应答
        /// </summary>
        public const UInt16 UP_EXG_MSG_RETURN_END_ACK = 0x1206;
        /// <summary>
        /// 申请交换指定车辆定位信息请求
        /// </summary>
        public const UInt16 UP_EXG_MSG_APPLY_FOR_MONITOR_STARTUP = 0x1207;
        /// <summary>
        /// 取消交换指定车辆定位信息请求
        /// </summary>
        public const UInt16 UP_EXG_MSG_APPLY_FOR_MONITOR_END = 0x1208;
        /// <summary>
        /// 补发车辆定位信息请求
        /// </summary>
        public const UInt16 UP_EXG_MSG_APPLY_HISMSGBodyDATA_REQ = 0x1209;
        /// <summary>
        /// 上报车辆驾驶员身份信息应答
        /// </summary>
        public const UInt16 UP_EXG_MSG_REPORT_DRIVER_INFO_ACK = 0x120A;
        /// <summary>
        /// 上报车辆电子运单应答
        /// </summary>
        public const UInt16 UP_EXG_MSG_TAKE_EWAYBILL_ACK = 0x120B;
        /// <summary>
        /// 主动上报家驾驶员远身份信息
        /// </summary>
        public const UInt16 UP_EXG_MSG_REPORT_DRIVER_INFO = 0x120C;
        /// <summary>
        /// 主动上报车辆电子运单信息
        /// </summary>
        public const UInt16 UP_EXG_MSG_REPORT_EWAYBILL_INFO = 0x120D;
        #endregion

        #region DOWN_EXG_MSG 从链路动态信息交换消息
        /// <summary>
        /// 交换车辆定位信息
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_CAR_LOCATION = 0x9202;
        /// <summary>
        /// 车辆定位信息交换补发
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_HISTORY_ARCOSSAREA = 0x9203;
        /// <summary>
        /// 交换车辆静态信息
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_CAR_INFO = 0x9204;
        /// <summary>
        /// 启动车辆定位信息交换请求
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_RETURN_STARTUP = 0x9205;
        /// <summary>
        /// 结束车辆定位信息交换请求
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_RETURN_END = 0x9206;
        /// <summary>
        /// 申请交换指定车辆定位信息应答
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_APPLY_FOR_MONITOR_STARTUP_ACK = 0x9207;
        /// <summary>
        /// 取消交换指定车辆定位信息应答
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_APPLY_FOR_MONITOR_END_ACK = 0x9208;
        /// <summary>
        /// 补发车辆定位信息应答
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_APPLY_HISMSGBodyDATA_ACK = 0x9209;
        /// <summary>
        /// 上报车辆驾驶员身份信息请求
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_REPORT_DRIVERINFO = 0x920A;
        /// <summary>
        /// 上报车辆电子运单请求
        /// </summary>
        public const UInt16 DOWN_EXG_MSG_TAKE_EWAYBILL_REQ = 0x920B;
        #endregion

        #region UP_PLATFORM_MSG 主链路平台间信息交互消息
        /// <summary>
        /// 平台查岗应答
        /// </summary>
        public const UInt16 UP_PLATFORM_MSG_POST_QUERY_ACK = 0x1301;
        /// <summary>
        /// 下发平台间报文应答
        /// </summary>
        public const UInt16 UP_PLATFORM_MSG_INFO_ACK = 0x1302;
        #endregion

        #region DOWN_PLATFORM_MSG 从链路平台间信息交互消息
        /// <summary>
        /// 下发平台查岗请求
        /// </summary>
        public const UInt16 DOWN_PLATFORM_MSG_POST_QUERY_REQ = 0x9301;
        /// <summary>
        ///下发平台间报文请求
        /// </summary>
        public const UInt16 DOWN_PLATFORM_MSG_INFO_REQ = 0x9302;
        #endregion

        #region UP_WARN_MSG 主链路平台间信息交互消息
        /// <summary>
        /// 报警督办应答
        /// </summary>
        public const UInt16 UP_WARN_MSG_URGE_TODO_ACK = 0x1401;
        /// <summary>
        ///上报报警信息
        /// </summary>
        public const UInt16 UP_WARN_MSG_ADPT_INFO = 0x1402;
        /// <summary>
        /// 主动上报报警处理结果信息
        /// </summary>
        public const UInt16 UP_WARN_MSG_ADPT_TODO_INFO = 0x1403;

        #endregion

        #region DOWN_WARN_MSG 从链路报警信息交互消息
        /// <summary>
        /// 报警督办请求
        /// </summary>
        public const UInt16 DOWN_WARN_MSG_URGE_TODO_REQ = 0x9401;
        /// <summary>
        /// 报警预警
        /// </summary>
        public const UInt16 DOWN_WARN_MSG_INFORM_TIPS = 0x9402;
        /// <summary>
        /// 实时交换报警
        /// </summary>
        public const UInt16 DOWN_WARN_MSG_EXG_INFORM = 0x9403;
        #endregion

        #region UP_CTRL_MSG 主链路车辆监管消息
        /// <summary>
        /// 车辆单向监听应答
        /// </summary>
        public const UInt16 UP_CTRL_MSG_MONITOR_VEHICLE_ACK = 0x1501;
        /// <summary>
        /// 车辆拍照应答
        /// </summary>
        public const UInt16 UP_CTRL_MSG_TAKE_PHOTO_ACK = 0x1502;
        /// <summary>
        /// 下发车辆报文应答
        /// </summary>
        public const UInt16 UP_CTRL_MSG_TEXT_INFO_ACK = 0x1503;
        /// <summary>
        /// 上报车辆行驶记录应答
        /// </summary>
        public const UInt16 UP_CTRL_MSG_TAKE_TRAVEL_ACK = 0x1504;
        /// <summary>
        /// 车辆应急介入监管平台应答
        /// </summary>
        public const UInt16 UP_CTRL_MSG_EMERGENCY_MONITORING_ACK = 0x1505;
        #endregion

        #region DOWN_CTRL_MSG 从链路车辆监管消息
        /// <summary>
        /// 车辆单向监听请求
        /// </summary>
        public const UInt16 DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ = 0x9501;
        /// <summary>
        /// 车辆拍照请求
        /// </summary>
        public const UInt16 DOWN_CTRL_MSG_TAKE_PHOTO_REQ = 0x9502;
        /// <summary>
        /// 下发车辆报文请求
        /// </summary>
        public const UInt16 DOWN_CTRL_MSG_TEXT_INFO = 0x9503;
        /// <summary>
        /// 上报车辆行驶记录请求
        /// </summary>
        public const UInt16 DOWN_CTRL_MSG_TAKE_TRAVEL_REQ = 0x9504;
        /// <summary>
        /// 车辆应急接入监管平台请求
        /// </summary>
        public const UInt16 DOWN_CTRL_MSG_EMERGENCY_MONITORING_REQ = 0x9505;
        #endregion

        #region UP_BASE_MSG 主链路静态信息交换消息
        /// <summary>
        /// 补报车辆静态信息应答
        /// </summary>
        public const UInt16 UP_BASE_MSG_VEHICLE_ADDED_ACK = 0x1601;
        #endregion

        #region DOWN_BASE_MSG 从链路静态信息交换消息
        /// <summary>
        /// 补报车辆静态信息请求
        /// </summary>
        public const UInt16 DOWN_BASE_MSG_VEHICLE_ADDED = 0x9601;
        #endregion

        #endregion

        #region 主链路业务类型(消息头ID类型)
        /// <summary>
        /// 主链路登录请求
        /// </summary>
        public const UInt16 UP_CONNECT_REQ = 0x1001;
        /// <summary>
        /// 主链路登录应答
        /// </summary>
        public const UInt16 UP_CONNECT_RSP = 0x1002;
        /// <summary>
        /// 主链路注销请求
        /// </summary>
        public const UInt16 UP_DISCONNECT_REQ = 0x1003;
        /// <summary>
        /// 主链路注销应答
        /// </summary>
        public const UInt16 UP_DISCONNECT_RSP = 0x1004;
        /// <summary>
        /// 主链路连接保持请求
        /// </summary>
        public const UInt16 UP_LINKEST_REQ = 0x1005;
        /// <summary>
        /// 主链路连接保持应答
        /// </summary>
        public const UInt16 UP_LINKEST_RSP = 0x1006;
        /// <summary>
        /// 主链路断开通知
        /// </summary>
        public const UInt16 UP_DISCONNECT_INFORM = 0x1007;
        /// <summary>
        /// 下级平台主动关闭链路通知
        /// </summary>
        public const UInt16 UP_CLOSELINK_INFORM = 0x1008;
        /// <summary>
        /// 主链路动态信息交换
        /// </summary>
        public const UInt16 UP_EXG_MSG = 0x1200;
        /// <summary>
        /// 主链路平台间信息交互
        /// </summary>
        public const UInt16 UP_PLATFORM_MSG = 0x1300;
        /// <summary>
        /// 主链路报警信息交互
        /// </summary>
        public const UInt16 UP_WARN_MSG = 0x1400;
        /// <summary>
        /// 主链路车辆监管
        /// </summary>
        public const UInt16 UP_CTRL_MSG = 0x1500;
        /// <summary>
        /// 主链路静态信息交换
        /// </summary>
        public const UInt16 UP_BASE_MSG = 0x1600;
        #endregion

        #region 从链路业务类型(消息头ID类型)
        /// <summary>
        /// 从链路连接请求
        /// </summary>
        public const UInt16 DOWN_CONNECT_REQ = 0x9001;
        /// <summary>
        /// 从链路连接应答
        /// </summary>
        public const UInt16 DOWN_CONNECT_RSP = 0x9002;
        /// <summary>
        /// 从链路注销请求
        /// </summary>
        public const UInt16 DOWN_DISCONNECT_REQ = 0x9003;
        /// <summary>
        /// 从链路注销应答
        /// </summary>
        public const UInt16 DOWN_DICONNECT_RSP = 0x9004;
        /// <summary>
        /// 从链路连接保持请求
        /// </summary>
        public const UInt16 DOWN_LINKTEST_REQ = 0x9005;
        /// <summary>
        /// 从链路连接保持应答
        /// </summary>
        public const UInt16 DOWN_LINKTEST_RSP = 0x9006;
        /// <summary>
        /// 从链路断开通知
        /// </summary>
        public const UInt16 DOWN_DISCONNECT_INFORM = 0x9007;
        /// <summary>
        /// 上级平台主动关闭链路通知
        /// </summary>
        public const UInt16 DOWN_CLOSELINK_INFORM = 0x9008;
        /// <summary>
        /// 接收定位信息数量通知
        /// </summary>
        public const UInt16 DOWN_TOTAL_RECY_BACK_MSG = 0x9101;
        /// <summary>
        /// 从链路动态信息交换
        /// </summary>
        public const UInt16 DOWN_EXG_MSG = 0x9200;
        /// <summary>
        /// 从链路平台间信息交互
        /// </summary>
        public const UInt16 DOWN_PLATFORM_MSG = 0x9300;
        /// <summary>
        /// 从链路报警信息交互
        /// </summary>
        public const UInt16 DOWN_WARN_MSG = 0x9400;
        /// <summary>
        ///从链路车辆监管
        /// </summary>
        public const UInt16 DOWN_CTRL_MSG = 0x9500;
        /// <summary>
        /// 从链路静态信息交换
        /// </summary>
        public const UInt16 DOWN_BASE_MSG = 0x9600;
        #endregion
    }

      /// <summary>
      /// 国家海洋电子信息协议定位模式
      /// </summary>
      [StructLayout(LayoutKind.Sequential)]
      public struct NMEAMode
      {
          /// <summary>
          /// 北斗卫星模式
          /// </summary>
          public const string DB = "BD";
          /// <summary>
          /// GPS卫星模式
          /// </summary>
          public const string GP = "GP";
          /// <summary>
          /// 双模式
          /// </summary>
          public const string GN = "GN";
          /// <summary>
          /// GLONASS模式 
          /// </summary>
          public const string GL = "GL";
          /// <summary>
          /// 伽利略模式
          /// </summary>
          public const string GA = "GA";
          /// <summary>
          /// 计算机模式
          /// </summary>
          public const string CC = "CC";
          /// <summary>
          /// 自定义模式
          /// </summary>
          public const string CF = "CF";
      }

      /// <summary>
      /// 国家海洋电子信息协议定位标识符
      /// </summary>
      [StructLayout(LayoutKind.Sequential)]
      public struct NMEAFlag
      {
          /// <summary>
          /// 位置信息
          /// </summary>
          public const string GGA = "GGA";
          /// <summary>
          /// 地理位置坐标信息
          /// </summary>
          public const string GLL = "GLL";
          /// <summary>
          /// 有效卫星信号（推荐）
          /// </summary>
          public const string GSA = "GSA";
          /// <summary>
          /// 可视卫星
          /// </summary>
          public const string GSV = "GSV";
          /// <summary>
          /// 最简导航位置信息
          /// </summary>
          public const string RMC = "RMC";
          /// <summary>
          /// 文本信息
          /// </summary>
          public const string TXT = "TXT";
          /// <summary>
          /// 日历信息
          /// </summary>
          public const string ZDA = "ZDA";
          /// <summary>
          /// 地面速度
          /// </summary>
          public const string VTG = "VTG";
          /// <summary>
          /// 卫星信号信息
          /// </summary>
          public const string MSS = "MSS";
      }
}
